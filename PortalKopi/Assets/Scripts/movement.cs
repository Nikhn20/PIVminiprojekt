using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    //Movement and jump parameters
    public float movementSpeed = 5f;
    public float jumpForce = 10f;
    private bool isJumping = false;
    private bool checkLand = false;

    //Input handling
    private InputAction inputMovement;
    private Vector2 movementDirection;
    private Rigidbody rb;

    
    //Stairs
    public GameObject StepRayUnder;
    public GameObject StepRayOver;
    public float stepHeight = 0.2f;
    public float stepSmooth = 0.05f;

    //Object grabbing
    public Camera fpsCam;
    private bool isGrabbing = false;
    private GameObject grabbedObject;

    //UI text
    public TextMeshProUGUI victoryText;

    

    private void Awake()
    {
        //Initialize input and components
        victoryText.enabled = false;
        inputMovement = new InputAction(binding: "<Keyboard>/WASD");
        inputMovement.performed += OnMove;
        inputMovement.Enable();

        rb = GetComponent<Rigidbody>();

        StepRayOver.transform.position =
            new Vector3(StepRayOver.transform.position.x, transform.position.y + stepHeight, StepRayOver.transform.position.z);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //Read movement input
        movementDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //Trigger jump when the action is performed
        if (context.performed)
        {
            Jump();

        }
    }

    public void onObjectGrab(InputAction.CallbackContext context)
    {
        //Handle object grabbing using raycasting
        if (context.started)
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
            {
                if (hit.collider.CompareTag("Object"))
                {
                    isGrabbing = true;
                    grabbedObject = hit.collider.gameObject;
                    grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                }
            }
        }
        else if (context.canceled)
        {
            //Here the object gets released
            grabbedObject.GetComponent<Rigidbody>().useGravity = true;
            isGrabbing = false;
            grabbedObject = null;
        }
        
    }

    private void OnTriggerEnter(Collider context)
    {
        if (context.CompareTag("Ground"))
        {
            checkLand = true;
        }

        if (context.CompareTag("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (context.CompareTag("Victory"))
        {
            //Displaying victory message
            victoryText.enabled = true;
            victoryText.text = "Goodjob on winning this level! :)";
        }


    }
    private void OnTriggerExit(Collider context)
    {
        if (context.CompareTag("Ground"))
        {
            checkLand = false;
        }
    }

    public void Jump()
    {
        //Sets the isJumping boolean to true, if on the ground
        if (checkLand)
        {
            isJumping = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Handle player movement
        Vector3 move = new Vector3(movementDirection.x, 0, movementDirection.y) * movementSpeed * Time.deltaTime;
        transform.Translate(move);
        
        if (isJumping) 
        { 
            //Handles player jumping using addforce on the rigidbody of the player
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
            isJumping = false;
        }
        
        //Handle object grabbing and the objects movement
        if (isGrabbing && grabbedObject != null)
        {
            //Use raycasting to determine the new position of the grabbed object
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
            {
                //Update the position of the grabbed object
                Vector3 directionToHit = hit.point - fpsCam.transform.position;
                float distanceToHit = Vector3.Distance(fpsCam.transform.position, grabbedObject.transform.position);
                Vector3 newPosition = fpsCam.transform.position + fpsCam.transform.forward * distanceToHit;

                grabbedObject.transform.position = newPosition;
                if (Input.GetKey(KeyCode.X))
                {
                    grabbedObject.transform.position += transform.position * (0.2f * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.Z))
                {
                    grabbedObject.transform.position -= transform.position * (0.2f * Time.deltaTime);
                }
                
            }
        }

       
    }

    private void FixedUpdate()
    {
        //Handles climbing steps
        climbStep();
    }

    void climbStep()
    {
        //Raycast to detect steps and then teleport the step
            RaycastHit hitLower;
            if (Physics.Raycast(StepRayUnder.transform.position, transform.TransformDirection(Vector3.forward),
                    out hitLower, 0.1f))
            {
                RaycastHit hitUpper;
                if (!Physics.Raycast(StepRayOver.transform.position, transform.TransformDirection(Vector3.forward),
                        out hitUpper, 0.2f))
                {
                    Vector3 move = new Vector3(0, stepSmooth, 0) * (movementSpeed * 15 * Time.deltaTime);
                    transform.Translate(move);
                }
            }

            RaycastHit hitLower45;
            if (Physics.Raycast(StepRayUnder.transform.position, transform.TransformDirection(1.5f, 0, 1),
                    out hitLower45, 0.1f))
            {
                RaycastHit hitUpper45;
                if (!Physics.Raycast(StepRayOver.transform.position, transform.TransformDirection(1.5f, 0, 1),
                        out hitUpper45, 0.2f))
                {
                    Vector3 move = new Vector3(0, stepSmooth, 0) * (movementSpeed * 15 * Time.deltaTime);
                    transform.Translate(move);
                }
            }

            RaycastHit hitLowerMin45;
            if (Physics.Raycast(StepRayUnder.transform.position, transform.TransformDirection(-1.5f, 0, 1),
                    out hitLowerMin45, 0.1f))
            {
                RaycastHit hitUpperMin45;
                if (!Physics.Raycast(StepRayOver.transform.position, transform.TransformDirection(-1.5f, 0, 1),
                        out hitUpperMin45, 0.2f))
                {
                    Vector3 move = new Vector3(0, stepSmooth, 0) * (movementSpeed * 15 * Time.deltaTime);
                    transform.Translate(move);
                }
            }
        
    }
}
