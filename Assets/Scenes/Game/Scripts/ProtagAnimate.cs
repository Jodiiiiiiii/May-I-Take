using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagAnimate : MonoBehaviour
{

    // unity variables
    private Animator animator;
    private SpawnManager spawnManager;

    // variables
    private int currentLandmark;

    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        currentLandmark = 0;

        // set initial rand value
        animator.SetInteger("rand", Random.Range(1, 5)); // pick random animation 1, 2, 3, or 4
    }

    // Update is called once per frame
    void Update()
    {
        // only set to 4 if 
        if (spawnManager.GetStage() != 4 || !GameManager.instance.GetUnlimited())
            animator.SetInteger("stage", spawnManager.GetStage());

        if(spawnManager.GetTotalTime() - currentLandmark > 10f)
        {
            currentLandmark += 10;
            animator.SetInteger("rand", Random.Range(1, 5)); // pick random animation 1, 2, 3, or 4
        }
    }
}
