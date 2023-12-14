using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHeadMover : MonoBehaviour
{

    public Transform startPoint;
    public Transform endPoint;

    public void MovePlayHead(double playedFraction)
    {
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, (float)playedFraction);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
