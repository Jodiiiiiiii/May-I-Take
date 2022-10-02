using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlphaOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // constants
    private const float ALPHA_ON = 0.4f;

    // unity variables
    private Image image;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }

    void Update()
    {
        if((name == "Breakfast Button" && !GameManager.instance.GetUnlimited())
            || (name == "Breakfast Button" && GameManager.instance.GetUnlimited() && GameManager.instance.GetBreakfastClear())
            || (name == "Lunch Button" && !GameManager.instance.GetUnlimited() && GameManager.instance.GetBreakfastClear())
            || (name == "Lunch Button" && GameManager.instance.GetUnlimited() && GameManager.instance.GetLunchClear())
            || (name == "Dinner Button" && !GameManager.instance.GetUnlimited() && GameManager.instance.GetLunchClear())
            || (name == "Dinner Button" && GameManager.instance.GetUnlimited() && GameManager.instance.GetDinnerClear())
            || (name == "HowTo Button")
            || (name == "Infinity Button"))
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(button.interactable)
            image.color = new Color(image.color.r, image.color.g, image.color.b, ALPHA_ON);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }
}
