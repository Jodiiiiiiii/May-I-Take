using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MealIndicator : MonoBehaviour
{
    //unity variables
    [SerializeField] private Sprite yes;
    [SerializeField] private Sprite no;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // set image based on whether it is unlimited
        if ((name == "BreakfastIndicator" && GameManager.instance.GetBreakfastClear())
            || (name == "LunchIndicator" && GameManager.instance.GetLunchClear())
            || (name == "DinnerIndicator" && GameManager.instance. GetDinnerClear()))
        {
            image.sprite = yes;
        }
        else
        {
            image.sprite = no;
        }
    }
}
