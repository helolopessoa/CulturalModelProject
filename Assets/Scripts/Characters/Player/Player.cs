using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Dictionary<KeyCode, string> keyActions = new Dictionary<KeyCode, string>
    {
        { KeyCode.O, "is_giving_item" },
        { KeyCode.G, "is_giving_money" },
        { KeyCode.F, "is_stealing_item" },
        { KeyCode.H, "is_stealing_money" },
        { KeyCode.P, "is_talking_politely" },
        { KeyCode.R, "is_not_talking_politely" },
        { KeyCode.T, "is_talking" }
    };



    [SerializeField]
    private ElementBar healthBar;
    [HideInInspector]
    private bool isMoving;
    public bool chatboxDisabled = true;

    // public Rigidbody2D playerBody;
    public Animator animComp;
    public CharacterController controller;
    private float maxHealth = 100;
    public float currentHealth = 100;


    [HideInInspector]
    public float width;
    [HideInInspector]
    public float height;
    [HideInInspector]
    public NPC currentNPC;
    
    public PlayerMovement mouseLook;
    // public Image healthBar;
    
    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        healthBar.SetMaxValue(maxHealth);
        // playerBody = GetComponent<Rigidbody2D>();
        width = 0.2f;
        height = 0.35f;

        // Subscribe to the OnPointedObjectChanged event
        if (mouseLook != null)
        {
            mouseLook.OnPointedGameObjectChanged += HandlePointedGameObjectChanged;
        }

    }
    
    public float speed = 12f;
    public float gravity = -9.81f;



    void Update()
    {

        isMoving = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && chatboxDisabled;

        healthBar.SetValue(currentHealth);
        if (isMoving)
        {
           animComp.SetTrigger("IsWalking");
        }
        else if(!isMoving)
        {
           animComp.SetTrigger("StoppedWalking");
        }

        if (currentHealth <= 0)
        {
            playerDied();
        }

        foreach (var action in keyActions)
        {
            if (Input.GetKeyDown(action.Key) && )
            {
                currentNPC.DispatchPlayerState(action.Value);
            }
        }

    }

    private void HandlePointedGameObjectChanged(GameObject newPointedObject)
    {
        if (newPointedObject != null)
        {
            if (newPointedObject.TryGetComponent<NPC>(out NPC npc)){
                currentNPC = newPointedObject.GetComponent<NPC>();
                currentNPC.OnMouseAimEnter();
                Debug.Log("Now pointing at: " + currentNPC);

            }
            // Example: Perform interaction or update based on the pointed object
            // InteractWithObject(newPointedObject);
        }
        else
        {
            currentNPC?.OnMouseAimExit();
            Debug.Log("No object is currently pointed at.");
        }
    }

    void playerDied()
    {
        Debug.Log("PLAYER TA MORTIN");
        if (mouseLook != null)
        {
            mouseLook.OnPointedGameObjectChanged -= HandlePointedGameObjectChanged;
        }
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

    // void OnDestroy()
    // {
    //     // Unsubscribe from the event when this script is destroyed
    //     if (mouseLookScript != null)
    //     {
    //         mouseLookScript.OnPointedObjectChanged -= HandlePointedObjectChanged;
    //     }
    // }
    // public Vector2 GetCurrentPosition()
    // {
    //     // return playerBody.position;
    //     return;
    // }
}
