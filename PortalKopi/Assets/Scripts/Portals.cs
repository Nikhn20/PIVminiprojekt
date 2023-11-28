using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using RenderPipeline = UnityEngine.Rendering.RenderPipelineManager;

public class Portals : MonoBehaviour


{
    //Reference the portal objects and PortalGun script
    public GameObject PortalA;
    public GameObject PortalB;
    public PortalGun scriptPortal;

    //Boolean to check if the object has teleported to prevent continous teleportation
    private bool objHasTeleported = false;
    private void Start()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Checks if the object has not teleported
        if (!objHasTeleported)
        {
            //Check if the trigger is "PorA" portal
            if (other.CompareTag("PorA"))
            {
                //Sets the boolean to true
                objHasTeleported = true;
                
                //Get the rotation of the portal
                Quaternion portalRotation = PortalB.transform.rotation;
                
                //Teleports the object based on the rotation of Portal B
                if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, -90f)))
                {
                    transform.position = PortalB.transform.position + new Vector3(4, 0, 0);
                }
                else if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, 90f)))
                {
                    transform.position = PortalB.transform.position + new Vector3(-4, 0, 0);
                }
                else if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, 0f)))
                {
                    transform.position = PortalB.transform.position + new Vector3(0, 0, 4);
                }
                else if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, 180f)))
                {
                    transform.position = PortalB.transform.position + new Vector3(0, 0, -4);
                }
                else if (PortalB.transform.position.y > transform.position.y)
                {
                    transform.position = PortalB.transform.position + new Vector3(0, -4f, 0);
                }
                else
                {
                    transform.position = PortalB.transform.position + new Vector3(0, 4f, 0);
                }

            }
            //Same as above just with this being about checking for PorB and teleporting the object to PortalA
            else if (other.CompareTag("PorB"))
            {
                
                objHasTeleported = true;
                Quaternion portalRotation = PortalA.transform.rotation;
                if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, -90f)))
                {
                    transform.position = PortalA.transform.position + new Vector3(4, 0, 0);
                }
                else if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, 90f)))
                {
                    transform.position = PortalA.transform.position + new Vector3(-4, 0, 0);
                }
                else if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, 0f)))
                {
                    transform.position = PortalA.transform.position + new Vector3(0, 0, 4);
                }
                else if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, 180f)))
                {
                    transform.position = PortalA.transform.position + new Vector3(0, 0, -4);
                }
                else if (PortalA.transform.position.y > transform.position.y)
                {
                    transform.position = PortalA.transform.position + new Vector3(0, -4f, 0);
                }
                else
                {
                    transform.position = PortalA.transform.position + new Vector3(0, 4f, 0);
                }
            }
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PorA") || other.CompareTag("PorB"))
        {
            //Resets the boolean when the object exits a portal
            objHasTeleported = false;
        }
    }

    //Helper function to check if two rotations are similar within a threshold
    private bool IsSimilarRotation(Quaternion a, Quaternion b, float threshold = 0.01f)
    {
        return Quaternion.Angle(a, b) < threshold;
    }

    private void Update()
    {
        //Finding the instatiated portals using the portalgun script
        PortalB = FindObjectOfType<PortalGun>().PortalB;
        PortalA = FindObjectOfType<PortalGun>().PortalA; 
    }
}
