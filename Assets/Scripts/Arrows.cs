using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    public Manager manager = null;

    [SerializeField] private GameObject CompassObject = null;
    [SerializeField] private GameObject Collectables = null;

    private float Dist = 0f;

    private void Start()
    {
        Dist = Vector3.Distance(CompassObject.transform.position, transform.position);
    }

    private void Update()
    {
        if(Manager.InWater)
        {
            Vector3 dir = (Collectables.transform.GetChild(manager.Object_Cnt + 1).transform.position - CompassObject.transform.position);
            
            gameObject.transform.localPosition =  dir * Dist;
        }
    }
}
