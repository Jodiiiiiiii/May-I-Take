using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // constants
    private const float Y_CENTER = 1.3f;
    private const float STAGE_TIME = 30f;

    // variables
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private string[] characters;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float maxSpawnInterval;
    [SerializeField] private float ySpawnRange;
    [SerializeField] private float tileSpeed;
    [SerializeField] private float verticalBoundary;

    float spawnTimer;
    float spawnTime;
    float totalTime;
    int stage;

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
        totalTime = 0;
        stage = 0;

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
            totalTime += Time.deltaTime;
            // iterate stages
            if(stage == 0 && totalTime > STAGE_TIME)
            {
                stage++;
                Accelerate();
            }
            else if (stage == 1 && totalTime > 2 * STAGE_TIME)
            {
                stage++;
                Accelerate();
            }
            else if (stage == 2 && totalTime > 3 * STAGE_TIME)
            {
                stage++;
                Accelerate();
            }
            else if (stage == 3 && totalTime > 4 * STAGE_TIME)
            {
                stage++; // stage == 4 indicates success on non-infinite level
            }

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
                    if ((character.Length==1) ? Input.GetKeyDown(character) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) : Input.GetKeyDown(character[1].ToString()) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
                    {
                        misinput = true; // set to true for now, should be false after next for loop if input was valid
                        foreach (Transform child in transform) // compare pressed key to tiles to see if one should be deleted
                        {
                            // destroy if character matches
                            if (child.position.x > verticalBoundary && character == child.GetComponent<TileController>().GetCharacter())
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

    private void Accelerate()
    {
        // update stats of future tiles
        minSpawnInterval *= 0.8f;
        maxSpawnInterval *= 0.8f;
        tileSpeed *= 1.2f;

        // accelerate existing tiles as well
        foreach(Transform child in transform)
        {
            child.GetComponent<TileController>().Accelerate();
        }
    }

    public int GetStage()
    {
        return stage;
    }

    public float GetTotalTime()
    {
        return totalTime;
    }

}
