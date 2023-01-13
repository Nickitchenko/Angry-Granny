using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnDelay;

    public GameObject enemyPrefab;

    public Transform spawnPoint;

    public Vector3 volume;

    private void Start()
    {
        StartCoroutine("Spawner");
    }

    IEnumerator Spawner()
    {
        int spawnCount=100;
        GameObject parent = new();
        while(spawnCount>0)
        {
            spawnCount--;
            Vector3 pos = new Vector3(Random.Range(spawnPoint.position.x - volume.x,
                spawnPoint.position.x+volume.x), spawnPoint.position.y, 
                Random.Range(spawnPoint.position.z-volume.z, spawnPoint.position.z+volume.z));
            GameObject spawnObj = Instantiate(enemyPrefab, pos, Quaternion.identity, parent.transform); 
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
