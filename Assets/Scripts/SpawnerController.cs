using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform enemyParent; 
    [SerializeField] Transform powerupParent; 
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject powerupPrefab;
    [SerializeField] PlayerController playerController;

    // Initialize components
    void Start()
    {
        playerController = player.gameObject.GetComponent<PlayerController>();
        
        InvokeRepeating("SpawnEnemy", 2f, 2f);
        InvokeRepeating("SpawnPowerup", 5f, 3f);
    }

    // Do these all the time
    void Update()
    {

    }

    void SpawnEnemy()
    {
        // Generate a spawn position
        float x = Random.Range(-12f, 12f);
        float z = Random.Range(10f, 18f);
        Vector3 spawnPosition = new Vector3(x, 1f, z);

        // Instantiate the prefab move the enemy by it's normalized velocity
        if (!playerController.dead)
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, enemyParent);
    }

    void SpawnPowerup()
    {
        // Generate a spawn position
        float x = Random.Range(-12f, 12f);
        float z = Random.Range(10f, 18f);
        Vector3 spawnPosition = new Vector3(x, 1f, z);

        // Instantiate the prefab move the enemy by it's normalized velocity
        if (!playerController.dead)
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, enemyParent);
    }
}
