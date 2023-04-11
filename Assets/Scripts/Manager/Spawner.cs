using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{

    public List<GameObject> pooledObjects;
    public GameObject objToPool;
    public int poolAmount;

    
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

    }

    public void SpawnNewEnemy()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].transform.position = transform.position;
                pooledObjects[i].transform.rotation = transform.rotation;
                pooledObjects[i].SetActive(true);
                pooledObjects[i].GetComponent<Collider2D>().enabled = true;
                return;
            }
        }
    }

}
