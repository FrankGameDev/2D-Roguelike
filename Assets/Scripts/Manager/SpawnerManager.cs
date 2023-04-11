using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    private static SpawnerManager _instance;

    public GameObject[] spawnersObj;
    private List<Spawner> spawners;

    public float spawnTimer;
    private float _maxSpawnTimer;


    public static SpawnerManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    private void Start()
    {
        _maxSpawnTimer = spawnTimer;
        spawners = new List<Spawner>(spawnersObj.ToList().Select(elem => elem.GetComponent<Spawner>()));
    }
    private void Update()
    {
        SpawnerHandler();
    }

    private void SpawnerHandler()
    {
        int randomSpawner = UnityEngine.Random.Range(0, spawners.Count);
        if (spawnTimer <= 0)
        {
            spawners[randomSpawner].SpawnNewEnemy();
            spawnTimer = _maxSpawnTimer;
        }
        else
            spawnTimer -= Time.deltaTime;
    }

}
