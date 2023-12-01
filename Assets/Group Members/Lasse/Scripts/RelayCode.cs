using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RelayCode : MonoBehaviour
{
    private DontDestroyData dontDestroyData;
    private TMP_Text relayCodeText;

    private void Awake()
    {
        dontDestroyData = FindObjectOfType<DontDestroyData>();
        relayCodeText = GetComponent<TMP_Text>();
        relayCodeText.text = dontDestroyData.relayCode;
    }
}
