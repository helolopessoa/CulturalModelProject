using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxemicsBehavior : MonoBehaviour
{

    public NPC relatedNPC;
    public float[] proxemicValues;
    public Player currentPlayer;

    string[] proxemicStates;
    float maxProxemicsTimer = 2f;
    float proxemicsTimer = 0;
    List<GameObject> otherEnemies;

    private void Awake()
    {
        otherEnemies = new List<GameObject>();
    }

    // Use this for initialization
    void Start()
    {
        SetCurrentPlayer(null);
        proxemicValues = new float[3] { 0.7f, 0.45f, 0.25f };
        proxemicStates = new string[3] { "is_social", "is_personal", "is_intimate" };
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if (currentPlayer && proxemicsTimer > maxProxemicsTimer)
        {
            Vector3 playerPos = currentPlayer.gameObject.transform.position;
            float playerWidth = currentPlayer.width;
            float playerHeight = currentPlayer.height;
            playerPos.x += playerWidth / 2;
            playerPos.y += playerHeight / 2;

            Vector3 NPCPos = relatedNPC.gameObject.transform.position;
            float playerDist = Vector3.Distance(playerPos, NPCPos);
            string currProxState = "";

            //Debug.Log(currProxState);
            for (int i = 0; i < proxemicValues.Length; i++)
            {
                float currProxVal = proxemicValues[i];

                if (playerDist <= currProxVal)
                {
                    currProxState = proxemicStates[i];
                }
            }

            relatedNPC.DispatchPlayerState(currProxState);

            if (currentPlayer.currentHealth < 60 && currProxState == "is_social")
            {
                relatedNPC.DispatchPlayerState("is_injured");
            }

            for (int i = 0; i < otherEnemies.Count; i++)
            {
                GameObject obj = otherEnemies[i];
                NPC enemy = obj.gameObject.GetComponent<NPC>();
                if (enemy.hasBeenShot)
                {
                    relatedNPC.DispatchPlayerState("is_harming");
                }
            }

            proxemicsTimer = 0;
        }

        proxemicsTimer += dt;
    }

    /// <summary>
    /// Sets the current player.
    /// </summary>
    /// <param name="pObj">PlayerController object.</param>
    public void SetCurrentPlayer(Player pObj)
    {
        currentPlayer = pObj;
    }

    /// <summary>
    /// Identify collision entring.
    /// </summary>
    /// <param name="collision">Who entered the trigger collision.</param>
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentPlayer = collision.GetComponent<Player>();
        }
        else if (collision.CompareTag("Bullet"))
        {
            relatedNPC.DispatchPlayerState("is_shooting");
        }
        //else if (collision.CompareTag("Enemy")) {
        //    GameObject currEnemy = collision.gameObject;
        //    if (currEnemy) {
        //        otherEnemies.Add(currEnemy);
        //    }
        //}
    }

    /// <summary>
    /// Identify collision exiting.
    /// </summary>
    /// <param name="collision">Who exited the trigger collision.</param>
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetCurrentPlayer(null);
        }
        else if (collision.CompareTag("Bullet"))
        {
            relatedNPC.DispatchPlayerState("is_shooting");
        }
        //else if (collision.CompareTag("Enemy")) {
        //    GameObject currEnemy = collision.gameObject;
        //    otherEnemies.Remove(currEnemy);
        //}
    }


    /// <summary>
    /// Draw selected object's gizmos.
    /// </summary>
    //void OnDrawGizmosSelected() {
    //    // Display the proxemics radius when the obj is selected in scene
    //    for (int i=0; i < proxemicValues.Length; i++) {
    //        float currRadius = proxemicValues[i];

    //        Gizmos.color = Color.red;
    //        Gizmos.DrawWireSphere(transform.position, currRadius);
    //    }
    //}
}
