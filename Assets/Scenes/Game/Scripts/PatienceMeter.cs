using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatienceMeter : MonoBehaviour
{
    // constants
    private const float METER_LENGTH = 14.361f;
    private const int MAX_PATIENCE = 30;
    private const float CLOCK_SPEED = 5;

    // variables
    private static int patience;

    // game objects
    [SerializeField] private GameObject clock;
    [SerializeField] private GameObject grayBox;

    // Start is called before the first frame update
    void Start()
    {
        patience = MAX_PATIENCE;
    }

    // Update is called once per frame
    void Update()
    {
        // set to end position when patience is up
        if (clock.transform.localPosition.x >= METER_LENGTH)
        {
            clock.transform.localPosition = new Vector3(METER_LENGTH, clock.transform.localPosition.y, 0);
        }
        // slide clock to current patience level at clock speed
        else if (clock.transform.localPosition.x < ((float)(MAX_PATIENCE - patience) / MAX_PATIENCE) * METER_LENGTH)
        {
            clock.transform.localPosition += Vector3.right * CLOCK_SPEED * Time.deltaTime;
        }

        // set gray bar to fill region behind clock's current position
        grayBox.transform.localPosition = new Vector3(clock.transform.localPosition.x / 2, 0, 0);
        grayBox.transform.localScale = new Vector3(clock.transform.localPosition.x, grayBox.transform.localScale.y, 1);
    }

    public static void DecrementPatience()
    {
        patience--;
    }

    public static bool OutOfPatience()
    {
        return patience <= 0;
    }
}
