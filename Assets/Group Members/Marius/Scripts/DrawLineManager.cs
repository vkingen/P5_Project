using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawLineManager : MonoBehaviour
{

    public GameObject lineOrigin;

    public Material drawingMaterial;

    [Range(0.01f, 0.1f)]
    public float lineWidth = 0.01f;

    [Range(0.01f, 0.1f)]
    public float lineWidthFactor = 0.001f;

    //public Color[] penColors;

    public LineTextureMode textureMode = LineTextureMode.Tile;

    private bool colorSwitchTrigger;


    private LineRenderer currentDrawing;
    private int index;
    private int currentColorIndex;

    public InputActionProperty drawInput;
    public InputActionProperty changeWidthInput;


    private void Start()
    {
        currentColorIndex = 0;
        //tipMaterial.color = Color.red;
    }

    private void Update()
    {
        if (drawInput.action.ReadValue<float>() > 0f)
        {
            Draw();
        }
        else if (currentDrawing != null)
        {
            currentDrawing = null;
        }

        ChangeLineWidth();


        /*if (colorChangeInput.action.ReadValue<float>() > 0f && isGrabbed)
        {
            if (!colorSwitchTrigger)
            {
                colorSwitchTrigger = true;
                SwitchColor();
            }
        }
        else
        {
            colorSwitchTrigger = false;
        }*/
    }

    public void Draw()
    {
        if (currentDrawing == null)
        {
            index = 0;
            currentDrawing = new GameObject().AddComponent<LineRenderer>();
            currentDrawing.material = drawingMaterial;
            currentDrawing.numCornerVertices = 90;
            currentDrawing.numCapVertices = 90;
            //currentDrawing.textureScale = new Vector2(100, 200);
            currentDrawing.textureMode = textureMode;
            currentDrawing.startColor = currentDrawing.endColor = Color.red;
            currentDrawing.startWidth = currentDrawing.endWidth = lineWidth;
            currentDrawing.positionCount = 1;
            currentDrawing.SetPosition(0, lineOrigin.transform.position);
        }
        else
        {
            Vector3 currentPosition = currentDrawing.GetPosition(index);
            if (Vector3.Distance(currentPosition, lineOrigin.transform.position) > 0.01f)
            {
                index++;
                currentDrawing.positionCount = index + 1;
                currentDrawing.SetPosition(index, lineOrigin.transform.position);
            }
        }
    }

    private bool maxWidthReached = false;
    private bool minWidthReached = false;

    private void ChangeLineWidth()
    {
        if (changeWidthInput.action.ReadValue<float>() > 0)
        {
            if (maxWidthReached)
            {
                Vector3 originSize = new Vector3(currentDrawing.startWidth, 0.00191f, currentDrawing.startWidth);
                lineOrigin.transform.localScale = originSize;

                lineWidth = lineWidth - lineWidthFactor;
            }
            else if (minWidthReached)
            {
                Vector3 originSize = new Vector3(currentDrawing.startWidth, 0.00191f, currentDrawing.startWidth);
                lineOrigin.transform.localScale = originSize;

                lineWidth = lineWidth + lineWidthFactor;
            }
        }

        if (lineWidth <= 0.01f)
        {
            print("Increase width");
            minWidthReached = true;
            maxWidthReached = false;
        }
        else if (lineWidth >= 0.1f)
        {
            print("Decrease width");
            minWidthReached = false;
            maxWidthReached = true;
        }



            
            //lineWidth = lineWidth + lineWidthFactor;
            /*
            if (lineWidth <= 0.01)
            {
                lineWidth = lineWidth + lineWidthFactor;
            }
            else if (lineWidth >= 0)
            {
                lineWidth = lineWidth - lineWidthFactor;
            }*/
                
    }

    /*public void SwitchColor()
    {
        if (currentColorIndex == penColors.Length - 1)
        {
            currentColorIndex = 0;
        }
        else
        {
            currentColorIndex++;
        }
        tipMaterial.color = penColors[currentColorIndex];
    }

    
    private void OnDrawLineStarted(InputAction.CallbackContext context)
    {
        print("Hello");
        GameObject line = new GameObject();
        line.AddComponent<LineRenderer>();
        currLine = line.AddComponent<LineRenderer>();
        numClicks = 0;
    }*/
}