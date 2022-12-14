using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialSpaceBehavior : MonoBehaviour
{

    public NPC relatedNPC;
    float maxProxemicsTimer = 1f;
    float proxemicsTimer = 0;
    Player currentPlayer;

    // Use this for initialization
    void Start()
    {
        SetCurrentPlayer(null);
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if (currentPlayer && proxemicsTimer > maxProxemicsTimer)
        {
            relatedNPC.DispatchPlayerState("is_social");

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentPlayer = collision.GetComponent<Player>();
        }
    }

    /// <summary>
    /// Identify collision exiting.
    /// </summary>
    /// <param name="collision">Who exited the trigger collision.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetCurrentPlayer(null);
        }
    }
}
