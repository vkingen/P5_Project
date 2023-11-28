using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class TestRelay : MonoBehaviour
{
    [SerializeField] private NetworkSceneChange networkSceneChange;

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in: " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    //public TMP_Text code;
    public async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3); // 3 = number of possible players - 1: Host + 3

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            //string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            DontDestroyData dontDestroyData = FindObjectOfType<DontDestroyData>();
            dontDestroyData.relayCode = joinCode;
            //code.text = joinCode;
            //Debug.Log(joinCode);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();
            networkSceneChange.LoadSceneNetwork(); 
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }

    public async void JoinRelay(string joinCode)
    {
        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);


            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException e)
        {
            // Delete inputfield text = "";
            Debug.Log(e);
        }
    }

    //public async void CreateRelay() // Deprecated
    //{
    //    try
    //    {
    //        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3); // 3 = number of possible players - 1: Host + 3

    //        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

    //        Debug.Log(joinCode);

    //        NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
    //            allocation.RelayServer.IpV4,
    //            (ushort)allocation.RelayServer.Port,
    //            allocation.AllocationIdBytes,
    //            allocation.Key,
    //            allocation.ConnectionData
    //        );

    //        NetworkManager.Singleton.StartHost();
    //    }
    //    catch (RelayServiceException e)
    //    {
    //        Debug.Log(e);
    //    }
    //}


    //public async void JoinRelay(string joinCode) // Deprecated
    //{
    //    try
    //    {
    //        JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);



    //        NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
    //            joinAllocation.RelayServer.IpV4,
    //            (ushort)joinAllocation.RelayServer.Port,
    //            joinAllocation.AllocationIdBytes,
    //            joinAllocation.Key,
    //            joinAllocation.ConnectionData,
    //            joinAllocation.HostConnectionData
    //        );

    //        NetworkManager.Singleton.StartClient();
    //    }
    //    catch (RelayServiceException e)
    //    {
    //        Debug.Log(e);
    //    }
    //}
}
