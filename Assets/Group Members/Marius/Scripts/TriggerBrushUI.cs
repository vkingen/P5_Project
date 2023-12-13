using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBrushUI : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if(other == CompareTag("Player"))
        {
            Debug.Log("Triggered");

            GameObject uiBrushCanvas = other.transform.Find("UIBrushCanvas").gameObject;

            uiBrushCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == CompareTag("Player"))
        {
            GameObject uiBrushCanvas = other.transform.Find("UIBrushCanvas").gameObject;

            uiBrushCanvas.gameObject.SetActive(false);
        }
    }
}
