using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
//using Unity.Services.Vivox;

    
public class voIP : MonoBehaviour
{
    async void InitializeAsync()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        //await VivoxService.Instance.InitializeAsync();
    }

    public async void LoginToVivoxAsync()
    {
        //LoginOptions options = new LoginOptions();
        //options.DisplayName = UserDisplayName;
        //options.EnableTTS = true;
        //await VivoxService.Instance.LoginAsync(options);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
