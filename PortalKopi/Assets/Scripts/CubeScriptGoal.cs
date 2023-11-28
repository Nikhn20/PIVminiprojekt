using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScriptGoal : MonoBehaviour
{ 
    //Reference to the animator RunwayAnimation
    public Animator runwayAnimation;

    //Reference
    public Animator wayAnimation;

    //Booleans to check if the cube has collided with the objectives
    private bool hasCollided = false;
    private bool hasCollidedfirst = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Check if the cube has not collided and the trigger is an "Objective"
        if (!hasCollided && other.CompareTag("Objective"))
        {
            hasCollided = true;
            
            //Trigger the runway animation if it is assigned
            if (runwayAnimation != null)
            {
                runwayAnimation.SetTrigger("TriggerAni");
            }
        }
        //Check if the cube has not collided and the trigger is an "Objectivefirst"
        if (other.CompareTag("Objectivefirst"))
        {
            if (!hasCollidedfirst && other.CompareTag("Objective"))
            {
                hasCollidedfirst = true;
                
                //Trigger the way animation if it assigned
                if (wayAnimation != null)
                {
                    wayAnimation.SetTrigger("WayTrigger");
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
