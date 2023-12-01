using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BrushStroke : NetworkBehaviour {
    [SerializeField]
    private BrushStrokeMesh _mesh = null;

    // Ribbon State
    struct RibbonPoint 
    {
        public Vector3    position;
        public Quaternion rotation;
    }

    private List<RibbonPoint> _ribbonPoints = new List<RibbonPoint>();

    private NetworkVariable<Vector3> _brushTipPosition = new NetworkVariable<Vector3>();
    private NetworkVariable<Quaternion> _brushTipRotation = new NetworkVariable<Quaternion>();
    private NetworkVariable<bool> _brushStrokeFinalized = new NetworkVariable<bool>();

    // Smoothing
    private Vector3    _ribbonEndPosition;
    private Quaternion _ribbonEndRotation = Quaternion.identity;

    // Mesh
    private Vector3    _previousRibbonPointPosition;
    private Quaternion _previousRibbonPointRotation = Quaternion.identity;

    // Unity Events
    private void Update() {
        // Animate the end of the ribbon towards the brush tip
        AnimateLastRibbonPointTowardsBrushTipPosition();

        // Add a ribbon segment if the end of the ribbon has moved far enough
        AddRibbonPointIfNeeded();
    }

    // Interface
    public void BeginBrushStrokeWithBrushTipPoint(Vector3 position, Quaternion rotation) {
        // Update the model
        _brushTipPosition.Value = position;
        _brushTipRotation.Value = rotation;

        // Update last ribbon point to match brush tip position & rotation
        _ribbonEndPosition = position;
        _ribbonEndRotation = rotation;
        _mesh.UpdateLastRibbonPoint(_ribbonEndPosition, _ribbonEndRotation);
    }

    public void MoveBrushTipToPoint(Vector3 position, Quaternion rotation) {
        _brushTipPosition.Value = position;
        _brushTipRotation.Value = rotation;
    }

    public void EndBrushStrokeWithBrushTipPoint(Vector3 position, Quaternion rotation) {
        // Add a final ribbon point and mark the stroke as finalized
        AddRibbonPoint_ServerRpc(position, rotation);
        _brushStrokeFinalized.Value = true;
    }


    // Ribbon drawing
    private void AddRibbonPointIfNeeded() {
        // If the brush stroke is finalized, stop trying to add points to it.
        if (_brushStrokeFinalized.Value)
            return;

        if (Vector3.Distance(_ribbonEndPosition, _previousRibbonPointPosition) >= 0.01f ||
            Quaternion.Angle(_ribbonEndRotation, _previousRibbonPointRotation) >= 10.0f) {

            // Add ribbon point model to ribbon points array. This will fire the RibbonPointAdded event to update the mesh.
            AddRibbonPoint_ServerRpc(_ribbonEndPosition, _ribbonEndRotation);

            // Store the ribbon point position & rotation for the next time we do this calculation
            _previousRibbonPointPosition = _ribbonEndPosition;
            _previousRibbonPointRotation = _ribbonEndRotation;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void AddRibbonPoint_ServerRpc(Vector3 position, Quaternion rotation) {
        // Create the ribbon point
        RibbonPoint ribbonPoint = new RibbonPoint();
        ribbonPoint.position = position;
        ribbonPoint.rotation = rotation;
        _ribbonPoints.Add(ribbonPoint);

        // Update the mesh
        _mesh.InsertRibbonPoint(position, rotation);
    }

    


    // Brush tip + smoothing
    private void AnimateLastRibbonPointTowardsBrushTipPosition() {
        // If the brush stroke is finalized, skip the brush tip mesh, and stop animating the brush tip.
        if (_brushStrokeFinalized.Value) {
            _mesh.skipLastRibbonPoint = true;
            return;
        }

        Vector3    brushTipPosition = _brushTipPosition.Value;
        Quaternion brushTipRotation = _brushTipRotation.Value;

        // If the end of the ribbon has reached the brush tip position, we can bail early.
        if (Vector3.Distance(_ribbonEndPosition, brushTipPosition) <= 0.0001f &&
            Quaternion.Angle(_ribbonEndRotation, brushTipRotation) <= 0.01f) {
            return;
        }

        // Move the end of the ribbon towards the brush tip position
        _ribbonEndPosition =     Vector3.Lerp(_ribbonEndPosition, brushTipPosition, 25.0f * Time.deltaTime);
        _ribbonEndRotation = Quaternion.Slerp(_ribbonEndRotation, brushTipRotation, 25.0f * Time.deltaTime);

        // Update the end of the ribbon mesh
        _mesh.UpdateLastRibbonPoint(_ribbonEndPosition, _ribbonEndRotation);
    }
}
