﻿using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class Brush : NetworkBehaviour 
{
    // Prefab to instantiate when we draw a new brush stroke
    [SerializeField] private GameObject _brushStrokePrefab = null;

    public InputActionProperty drawInput;

    public GameObject origin;

    private BrushStroke1 _activeBrushStroke;

    private ulong brushStrokeInstanceId = 0;



    //A list to hold the gameobjects instantiated, in order to be able to delete them again
    //public List<GameObject> sceneObjects = new List<GameObject>(); 

    [ServerRpc (RequireOwnership = false)]
    private void SpawnMesh_ServerRpc(Vector3 pos, Quaternion rot, ServerRpcParams serverRpcParams = default)
    {
        
        GameObject brushStrokeGameObject = Instantiate(_brushStrokePrefab);

        NetworkObject networkObject = brushStrokeGameObject.GetComponent<NetworkObject>();
        networkObject.Spawn();
        
        
        

        // Grab the BrushStroke component from it
        _activeBrushStroke = brushStrokeGameObject.GetComponent<BrushStroke1>();

        // Tell the BrushStroke to begin drawing at the current brush position
        _activeBrushStroke.Initialize(pos, rot);
        // _activeBrushStroke.BeginBrushStrokeWithBrushTipPoint(pos, rot);

        var clientId = serverRpcParams.Receive.SenderClientId;
        

        // NOTE! In case you know a list of ClientId's ahead of time, that does not need change,
        // Then please consider caching this (as a member variable), to avoid Allocating Memory every time you run this function
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { clientId }
            }
        };

        var id = networkObject.NetworkObjectId;
        Debug.Log("OUR KEY: " + id);
        SetLocalBrushStrokeInstanceId_ClientRpc((ulong) id, clientRpcParams);

        // InitializeBrushStroke(brushStrokeGameObject, pos, rot);
    }

    [ClientRpc]
    public void SetLocalBrushStrokeInstanceId_ClientRpc(ulong id, ClientRpcParams clientRpcParams)
    {
        brushStrokeInstanceId = id;
        if(!NetworkManager.SpawnManager.SpawnedObjects.ContainsKey(id))
        {
            Debug.LogError("DIDN?T HAVE KEY " + id);
            return;
        }
        else
        {
            Debug.Log("WE CONTAIN KEY " + id);
            _activeBrushStroke = NetworkManager.SpawnManager.SpawnedObjects[id].GetComponent<BrushStroke1>();

        }
    }

    private void Update() 
    {
        // Get the position & rotation of the point from which we draw
        Vector3 originPos = origin.transform.position;
        Quaternion originRot = origin.transform.rotation;

        // Figure out if the trigger is pressed or not
        bool drawTriggerPressed = drawInput.action.ReadValue<float>() > 0f;

        // If the trigger is pressed and we haven't created a new brush stroke to draw, create one!
        if (drawTriggerPressed && _activeBrushStroke == null) {
            // Instantiate a copy of the Brush Stroke prefab.


            SpawnMesh_ServerRpc(originPos, originRot);

            //sceneObjects.Add(brushStrokeGameObject);


        }

        // If the trigger is pressed, and we have a brush stroke, move the brush stroke to the new brush tip position
        if (drawTriggerPressed)
            _activeBrushStroke.MoveBrushTipToPoint(originPos, originRot);

        // If the trigger is no longer pressed, and we still have an active brush stroke, mark it as finished and clear it.
        if (!drawTriggerPressed && _activeBrushStroke != null) {
            _activeBrushStroke.EndBrushStrokeWithBrushTipPoint(originPos, originRot);
            _activeBrushStroke = null;

            //Tilføjet af Marius
            /*MeshCollider collider = _brushStrokePrefab.GetComponent<MeshCollider>();
            Mesh meshComponent = _brushStrokePrefab.transform.GetComponentInChildren<Mesh>();
            collider.sharedMesh = meshComponent;
            collider.convex = true;
            collider.isTrigger = true;*/

            string gameObjectName = "BrushStroke(Clone)";

            /*if (sceneObjects != null)
            {
                foreach (GameObject sceneObject in sceneObjects)
                {
                    MeshCollider collider = sceneObject.GetComponent<MeshCollider>();

                    // Check if the MeshCollider is not already present
                    if (collider == null)
                    {
                        // If not, add a new MeshCollider
                        collider = sceneObject.AddComponent<MeshCollider>();
                    }

                    Transform child = sceneObject.transform.GetChild(0);

                    //MeshFilter meshFilter = sceneObject.GetComponentInChildren<MeshFilter>();

                    //Transform meshFilter2 = meshFilter.transform.GetChild(0);
                    MeshFilter meshFilter = child.GetComponent<MeshFilter>();

                    print(meshFilter.gameObject.name);

                    // Check if a MeshFilter is found
                    if (meshFilter != null)
                    {
                        Mesh meshComponent = meshFilter.sharedMesh;

                        // Check if a mesh is found in the MeshFilter
                        if (meshComponent != null)
                        {
                            // Set the sharedMesh of the MeshCollider
                            collider.sharedMesh = meshComponent;
                            collider.convex = true;
                            collider.isTrigger = true;
                        }
                        else
                        {
                            Debug.LogError("Mesh not found in MeshFilter.");
                        }
                    }
                    else
                    {
                        Debug.LogError("MeshFilter not found.");
                    }
                }
            }
            else
            {
                Debug.LogError("GameObject with name '" + gameObjectName + "' not found in the scene.");
            }

            /*MeshCollider collider = _brushStrokePrefab.AddComponent<MeshCollider>();
            Mesh meshComponent = _brushStrokePrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
            collider.sharedMesh = meshComponent;
            collider.convex = true;
            collider.isTrigger = true;*/

        }
    }
}
