using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObject : MonoBehaviour
{
    internal enum RotateDirection
    {
        Right,
        Left,
    }

    public GameObject objectRotate;

    public float rotateSpeed = 50f;
    [SerializeField] RotateDirection rotateDirection = RotateDirection.Right;
    [SerializeField] bool shouldRotate = false;

    int rotateDirValue = 1;

    void Start()
    {
        if(rotateDirection == RotateDirection.Left)
            rotateDirValue = -rotateDirValue;
    }

    void Update()
    {
        if (shouldRotate)
            objectRotate.transform.Rotate(transform.up, rotateSpeed * Time.deltaTime * rotateDirValue);
    }
}