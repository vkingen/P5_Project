using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetStringInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public TestRelay relay;

    public void CheckInput()
    {
        if (inputField.text.Length == 6)
        {
            relay.JoinRelay(inputField.text);
        }
        else
        {
            inputField.text = "";
        }
    }
}
