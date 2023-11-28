using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Components;


using Unity.Netcode; // TEST

public class NetworkTransformClient : NetworkTransform
{
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        CanCommitToTransform = IsOwner;
    }

    protected override void Update()
    {
        base.Update();

        if (NetworkManager.Singleton != null && (NetworkManager.Singleton.IsConnectedClient || NetworkManager.Singleton.IsConnectedClient))
        {
            CanCommitToTransform = IsOwner;

            if (CanCommitToTransform)
            {
                TryCommitTransformToServer(transform, NetworkManager.LocalTime.Time);
            }
        }
    }
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
