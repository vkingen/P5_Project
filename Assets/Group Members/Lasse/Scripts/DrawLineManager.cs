using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DrawLineManager : MonoBehaviour
{
    public GameObject lineOrigin;

    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject rightSphere;

    [SerializeField] private GameObject leftHand;
    //[SerializeField] private GameObject leftSphere;

    [SerializeField] private MeshRenderer leftSphereMesh;

    [SerializeField] private Slider lineWidthSlider;

    [SerializeField] private float lineWidth;

    [SerializeField] private GameObject brushUIPanel;

    private bool allowDraw = false;

    public Material drawingMaterial;

    //public Shader drawingShader;

    public Color drawingColor;

    public LineTextureMode textureMode = LineTextureMode.Tile;


    private LineRenderer currentDrawing;
    private int index;


    public InputActionProperty drawInput;


    private void Start()
    {
        //leftSphereMesh = leftSphere.GetComponent<MeshRenderer>();

        if(lineWidthSlider != null)
        {
            lineWidthSlider.minValue = 0.005f;
            lineWidthSlider.maxValue = 0.1f;
            lineWidthSlider.value = lineWidthSlider.maxValue / 4;
        }
        drawingColor = Color.white;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DrawTrigger"))
            allowDraw = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("DrawTrigger"))
            allowDraw = false;
    }

    private void Update()
    {
        if (allowDraw)
        {
            leftHand.SetActive(false);
            leftSphereMesh.enabled = true;

            rightHand.SetActive(false);

            rightSphere.SetActive(true);

            brushUIPanel.SetActive(true);

            if (drawInput.action.ReadValue<float>() > 0f)
            {
                Draw();
                //GenerateMeshCollider();
            }
            else if (currentDrawing != null)
            {
                currentDrawing = null;

            }            
        }
        else
        {
            rightHand.SetActive(true);
            rightSphere.SetActive(false);

            leftHand.SetActive(true);
            leftSphereMesh.enabled = false;

            brushUIPanel.SetActive(false);
        }
    }

    public void GenerateMeshCollider()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("line");

        //GameObject.FindGameObjectsWithTag("line");

        if (objectsWithTag.Length > 0)
        {
            // Iterate through the array of objects
            foreach (GameObject obj in objectsWithTag)
            {
                if (obj.CompareTag("line"))
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

                    //obj.AddComponent<MeshColliderGeneratedFlag>();
                }
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

            //Vi laver en ny instance af drawingMaterial fordi vi ellers ikke kan �ndre farven p� materialet, da det vil �ndre farven p� alle streger.
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

            MeshCollider collider = currentDrawing.GetComponent<MeshCollider>();
            LineRenderer lineRenderer = currentDrawing.GetComponent<LineRenderer>();

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

    public void ChangeBrushMaterial(Button clickedButton)
    {
        Image buttonImage = clickedButton.GetComponent<Image>();

        if (buttonImage != null)
        {
            Material material = buttonImage.material;

            drawingMaterial = material;

            textureMode = LineTextureMode.Tile;

            if(drawingMaterial.name == "HeartBrush")
            {
                textureMode = LineTextureMode.Stretch;
            }
        }
    }
}

