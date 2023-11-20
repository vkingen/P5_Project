using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Samples.Utilities;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : NetworkBehaviour
{

    [SerializeField] private GameObject m_playerPrefab;

    //private void Awake()
    //{
    //    NetworkManager.Singleton.SceneManager.OnLoadComplete += OnLoadComplete;
    //}


    //private void OnLoadComplete(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    //{
    //    GameObject go =
    //       NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(
    //           m_playerPrefab,
    //           transform.position,
    //           clientId,
    //           true);
    //}
}
