//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class PlayerController : MonoBehaviour {

//    public Vector2 movementSpeed;
//    public Rigidbody playerBody;

//    [HideInInspector]
//    public float width;
//    [HideInInspector]
//    public float height;

//    private Vector2 direction;


//    public Vector2 GetCurrentPosition() {
//        Vector2 position = playerBody.position;

//        return position;
//    }

//    //// Use this for initialization
//    void Start () {
//        direction = new Vector2(1.0f, 0);
//        //    currentGun.SetShotBy("Player");
//        width = 0.2f;
//        height = 0.35f;
//    }


//    //// Update is called once per frame
//    void Update () {
//    //    float dt = Time.deltaTime;

//    //    UpdateMovement(dt);

//    //    float shot = Input.GetAxis("Fire3");
//    //    if (shot == 1.0f)
//    //    {
//    //        currentGun.SetTrigger(true);
//    //    }

//    //    Vector2 transformPos = transform.position;
//    //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//    //Vector2 shotDirection = mousePosition - transformPos;
//    //shotDirection.Normalize();

//    //    currentGun.SetDirection(shotDirection.x, shotDirection.y);
//    }

///// <summary>
///// On the collision enter2d.
///// </summary>
///// <param name="collision">Collision.</param>
//private void OnCollisionEnter(Collision collision)
//{
//        GameObject obj = collision.gameObject;

//        Debug.Log("entrei");

//        if (collision.gameObject.CompareTag("Bullet"))
//        {
//            Bullet b = obj.GetComponent<Bullet>();

//            if (b.GetShotBy() == "Enemy")
//            {
//                healthBar.fillAmount -= 10 / maxHealth;
//                Destroy(obj);
//            }
//        }
//    }

///// <summary>
///// Identify collision entring.
///// </summary>
///// <param name="collision">Who entered the trigger collision.</param>
//private void OnTriggerEnter2D(Collider2D collision)
//    {
//        GameObject obj = collision.gameObject;

//        if (collision.gameObject.CompareTag("Bullet"))
//        {
//            Bullet b = obj.GetComponent<Bullet>();
//            healthBar.fillAmount -= 10 / maxHealth;
//            Destroy(obj);
//        }
//    }

///// <summary>
///// Draw selected object's gizmos.
///// </summary>
//void OnDrawGizmosSelected()
//{
//    // Display players dimensions (width, height)
//    Gizmos.color = Color.red;
//    Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
//}
//}
