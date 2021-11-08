using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Objects")]

    public GameObject[] obstacles;

    [Header("Min-Max")]
    public float minSpawnY;
    public float maxSpawnY;

    [Header("Spawn")]
    private float leftSpawnX;
    private float rightSpawnX;
    public float spawnRate;
    private float lastSpawn;


    //private List<GameObject> pooledObstacles = new List<GameObject>();
    //private int initialPoolSize = 20;

    void Start()
    {
        //Choosing spawn left or right by cam

        Camera cam = Camera.main;
        float camWidth = (2.0f * cam.orthographicSize) * cam.aspect;
        leftSpawnX = -camWidth / 2;
        rightSpawnX = camWidth / 2;

        //initial pool
        
        //CreateInitialPool();
    }
    void Update()
    {

        if (Time.time - spawnRate >= lastSpawn)
        {
            lastSpawn = Time.time;
            SpawnObstacle();
        }
    }
   /* void CreateInitialPool()
    {
        for (int index = 0; index < initialPoolSize; index++)
        {
            GameObject obstacleToSpawn = obstacles[index % 4]; //determing which   
            GameObject spawnedObject = Instantiate(obstacleToSpawn);
            //POOLING
            pooledObstacles.Add(spawnedObject); // add it to the pool
            spawnedObject.SetActive(false); // stop it,spawn it later
        } 
    }*/
    void SpawnObstacle()
    {
        //obj
        GameObject obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Length)], GetSpawnPosition(), Quaternion.identity);
        //pos
        obstacle.transform.position = GetSpawnPosition();
        // direction
        obstacle.GetComponent<Obstacle>().moveDir = new Vector3(obstacle.transform.position.x > 0 ? -1 : 1, 0, 0);
    }

    /*GameObject GetPooledObstacle()
    {
        GameObject pooledObj = null;
        foreach (GameObject obj in pooledObstacles)  //looking for each inactive object in pool
        {
            if (!obj.activeInHierarchy)
                pooledObj = obj;
        }
        pooledObj.SetActive(true);  // then setting it active
        return pooledObj;
    }*/

    Vector3 GetSpawnPosition()
    {
        float x = Random.Range(0, 2) == 1 ? leftSpawnX : rightSpawnX;
        float y = Random.Range(minSpawnY, maxSpawnY);
        return new Vector3(x, y, 0);
    }


}
