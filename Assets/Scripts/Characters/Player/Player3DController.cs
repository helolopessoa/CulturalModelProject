using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player3DController : MonoBehaviour {

    public Vector2 movementSpeed;
    // public Weapon currentGun;
    public Image healthBar;

    [HideInInspector]
    public float width;
    [HideInInspector]
    public float height;

    private float maxHealth = 100f;
    private Rigidbody2D playerBody;
    private Vector2 direction;


    public Vector2 GetCurrentPosition() {
        Vector2 position = playerBody.position;

        return position;
    }

    // Use this for initialization
    void Start () {
        playerBody = GetComponent<Rigidbody2D>();
        width = 0.2f;
        height = 0.35f;
    }

    /// <summary>
    /// Updates the movement.
    /// </summary>
    /// <param name="dt">Delta time.</param>
    // Update is called once per frame
    void Update () {
        float dt = Time.deltaTime;

        // UpdateMovement(dt);

        // float shot = Input.GetAxis("Fire3");
        // if (shot == 1.0f) {
        //     currentGun.SetTrigger(true);
        // }

        Vector2 transformPos = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shotDirection = mousePosition - transformPos;
        // shotDirection.Normalize();

        // currentGun.SetDirection(shotDirection.x, shotDirection.y);
    }

    /// <summary>
    /// On the collision enter2d.
    /// </summary>
    /// <param name="collision">Collision.</param>
    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject obj = collision.gameObject;

        if (collision.gameObject.CompareTag("Bullet")) {
               healthBar.fillAmount -= 10 / maxHealth;
               Destroy(obj);
        }
    }

    /// <summary>
    /// Identify collision entring.
    /// </summary>
    /// <param name="collision">Who entered the trigger collision.</param>
    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject obj = collision.gameObject;

        if (collision.gameObject.CompareTag("Bullet")) {

                healthBar.fillAmount -= 10 / maxHealth;
                Destroy(obj);
        }
    }

    /// <summary>
    /// Draw selected object's gizmos.
    /// </summary>
    void OnDrawGizmosSelected() {
        // Display players dimensions (width, height)
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
}
