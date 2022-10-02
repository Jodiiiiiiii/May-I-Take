using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{

    // constants
    private const float CAMERA_WIDTH = 9.5f;

    // variables
    private float xSpeed;
    private float initY;
    private string character;
    // y drift speed

    // constructor for spawn manager to use
    public void init(float _xSpeed, float _initY, string _character)
    {
        xSpeed = _xSpeed;
        initY = _initY;
        character = _character;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-CAMERA_WIDTH, initY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // translate horizontally
        transform.position += Vector3.right * xSpeed * Time.deltaTime;

        // destroy tile if its key is pressed
        if (Input.GetKeyDown(character))
        {
            Destroy(gameObject);
        }

        // destory object when leaving frame
        if(transform.position.x > CAMERA_WIDTH)
        {
            // decrement patience meter here
            Destroy(gameObject);
        }
    }
}
