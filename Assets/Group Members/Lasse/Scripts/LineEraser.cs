using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LineEraser : MonoBehaviour
{
    public InputActionProperty eraseInput;

    private void OnTriggerEnter(Collider other)
    {
        bool eraseTriggerPressed = eraseInput.action.ReadValue<float>() > 0f;

        if (eraseTriggerPressed)
        {
            if (other.gameObject.CompareTag("line"))
            {
                Destroy(other.gameObject);
            }
        }

    }
}
