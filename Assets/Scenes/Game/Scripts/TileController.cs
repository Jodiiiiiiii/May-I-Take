using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{

    // constants
    private const float CAMERA_WIDTH = 9.5f;
    private const float MIN_WOBBLE_HEIGHT = 0.3f;
    private const float MAX_WOBBLE_HEIGHT = 0.5f;
    private const float MAX_WOBBLE_SPEED = 0.5f;
    private const float DESTROY_TIME = 0.1f;

    // variables
    private float xSpeed;
    private float initY;
    private string character;
    private float yWobbleSpeed;
    private float wobbleHeight;
    private bool wobbleUp;
    private bool destroying;
    private float destroyTimer;

    // unity variables
    private SpawnManager spawnManager;
    private Animator tileAnimator;

    // constructor for spawn manager to use
    public void init(float _xSpeed, float _initY, string _character, Sprite _sprite)
    {
        // variables
        xSpeed = _xSpeed;
        initY = _initY;
        character = _character;
        GetComponent<SpriteRenderer>().sprite = _sprite;
        yWobbleSpeed = Random.Range(0, MAX_WOBBLE_SPEED);
        wobbleHeight = Random.Range(MIN_WOBBLE_HEIGHT, MAX_WOBBLE_HEIGHT);
        wobbleUp = Random.Range(0, 2) == 1; // randomly assigns true or false
        destroying = false;
        destroyTimer = 0;

        // unity variables
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        tileAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-CAMERA_WIDTH, initY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (destroying)
        {
            // only destroy after delay (allows pop animation to play)
            if(destroyTimer > DESTROY_TIME)
            {
                Destroy(gameObject);
            }

            destroyTimer += Time.deltaTime;
        }
        else // only do stuff for tile when it is not being destroyed (on delay)
        {
            // translate vertically (wobble)
            if (wobbleUp)
            {
                // wobbles up
                transform.position += Vector3.up * yWobbleSpeed * Time.deltaTime;

                // flip wobbleUp at max y
                if (transform.position.y > initY + wobbleHeight)
                    wobbleUp = false;
            }
            else
            {
                // wobbles down
                transform.position -= Vector3.up * yWobbleSpeed * Time.deltaTime;

                // flip wobbleUp at min Y
                if (transform.position.y < initY - wobbleHeight)
                    wobbleUp = true;
            }

            // only conduct these operations if not out of patience
            if (!PatienceMeter.OutOfPatience())
            {
                // translate horizontally
                transform.position += Vector3.right * xSpeed * Time.deltaTime;

                // destroy tile if its key is pressed
                if (Input.GetKeyDown(character))
                {
                    // invoke shake animation and pop sound
                    GameObject.Find("Main Camera").GetComponent<CameraController>().ShakeCamera();
                    spawnManager.PlayPopSound();
                    tileAnimator.SetTrigger("pop");
                    destroying = true;
                }

                // destory object when leaving frame
                if (transform.position.x > CAMERA_WIDTH)
                {
                    // decrement patience meter, then destroy tile
                    PatienceMeter.DecrementPatience();
                    Destroy(gameObject);
                }
            }
        }
        
    }

}
