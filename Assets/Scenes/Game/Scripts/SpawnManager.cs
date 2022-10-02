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

    // audio
    [SerializeField] private AudioClip popSound;
    [SerializeField] private AudioClip misinputSound;
    [SerializeField] private AudioClip missSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0;
        spawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);

        // audio
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // only handle spawning and deleting if not out of patience
        if(!PatienceMeter.OutOfPatience())
        {
            spawnTimer += Time.deltaTime;

            // handle spawning new tiles
            if (spawnTimer > spawnTime)
            {
                // reset new goal time for next spawn
                spawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
                spawnTimer = 0;

                // create new tile, and initialize its values
                GameObject tile = Instantiate(tilePrefab, gameObject.transform);
                // get new index (that is NOT a duplicate)
                bool duplicate;
                int index;
                do
                {
                    duplicate = false;
                    index = Random.Range(0, characters.Length);
                    foreach (Transform child in transform)
                    {
                        if (child.GetComponent<TileController>().GetCharacter() == characters[index])
                        {
                            duplicate = true;
                        }
                    }
                } while (duplicate); // keep getting new index until it is a new index
                                     // initialize tile
                tile.GetComponent<TileController>().init(tileSpeed, Random.Range(Y_CENTER - ySpawnRange, Y_CENTER + ySpawnRange), characters[index], sprites[index]);
            }

            // handle deleting tiles
            bool misinput = false;
            if (Input.anyKeyDown) // if any key pressed
            {
                foreach (string character in characters) // check for a key press of each key in characters
                {
                    if (Input.GetKeyDown(character))
                    {
                        misinput = true; // set to true for now, should be false after next for loop if input was valid
                        foreach (Transform child in transform) // compare pressed key to tiles to see if one should be deleted
                        {
                            // destroy if character matches
                            if (character == child.GetComponent<TileController>().GetCharacter())
                            {
                                child.GetComponent<TileController>().Destroy();
                                misinput = false;
                                break; // only one child should have the character
                            }
                        }
                        if (misinput) // apply patience penalty, play sound, and shake camera
                        {
                            PatienceMeter.DecrementPatience();
                            PlayMisinputSound();
                            GameObject.Find("Main Camera").GetComponent<CameraController>().ShakeCamera();
                        }
                    }
                }
            }
        }
    }

    public void PlayPopSound()
    {
        audioSource.PlayOneShot(popSound);
    }

    public void PlayMissSound()
    {
        audioSource.PlayOneShot(missSound);
    }

    public void PlayMisinputSound()
    {
        audioSource.PlayOneShot(misinputSound);
    }
}
