using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyData : MonoBehaviour
{
    [HideInInspector] public string relayCode;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
