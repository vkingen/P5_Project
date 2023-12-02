using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : NetworkBehaviour
{
    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Renderer[] meshToDisable;

    public GameObject objectToSpawn;

    public InputActionProperty input;


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(IsOwner)
        {
            foreach (var item in meshToDisable)
            {
                item.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (IsOwner)
        {
            root.position = VRRigReferences.instance.root.position;
            root.rotation = VRRigReferences.instance.root.rotation;

            head.position = VRRigReferences.instance.head.position;
            head.rotation = VRRigReferences.instance.head.rotation;

            leftHand.position = VRRigReferences.instance.leftHand.position;
            leftHand.rotation = VRRigReferences.instance.leftHand.rotation;

            rightHand.position = VRRigReferences.instance.rightHand.position;
            rightHand.rotation = VRRigReferences.instance.rightHand.rotation;


            if (input.action.ReadValue<float>() > 0f || Input.GetKeyDown(KeyCode.K))
            {
                SpawnObjectServerRpc();
            }
        }
    }
  
    [ServerRpc]
    public void SpawnObjectServerRpc()
    {
        //GameObject gameObjectClone = Instantiate(objectToSpawn, transform.position + new Vector3(0,2,2), transform.rotation);
        GameObject spawnedObject = Instantiate(objectToSpawn, transform.position + new Vector3(0, 2, 2),transform.rotation);
        spawnedObject.GetComponent<NetworkObject>().Spawn();

    }

    public void OnSelectGrabbable(SelectEnterEventArgs eventArgs)
    {
        if(IsClient && IsOwner)
        {
            NetworkObject networkObjectSelected = eventArgs.interactableObject.transform.GetComponent<NetworkObject>();
            if(networkObjectSelected != null)
            {
                // Request ownership
                RequestGrabbableOwnershipServerRpc(OwnerClientId, networkObjectSelected);
            }
        }
    }
    [ServerRpc]
    public void RequestGrabbableOwnershipServerRpc(ulong newOwnerClientId, NetworkObjectReference networkObjectReference)
    {
        if(networkObjectReference.TryGet(out NetworkObject networkObject))
        {
            networkObject.ChangeOwnership(newOwnerClientId);
        }
        else
        {
            Debug.LogWarning("Unable To Change ownership for clientid:" + newOwnerClientId);
        }
    }
}
