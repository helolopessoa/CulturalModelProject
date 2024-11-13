using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField]
    private ElementBar healthBar;

    public Animator animComp;
    // public Rigidbody2D playerBody;
    public CharacterController controller;
    private float maxHealth = 100;
    public float currentHealth = 100;
    private bool isWalking;

    [HideInInspector]
    public float width;
    [HideInInspector]
    public float height;
    [HideInInspector]
    public GameObject currentNPC; 

    // public Image healthBar;
    
    // Use this for initialization
    void Start () {
        this.healthBar.SetMaxValue(maxHealth);
        // playerBody = GetComponent<Rigidbody2D>();
        width = 0.2f;
        height = 0.35f;
    }
    
    public float speed = 12f;
    public float gravity = -9.81f;

    Vector3 velocity;


    void Update()
    {
        this.healthBar.SetValue(currentHealth);
        // while (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("d") || Input.GetKeyDown("s"))
        // {
        //    animComp.SetBool("isWalking",true);
        // }
        animComp.SetBool("isWalking", true);
        if (velocity.y > 0)
        {
           animComp.SetBool("isWalking", true);
        }
        else
        {
           animComp.SetBool("isWalking", false);
        }
        if (currentHealth <= 0)
        {
            playerDied();
        }
        if(Input.GetKeyDown(KeyCode.O)){
            currentNPC.DispatchPlayerState("is_giving_item");

        }
        if(Input.GetKeyDown(KeyCode.G)){
            currentNPC.DispatchPlayerState("is_giving_money");

        }
        if(Input.GetKeyDown(KeyCode.F)){
            currentNPC.DispatchPlayerState("is_stealing_item");

        }
        if(Input.GetKeyDown(KeyCode.F)){
            currentNPC.DispatchPlayerState("is_stealing_money");
        }



    }

    void playerDied()
    {
        Debug.Log("PLAYER TA MORTIN");
        // back to main menu
    }

    /// <summary>
    /// Identify collision entring.
    /// </summary>
    /// <param name="collision">Who entered the trigger collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Bullet b = obj.GetComponent<Bullet>();

            currentHealth-= 10 / maxHealth;
            Destroy(obj);
        }
    }
    // public Vector2 GetCurrentPosition()
    // {
    //     // return playerBody.position;
    //     return;
    // }
}
