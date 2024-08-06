using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVRController : MonoBehaviour {

    public CharacterController controller;
    // public Image healthBar;

    // Use this for initialization
    void Start () {
                // playerBody = GetComponent<Rigidbody3D>();
        // currentGun.SetShotBy("Player");
        // width = 0.2f;
        // height = 0.35f;
    }
    
    public float speed = 12f;
    public float gravity = -9.81f;

    Vector3 velocity;


    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity*Time.deltaTime);
    }
}
