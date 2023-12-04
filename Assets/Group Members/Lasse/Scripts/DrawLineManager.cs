using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DrawLineManager : MonoBehaviour
{
    public GameObject lineOrigin;

    [SerializeField] private Slider lineWidthSlider;

    [SerializeField] private float lineWidth;

    public Material drawingMaterial;

    //public Shader drawingShader;

    public Color drawingColor;

    public LineTextureMode textureMode = LineTextureMode.Tile;


    private LineRenderer currentDrawing;
    private int index;


    public InputActionProperty drawInput;


    private void Start()
    {
        if(lineWidthSlider != null)
        {
            lineWidthSlider.minValue = 0.005f;
            lineWidthSlider.maxValue = 0.1f;
            lineWidthSlider.value = lineWidthSlider.maxValue / 4;
        }

        drawingColor = Color.white;
        
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

        GenerateMeshCollider();
    }

    public void GenerateMeshCollider()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("line");

        GameObject.FindGameObjectsWithTag("line");

        if (objectsWithTag.Length > 0)
        {
            // Iterate through the array of objects
            foreach (GameObject obj in objectsWithTag)
            {
                MeshCollider collider = obj.GetComponent<MeshCollider>();
                LineRenderer lineRenderer = obj.GetComponent<LineRenderer>();

                if (collider == null)
                {
                    collider = currentDrawing.gameObject.AddComponent<MeshCollider>();
                }

                Mesh mesh = new Mesh();

                lineRenderer.BakeMesh(mesh);

                collider.sharedMesh = mesh;
                collider.convex = true;
                collider.isTrigger = true;
            }
        }
    }

    public void Draw()
    {
        if(lineWidthSlider != null)
        lineWidth = lineWidthSlider.value;

        if (currentDrawing == null)
        {

            index = 0;

            //Vi laver en ny instance af drawingMaterial fordi vi ellers ikke kan ændre farven på materialet, da det vil ændre farven på alle streger.
            Material material = Instantiate(drawingMaterial); 
            material.color = drawingColor;

            currentDrawing = new GameObject().AddComponent<LineRenderer>();
            currentDrawing.material = material;
            //currentDrawing.numCornerVertices = 90;
            currentDrawing.numCapVertices = 90;
            //currentDrawing.textureScale = new Vector2(100, 200);
            currentDrawing.textureMode = textureMode;
            currentDrawing.startColor = drawingColor;
            currentDrawing.endColor = Color.white;
            currentDrawing.startWidth = lineWidth;
            currentDrawing.positionCount = 1;
            currentDrawing.SetPosition(0, lineOrigin.transform.position);
            currentDrawing.tag = "line";
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

    // This method is called when any button is clicked
    public void ChangeBrushColor(Button clickedButton)
    {
        // Get the Image component of the clicked button
        Image buttonImage = clickedButton.GetComponent<Image>();

        if (buttonImage != null)
        {
            // Get the color of the clicked button
            Color buttonColor = buttonImage.color;

            // Assign the color to the target material
            drawingColor = buttonColor;

            // Now 'targetMaterial' should have the color of the clicked button
            Debug.Log("Color of the clicked button: " + buttonColor);
        }
        else
        {
            Debug.LogWarning("No Image component found on the clicked button.");
        }
    }    

    public void ChangeBrush()
    {

    }
}

