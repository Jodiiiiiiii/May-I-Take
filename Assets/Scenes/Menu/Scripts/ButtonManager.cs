using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void LoadBreakfast()
    {
        GameManager.instance.LoadBreakfast();
    }

    public void LoadLunch()
    {
        GameManager.instance.LoadLunch();
    }

    public void LoadDinner()
    {
        GameManager.instance.LoadDinner();
    }

    public void LoadHowTo()
    {
        GameManager.instance.LoadHowTo();
    }

    public void ToggleUnlimited()
    {
        GameManager.instance.ToggleUnlimited();
    }
}
