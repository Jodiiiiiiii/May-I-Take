using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // variables
    private Animator camAnimator;

    public void Start()
    {
        camAnimator = GetComponent<Animator>();
    }

    public void ShakeCamera()
    {
        camAnimator.SetTrigger("shake");
    }
}
