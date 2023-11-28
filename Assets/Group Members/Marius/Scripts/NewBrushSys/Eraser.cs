using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Eraser : MonoBehaviour
{
    public InputActionProperty eraseInput;

    private void OnTriggerËnter(Collider collider)
    {
        print("OnTriggerEnter Works");
        bool eraseTriggerPressed = eraseInput.action.ReadValue<float>() > 0f;

        if (eraseTriggerPressed)
        {
            if (collider.CompareTag("Mesh"))
            {
                Destroy(collider.gameObject);
            }
        }
    }
}
