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

public class PortalGun : MonoBehaviour
{
    public GameObject PortalA;

    public GameObject PortalB;

    public GameObject PortalAprefab;

    public GameObject PortalBprefab;

    private bool checkPortalA = false;

    private bool checkPortalB = false;

    public Camera fpsCam;

    private bool PortalTeleB;

    private bool PortalTeleA;

    [SerializeField] public bool hasTeleported = false;

    public TextMeshProUGUI text;

    private int portalCounter;

    // Start is called before the first frame update
    void Start()
    {
        portalCounter = 0;
        text.text = portalCounter + " Portals jumped";
    }

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
                        Destroy(PortalA);
                    }

                }

                PortalA = Instantiate(PortalAprefab);
                PortalA.transform.position = hit.point;

                if (fpsCam.transform.localRotation.x < 0.20f && fpsCam.transform.localRotation.x > -0.20f)
                {
                    Quaternion rotation = Quaternion.LookRotation(hit.normal);
                    rotation.eulerAngles = new Vector3(90f, rotation.eulerAngles.y, rotation.eulerAngles.z);
                    PortalA.transform.rotation = rotation;
                    
                    checkPortalA = true;

                }
                else
                {
                    Quaternion rotation = Quaternion.LookRotation(hit.normal);
                    rotation.eulerAngles = new Vector3(0f, rotation.eulerAngles.y, rotation.eulerAngles.z);
                    PortalA.transform.rotation = rotation;
                    
                    checkPortalA = true;
                }



            }

        }
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTeleported)
        {
            if (other.CompareTag("PorA") && checkPortalB)
            {
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
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PorA") || other.CompareTag("PorB"))
        {
            hasTeleported = false;
            portalCounter += 1;
            text.text = portalCounter + " Portals jumped";
        }
    }

    private bool IsSimilarRotation(Quaternion a, Quaternion b, float threshold = 0.01f)
    {
        return Quaternion.Angle(a, b) < threshold;
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    
}
