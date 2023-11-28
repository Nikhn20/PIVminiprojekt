using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

// This script is responsible for handling portal creation, teleportation, and keeping track of portal jumps.
public class PortalGun : MonoBehaviour
{
    //References to portal game objects, and the prefabs
    public GameObject PortalA;
    public GameObject PortalB;
    public GameObject PortalAprefab;
    public GameObject PortalBprefab;

    //Bools to check if the portals have been instantiated
    private bool checkPortalA = false;
    private bool checkPortalB = false;

    //Reference to the first-person camera
    public Camera fpsCam;

    
    private bool PortalTeleB;
    private bool PortalTeleA;

    //Check if the player has entered the portal to prevent continous teleportation
    [SerializeField] public bool hasTeleported = false;

    //Reference a Text object
    public TextMeshProUGUI text;

    //Portal counter, counts how many times you have jumped in the portal
    private int portalCounter;

    // Start is called before the first frame update
    void Start()
    {
        //Initializing the counter and updating text
        portalCounter = 0;
        text.text = portalCounter + " Portals jumped";
    }

    //Firing portal A using Unity Input System
    public void OnPortalAFire(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            if (context.performed)
            {
                if (checkPortalA)
                {
                    if (PortalA != null)
                    {
                        //Destroying portal A if it exists
                        Destroy(PortalA);
                    }

                }
                //Creates a new Portal A at the hit point of the raycast
                PortalA = Instantiate(PortalAprefab);
                PortalA.transform.position = hit.point;

                if (fpsCam.transform.localRotation.x < 0.20f && fpsCam.transform.localRotation.x > -0.20f)
                {
                    //Adjusting for horizontal placement
                    Quaternion rotation = Quaternion.LookRotation(hit.normal);
                    rotation.eulerAngles = new Vector3(90f, rotation.eulerAngles.y, rotation.eulerAngles.z);
                    PortalA.transform.rotation = rotation;
                    
                    checkPortalA = true;

                }
                else
                {
                    //Adjusting for vertical placement
                    Quaternion rotation = Quaternion.LookRotation(hit.normal);
                    rotation.eulerAngles = new Vector3(0f, rotation.eulerAngles.y, rotation.eulerAngles.z);
                    PortalA.transform.rotation = rotation;
                    
                    checkPortalA = true;
                }



            }

        }
    }
//The same as above (Portal A) just for Portal B instead
    public void OnPortalBFire(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            if (context.performed)
            {
                if (checkPortalB)
                {
                    if (PortalB != null)
                    {
                        Destroy(PortalB);
                    }

                }

                PortalB = Instantiate(PortalBprefab);
                PortalB.transform.position = hit.point;

                if (fpsCam.transform.localRotation.x < 0.20f && fpsCam.transform.localRotation.x > -0.20f)
                {
                    Quaternion rotation = Quaternion.LookRotation(hit.normal);
                    rotation.eulerAngles = new Vector3(90f, Math.Abs(rotation.eulerAngles.y), Math.Abs(rotation.eulerAngles.z));
                    PortalB.transform.rotation = rotation;

                    checkPortalB = true;

                }
                else
                {
                    Quaternion rotation = Quaternion.LookRotation(hit.normal);
                    rotation.eulerAngles = new Vector3(0f, Math.Abs(rotation.eulerAngles.y), Math.Abs(rotation.eulerAngles.z));
                    PortalB.transform.rotation = rotation;
                    
                    checkPortalB = true;
                }



            }

        }

    }
//Called when the object enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (!hasTeleported)
        {
            if (other.CompareTag("PorA") && checkPortalB)
            {
                //Teleports the player based on Portal B's position and rotation and checks the portals rotation and adjusts the players position
                hasTeleported = true;
                Quaternion portalRotation = PortalB.transform.rotation;
                if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, -90f)))
                {
                    gameObject.transform.position = PortalB.transform.position+new Vector3(1,0,0);
                }
                else if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, 90f)))
                {
                    gameObject.transform.position = PortalB.transform.position+new Vector3(-1,0,0);
                }
                else if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, 0f)))
                {
                    gameObject.transform.position = PortalB.transform.position+new Vector3(0,0,1);
                }
                else if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, 180f)))
                {
                    gameObject.transform.position = PortalB.transform.position+new Vector3(0,0,-1);
                }
                else if (PortalB.transform.position.y > transform.position.y)
                {
                    gameObject.transform.position = PortalB.transform.position+new Vector3(0,-1.2f,0);
                }
                else
                {
                    gameObject.transform.position = PortalB.transform.position+new Vector3(0,1.2f,0);
                }
                
            }
            else if (other.CompareTag("PorB") && checkPortalA)
            {
                hasTeleported = true;
                Quaternion portalRotation = PortalA.transform.rotation;
                if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, -90f)))
                {
                    gameObject.transform.position = PortalA.transform.position+new Vector3(1,0,0);
                }
                else if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, 90f)))
                {
                    gameObject.transform.position = PortalA.transform.position+new Vector3(-1,0,0);
                }
                else if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, 0f)))
                {
                    gameObject.transform.position = PortalA.transform.position+new Vector3(0,0,1);
                }
                else if (IsSimilarRotation(portalRotation, Quaternion.Euler(90f, 0f, 180f)))
                {
                   gameObject.transform.position = PortalA.transform.position+new Vector3(0,0,-1);
                }
                else if (PortalA.transform.position.y > transform.position.y)
                {
                    gameObject.transform.position = PortalA.transform.position+new Vector3(0,-1.2f,0);
                }
                else
                {
                    gameObject.transform.position = PortalA.transform.position+new Vector3(0,1.2f,0);
                }
            }
            
        }
    }
    
    //Exits the trigger collider
    private void OnTriggerExit(Collider other)
    {
        //reset teleportation status and update the portal jump counter
        if (other.CompareTag("PorA") || other.CompareTag("PorB"))
        {
            hasTeleported = false;
            portalCounter += 1;
            text.text = portalCounter + " Portals jumped";
        }
    }

    //Helper function to check if two rotations are similar within a threshold
    //Would not change the rotation of the portals if this function did not exist
    private bool IsSimilarRotation(Quaternion a, Quaternion b, float threshold = 0.01f)
    {
        return Quaternion.Angle(a, b) < threshold;
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    
}
