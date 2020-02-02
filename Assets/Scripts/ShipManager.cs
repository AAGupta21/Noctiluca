using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [SerializeField] private GameObject ActivateIdlePlayer = null;

    [SerializeField] private float RockingPower = 0.01f;

    private bool IsRocking = false;
    private float OriYPos = 0f;

    private void Start()
    {
        OriYPos = gameObject.transform.position.y;
    }

    public void DisablePlayerObject()
    {
        ActivateIdlePlayer.SetActive(false);
    }
    
    public void EnablePlayerObject()
    {
        ActivateIdlePlayer.SetActive(true);
    }

    public void SetPosforSurface()
    {
        transform.position = transform.GetChild(1).transform.localPosition;
        transform.rotation = transform.GetChild(1).transform.localRotation;
    }

    public void SetPosforUnderWater()
    {
        StopCoroutine("RockingEffect");

        transform.position = transform.GetChild(2).transform.localPosition;
        transform.rotation = transform.GetChild(2).transform.localRotation;

        IsRocking = false;
    }

    private void Update()
    {
        if(!IsRocking && !Manager.InWater)
        {
            IsRocking = true;
            StartCoroutine(RockingEffect(Random.Range(0.2f, 0.4f), Random.Range(0.1f, 0.5f), Random.Range(-1f, 1f)));
        }
    }

    private IEnumerator RockingEffect(float rocking_Duration, float delay, float direc)
    {
        float f = 0f;
        float dir = 0f;

        if(direc < 0f)
        {
            dir = -1f;
        }
        else
        {
            dir = 1f;
        }
        
        while(f < rocking_Duration)
        {
            yield return new WaitForSeconds(0.01f);
            gameObject.transform.position += Vector3.up * dir * RockingPower * Time.deltaTime;
            f += 0.01f;
        }

        dir *= -1f;

        while(f > 0f)
        {
            yield return new WaitForSeconds(0.01f);
            gameObject.transform.position += Vector3.up * dir * RockingPower * Time.deltaTime;
            f -= 0.01f;
        }

        //Vector3 pos = gameObject.transform.position;
        //pos.y = OriYPos;
        //gameObject.transform.position = pos;

        yield return new WaitForSeconds(delay);
        IsRocking = false;
    }
}