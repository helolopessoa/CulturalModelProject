using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRPlayer : MonoBehaviour {

    [SerializeField]
    private ElementBar healthBar;

    //public Weapon currentGun;
    //public Rigidbody2D playerBody;
    //public CharacterController controller;
    private float maxHealth = 100;
    private float currentHealth = 100;
    private bool walkingPressed;
    private bool isWalking;


    [HideInInspector]
    public float width;
    [HideInInspector]
    public float height;

    // public Image healthBar;
    
    // Use this for initialization
    void Start () {
        this.healthBar.SetMaxValue(maxHealth);
        //playerBody = GetComponent<Rigidbody2D>();
        //isWalking = animComp.GetBool("isWalking");
        //currentGun.SetShotBy("Player");
        // width = 0.2f;
        // height = 0.35f;
    }
    
    public float speed = 12f;
    public float gravity = -9.81f;

    Vector3 velocity;


    void Update()
    {
        this.healthBar.SetValue(currentHealth);

        if(currentHealth <= 0)
        {
            playerDied();
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

        // currentHealth-= 1 / maxHealth;
        // Destroy(obj);
        // }
    }
}
