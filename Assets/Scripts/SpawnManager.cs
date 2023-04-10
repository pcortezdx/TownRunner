using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs = new GameObject[2];

    private Vector3 spawnPos = new Vector3(26, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 1.8f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Save a reference to the Player controller script to access the gameover value
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        repeatRate = Random.Range(1.5f, 2.5f);
        startDelay = Random.Range(1.8f, 2.5f);
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    private void SpawnObstacle()
    {
        // Keep spawning objects until the GameOver flag is true        
        if (!playerControllerScript.gameOver)
        {
            //Create a random obstacle from the Prefab array
            int index = Random.Range(0, obstaclePrefabs.Length);
            GameObject obstacle = obstaclePrefabs[index];

            Instantiate(obstacle, spawnPos, obstacle.transform.rotation);
        }            
    }
    
}
