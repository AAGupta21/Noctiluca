using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables_Hovering : MonoBehaviour
{
    [SerializeField] private float RotateSpeed = 10f;
    
    float Rot = 0f;
    
    private void Start()
    {
        Rot = transform.rotation.eulerAngles.y;
    }

    private void FixedUpdate()
    {
        Rot += Time.deltaTime * RotateSpeed;
        Quaternion rot = transform.rotation;
        rot.eulerAngles = Vector3.up * Rot;
        transform.rotation = rot;
    }
}