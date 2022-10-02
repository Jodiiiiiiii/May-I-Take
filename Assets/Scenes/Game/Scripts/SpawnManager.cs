using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // variables
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float maxSpawnInterval;
    [SerializeField] private float ySpawnRange;
    [SerializeField] private float tileSpeed;

    float spawnTimer;
    float spawnTime;
    TileController currentTile;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0;
        spawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if(spawnTimer > spawnTime)
        {
            // reset new goal time for next spawn
            spawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            spawnTimer = 0;

            // create new tile, and initialize its values
            GameObject tile = Instantiate(tilePrefab);
            tile.GetComponent<TileController>().init(tileSpeed, Random.Range(-ySpawnRange, ySpawnRange), "a");
        }
    }
}
