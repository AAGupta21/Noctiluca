using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharisFish : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;

    float OriX = 0f;

    private void Awake()
    {
        OriX = transform.position.x;
    }

    private void Update()
    {
        transform.position += Vector3.left * Speed * Time.deltaTime;

        if(Mathf.Abs(OriX - transform.position.x) > 1000f)
        {
            Destroy(gameObject);
        }
    }
}