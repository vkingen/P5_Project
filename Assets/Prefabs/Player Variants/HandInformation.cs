using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInformation : MonoBehaviour
{
    public enum HandModel { Left, Right}

    public HandModel handModel;
    public Transform[] fingersBones;

    public Transform root;
    public Animator animator;


    //private void Awake()
    //{
    //    animator = GetComponent<Animator>();
    //    root = GetComponent<Transform>();
    //}
}
