using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScriptGoal : MonoBehaviour
{
    public Animator runwayAnimation;

    public Animator wayAnimation;

    private bool hasCollided = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!hasCollided && other.CompareTag("Objective"))
        {
            hasCollided = true;
            
            if (runwayAnimation != null)
            {
                runwayAnimation.SetTrigger("TriggerAni");
            }
        }
        
        if (other.CompareTag("Objectivefirst"))
        {
            if (wayAnimation != null)
            {
                wayAnimation.SetTrigger("WayTrigger");
            }
        }
    }

    private void OnTriggerExit(Collider context)
    {
        if (context.CompareTag("Ground"))
        {
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
