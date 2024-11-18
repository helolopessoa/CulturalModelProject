// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Microsoft.CSharp.RuntimeBinder;

// [RequireComponent( typeof(CharacterController) )]
// public class PlayerMovement : MonoBehaviour
// {

// 	[SerializeField] float _speed = 25.0f;
// 	[SerializeField] float _jumpSpeed = 8.0f; 
// 	[SerializeField] float _gravity = 20.0f;
// 	[SerializeField] float _sensitivity = 5f;
// 	CharacterController _controller;
//     Rigidbody rb;
// 	float _horizontal, _vertical;
// 	float _mouseX, _mouseY;
// 	bool _jump;
	
// 	// use this for initialization

//     private void Start(){
//         rb = GetComponent<Rigidbody>();
//         RuntimeBinderException.freezeRotation=true;
//     }
// 	void Awake ()
// 	{
// 		_controller = GetComponent<CharacterController>();
// 	}

// 	// screen drawing update - read inputs here
// 	void Update ()
// 	{
// 		_horizontal = Input.GetAxis("Horizontal");
// 		_vertical = Input.GetAxis("Vertical");
// 		_mouseX = Input.GetAxis("Mouse X");
// 		_mouseY = Input.GetAxis("Mouse Y");
// 		_jump = Input.GetButton("Jump");
// 	}
	
// 	// physics simulation update - apply physics forces here
// 	void FixedUpdate ()
// 	{
// 		Vector3 moveDirection = Vector3.zero;

// 		// is the controller on the ground?
// 		if( _controller.isGrounded )
// 		{
// 			// feed moveDirection with input.
// 			moveDirection = new Vector3( _horizontal , 0 , _vertical );
// 			moveDirection = transform.TransformDirection( moveDirection );

// 			// multiply it by speed.
// 			moveDirection *= _speed;
			
// 			// jumping
// 			if( _jump )
// 				moveDirection.y = _jumpSpeed;
// 		}

// 		float turner = _mouseX * _sensitivity;
// 		if( turner!=0 )
// 		{
// 			// action on mouse moving right
// 			transform.eulerAngles += new Vector3( 0 , turner , 0 );
// 		}
		
// 		float looker = -_mouseY * _sensitivity;
// 		if( looker!=0 )
// 		{
// 			// action on mouse moving right
// 			transform.eulerAngles += new Vector3( looker , 0 , 0 );
// 		}
		
// 		// apply gravity to the controller
// 		moveDirection.y -= _gravity * Time.deltaTime;
		
// 		// make the character move
// 		_controller.Move( moveDirection * Time.deltaTime );
// 	}
// }


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    public Transform playerBody;
    float xRotation = 0f;
    float yRotation = 0f;

    public event Action<GameObject> OnPointedGameObjectChanged;
    private GameObject pointedObject;

    public float speed = 5.0f;
    public float turnSpeed = 10.0f;
    public Camera mainCamera;

    private CharacterController characterController;


    private float horizontalRotation = 0.0f;
    public float lookSensitivity = 2.0f;   // Mouse sensitivity for rotation
    public float maxVerticalAngle = 80.0f; // Limit for camera vertical rotation

    private float playerRotationX = 0.0f; // Horizontal rotation of the player body
    private float cameraVerticalRotation = 0.0f; // Vertical rotation of the camera



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        characterController = GetComponent<CharacterController>();
        
        // Assign the main camera if it’s not set
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        RotatePlayerAndCamera();

        // Create a ray from the camera to the mouse position
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        GameObject newPointedObject = null;
            // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            newPointedObject = hit.collider.gameObject;
        }

        // If the pointed object has changed, invoke the event
        if (newPointedObject != pointedObject)
        {
            pointedObject = newPointedObject;
            OnPointedGameObjectChanged?.Invoke(pointedObject); // Trigger event with new pointed objec
        }

        // Move character based on WASD keys
        MovePlayer();

    }

    void MovePlayer()
    {
        // Get WASD input
        float horizontal = Input.GetAxis("Horizontal"); // A/D keys
        float vertical = Input.GetAxis("Vertical");     // W/S keys
        
        // Combine input to a direction vector
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // Move in the direction relative to the character’s current forward facing
        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }
    }


    void RotatePlayerAndCamera()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Rotate the player body only on the X-axis (horizontal rotation)
        playerRotationX += mouseX;
        transform.rotation = Quaternion.Euler(0, playerRotationX, 0);

        // Rotate the camera freely on the Y-axis for looking up and down
        cameraVerticalRotation -= mouseY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -80f, 80f); // Limit up/down angle

        // Apply rotation to the camera (free movement on X and Y)
        mainCamera.transform.localRotation = Quaternion.Euler(cameraVerticalRotation, 0, 0);
    }


}
