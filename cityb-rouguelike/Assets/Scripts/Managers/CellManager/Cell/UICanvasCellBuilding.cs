using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasCellBuilding : MonoBehaviour
{
    public Transform cameraTransform;
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    
    void Update()
    {
        transform.rotation = cameraTransform.transform.rotation;
    }
}
