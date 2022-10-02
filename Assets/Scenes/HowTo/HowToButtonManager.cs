using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToButtonManager : MonoBehaviour
{
    public void Back()
    {
        GameManager.instance.LoadMenu();
    }
}
