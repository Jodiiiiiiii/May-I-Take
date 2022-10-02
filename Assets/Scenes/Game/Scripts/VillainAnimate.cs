using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainAnimate : MonoBehaviour
{

    // unity variables
    private Animator animator;
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("stage", spawnManager.GetStage());
    }
}
