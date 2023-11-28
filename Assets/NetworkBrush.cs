using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetworkBrush : MonoBehaviour
{
    //// Prefab to instantiate when we draw a new brush stroke
    //[SerializeField] private GameObject _brushStrokePrefab = null;

    //public InputActionProperty drawInput;

    //public GameObject origin;

    //private BrushStroke _activeBrushStroke;

    //private void Update()
    //{
    //    // Get the position & rotation of the point from which we draw
    //    Vector3 originPos = origin.transform.position;
    //    Quaternion originRot = origin.transform.rotation;

    //    // Figure out if the trigger is pressed or not
    //    bool drawTriggerPressed = drawInput.action.ReadValue<float>() > 0f;

    //    // If the trigger is pressed and we haven't created a new brush stroke to draw, create one!
    //    if (drawTriggerPressed && _activeBrushStroke == null)
    //    {
    //        // Instantiate a copy of the Brush Stroke prefab.
    //        GameObject brushStrokeGameObject = Instantiate(_brushStrokePrefab);

    //        // Grab the BrushStroke component from it
    //        _activeBrushStroke = brushStrokeGameObject.GetComponent<BrushStroke>();

    //        // Tell the BrushStroke to begin drawing at the current brush position
    //        _activeBrushStroke.BeginBrushStrokeWithBrushTipPoint(originPos, originRot);
    //    }

    //    // If the trigger is pressed, and we have a brush stroke, move the brush stroke to the new brush tip position
    //    if (drawTriggerPressed)
    //        _activeBrushStroke.MoveBrushTipToPoint(originPos, originRot);

    //    // If the trigger is no longer pressed, and we still have an active brush stroke, mark it as finished and clear it.
    //    if (!drawTriggerPressed && _activeBrushStroke != null)
    //    {
    //        _activeBrushStroke.EndBrushStrokeWithBrushTipPoint(originPos, originRot);
    //        _activeBrushStroke = null;
    //    }
    //}
}
