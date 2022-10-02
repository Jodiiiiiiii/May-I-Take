using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // constants
    private const float Y_CENTER = 1.5f;

    // variables
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private string[] characters;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float maxSpawnInterval;
    [SerializeField] private float ySpawnRange;
    [SerializeField] private float tileSpeed;

    float spawnTimer;
    float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0;
        spawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if(!PatienceMeter.OutOfPatience())
            spawnTimer += Time.deltaTime;

        if(spawnTimer > spawnTime)
        {
            // reset new goal time for next spawn
            spawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            spawnTimer = 0;

            // create new tile, and initialize its values
            GameObject tile = Instantiate(tilePrefab);
            int index = Random.Range(0, characters.Length);
            tile.GetComponent<TileController>().init(tileSpeed, Random.Range(Y_CENTER -ySpawnRange, Y_CENTER + ySpawnRange), characters[index], sprites[index]);
        }
    }
}
