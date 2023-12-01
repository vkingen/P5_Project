using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayers : NetworkBehaviour
{
    [SerializeField] private Vector2 placementArea = new Vector2(-10f, 10f);

    public override void OnNetworkSpawn()
    {
        DisableClientInput();
    }

    public void DisableClientInput()
    {
        if(IsClient && !IsOwner)
        {
            var clientMoveProvider = GetComponent<NetworkMoveProvider>();
            var clientControllers = GetComponentsInChildren<ActionBasedController>();
            var clientTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();
            var clientHead = GetComponentInChildren<TrackedPoseDriver>();
            var clientCamera = GetComponentInChildren<Camera>();
            //var clientBrush = GetComponentInChildren<NetworkBrush>();
            // clientBrush.enabled = false;
            //var interactionManager = GetComponentInChildren<XRInteractionManager>(); // TEST

            clientCamera.enabled = false;
            clientMoveProvider.enableInputActions = false;
            clientTurnProvider.enableTurnLeftRight = false;
            clientTurnProvider.enableTurnAround = false;
            clientHead.enabled= false;

            //interactionManager.enabled = false; // TEST

            foreach (var controller in clientControllers)
            {
                controller.enableInputActions= false;
                controller.enableInputTracking= false;
            }
        }
    }

    private void Start()
    {
        if(IsClient && IsOwner)
        {
            transform.position = new Vector3(Random.Range(placementArea.x, placementArea.y), transform.position.y, Random.Range(placementArea.x, placementArea.y));
        }
    }

    public void OnSelectGrabbable(SelectEnterEventArgs eventArgs)
    {
        if(IsClient && IsOwner)
        {
            NetworkObject networkObjectSelected = eventArgs.interactableObject.transform.GetComponent<NetworkObject>();
            if(networkObjectSelected != null)
            {
                // Request ownership from server
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

        }
    }
}
