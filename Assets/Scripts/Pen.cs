using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pen : MonoBehaviour
{
    public Transform tip;
    public Material drawingMaterial;
    public Material tipMaterial;
    [Range(0.01f, 0.1f)]
    public float penWidth;
    public Color[] penColors;

    public LineTextureMode textureMode = LineTextureMode.Tile;

    public bool isGrabbed = false;
    private bool colorSwitchTrigger;


    private LineRenderer currentDrawing;
    private int index;
    private int currentColorIndex;

    public InputActionProperty drawInput;
    public InputActionProperty colorChangeInput;

    public void IsGrabCheck(bool isGrabbing)
    {
        isGrabbed = isGrabbing;
    }


    private void Start()
    {
        currentColorIndex = 0;
        tipMaterial.color = penColors[currentColorIndex];
    }

    private void Update()
    {
        // Debugging
        if(Input.GetKey(KeyCode.P))
        {
            Debug.Log("HE");
            Draw();
        }

        if (drawInput.action.ReadValue<float>() > 0f && isGrabbed)
        {
            Draw();
        }
        else if(currentDrawing != null)
        {
            currentDrawing = null;
        }
        
        
        if(colorChangeInput.action.ReadValue<float>() > 0f && isGrabbed)
        {
            if(!colorSwitchTrigger)
            {
                colorSwitchTrigger = true;
                SwitchColor();
            }
        }
        else
        {
            colorSwitchTrigger = false;
        }
    }

    public void Draw()
    {
        if(currentDrawing== null)
        {
            index = 0;
            currentDrawing = new GameObject().AddComponent<LineRenderer>();
            currentDrawing.material = drawingMaterial;
            currentDrawing.textureMode = textureMode;
            currentDrawing.startColor = currentDrawing.endColor = penColors[currentColorIndex];
            currentDrawing.startWidth = currentDrawing.endWidth = penWidth;
            currentDrawing.positionCount = 1;
            currentDrawing.SetPosition(0, tip.transform.position);
        }
        else
        {
            Vector3 currentPosition = currentDrawing.GetPosition(index);
            if(Vector3.Distance(currentPosition, tip.transform.position) > 0.01f)
            {
                index++;
                currentDrawing.positionCount = index + 1;
                currentDrawing.SetPosition(index, tip.transform.position);
            }
        }
    }

    public void SwitchColor()
    {
        if(currentColorIndex == penColors.Length -1)
        {
            currentColorIndex = 0;
        }
        else
        {
            currentColorIndex++;
        }
        tipMaterial.color = penColors[currentColorIndex];
    }
}
