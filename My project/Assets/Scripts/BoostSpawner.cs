using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BoostSpawner : MonoBehaviour
{
    [Header("Boosts")]
    public GameObject[] boosts; //list of boosters
    private int boostsCount=0; //boosters count on map
    public int boostsMaxCount;

    [Header("Spawn boosts")]
    public Transform spawnPoint;
    public Vector3 volume; //range of spawn
    private GameObject parent;

    private void Start()
    {
        InvokeRepeating("SpawnBooster", 0f, 0.5f);
        parent = new();
    }

    private void SpawnBooster()
    {
        if(boostsCount<boostsMaxCount)
        {
            GameObject spawnObj = Instantiate(boosts[Random.Range(0, boosts.Length)], randomPoint(), Quaternion.identity, parent.transform);
            boostsCount++;
        }
    }

    private Vector3 randomPoint()
    {
        Vector3 pos = new Vector3(Random.Range(spawnPoint.position.x - volume.x,
                spawnPoint.position.x + volume.x), 1, Random.Range(spawnPoint.position.z - volume.z,
                spawnPoint.position.z + volume.z));
        return pos;
    }
}
