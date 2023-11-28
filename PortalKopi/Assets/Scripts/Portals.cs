using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using RenderPipeline = UnityEngine.Rendering.RenderPipelineManager;

public class Portals : MonoBehaviour


{
    public GameObject PortalA;

    public GameObject PortalB;

    public PortalGun scriptPortal;

    private bool objHasTeleported = false;
    private void Start()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!objHasTeleported)
        {
            if (other.CompareTag("PorA"))
            {
                objHasTeleported = true;
                Quaternion portalRotation = PortalB.transform.rotation;
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
            objHasTeleported = false;
        }
    }

    private bool IsSimilarRotation(Quaternion a, Quaternion b, float threshold = 0.01f)
    {
        return Quaternion.Angle(a, b) < threshold;
    }

    private void Update()
    {
        PortalB = FindObjectOfType<PortalGun>().PortalB;
        
        PortalA = FindObjectOfType<PortalGun>().PortalA; 
    }
}
