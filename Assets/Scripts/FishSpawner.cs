using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> FishList = null;

    [SerializeField] private Vector3 SpawnPoint_First = Vector3.zero;
    [SerializeField] private Vector3 SpawnPoint_Second = Vector3.zero;
    [SerializeField] private Vector3 SpawnPoint_Third = Vector3.zero;

    [SerializeField] private float SpawnYOffset = 50f;

    private void Start()
    {
        StartCoroutine(SpawnFish());
    }

    private IEnumerator SpawnFish()
    {
        yield return new WaitForSeconds(Random.Range(0.25f, 2.5f));

        int val = Random.Range(0, 3);

        switch(val)
        {
            case 0:
                {
                    Instantiate(FishList[0], SpawnPoint_First + Random.Range(-SpawnYOffset, SpawnYOffset) * Vector3.up, FishList[0].transform.rotation, transform);
                    break;
                }

            case 1:
                {
                    Instantiate(FishList[1], SpawnPoint_Second + Random.Range(-SpawnYOffset, SpawnYOffset) * Vector3.up, FishList[1].transform.rotation, transform);
                    break;
                }

            case 2:
                {
                    Instantiate(FishList[2], SpawnPoint_Third + Random.Range(-SpawnYOffset, SpawnYOffset) * Vector3.up, FishList[2].transform.rotation, transform);
                    break;
                }
        }

        StartCoroutine(SpawnFish());
    }
}