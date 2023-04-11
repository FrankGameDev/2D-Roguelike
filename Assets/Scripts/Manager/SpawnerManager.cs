using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager Instance;

    public List<GameObject> pooledObjects;
    public GameObject objToPool;
    public int poolAmount;

    public float spawnTimer;
    private float _maxSpawnTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(objToPool, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }


        _maxSpawnTimer = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnerHandler();
    }


    private void SpawnerHandler()
    {
        if (spawnTimer <= 0)
        {
            SpawnNewEnemy();
            spawnTimer = _maxSpawnTimer;
        }
        else
            spawnTimer -= Time.deltaTime;
    }


    private void SpawnNewEnemy()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].SetActive(true);
                pooledObjects[i].GetComponent<Collider2D>().enabled = true;
                return;
            }
        }
    }

}
