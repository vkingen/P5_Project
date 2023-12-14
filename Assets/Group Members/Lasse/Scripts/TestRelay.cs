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

    public async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(6); // 6 = number of possible players

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            DontDestroyData dontDestroyData = FindObjectOfType<DontDestroyData>();
            dontDestroyData.relayCode = joinCode;

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
            Debug.Log(e);
        }
    }

    public async void CreateServerRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(6); // 6 = number of possible players

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            DontDestroyData dontDestroyData = FindObjectOfType<DontDestroyData>();
            dontDestroyData.relayCode = joinCode;

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartServer();
            networkSceneChange.LoadSceneNetwork();
        }
        catch (RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
}
