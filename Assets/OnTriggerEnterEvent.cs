using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Samples.Utilities;
using UnityEngine;

public class OnTriggerEnterEvent : MonoBehaviour
{
    public NetworkObjectSpawner networkObjectSpawner;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finger"))
        {
            //networkObjectSpawner.SpawnNetworkObjectRuntime();
        }
    }
}
