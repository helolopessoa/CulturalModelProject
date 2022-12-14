using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHUD : MonoBehaviour
{
    //[SerializeField]
    //private GameManager manager;
    [SerializeField]
    private GameObject GiveOrStealHUD;
    [SerializeField]
    private GameObject TalkHUD;
    [SerializeField]
    private GameObject HUD;

    public NPC currentNPC;

    private bool giveOrSteal = true; // true for giving, false for stealing;

    // Start is called before the first frame update
    void Start()
    {
        HUD.SetActive(false);
        GiveOrStealHUD.SetActive(false);
        TalkHUD.SetActive(false);
    }


    public void Exit()
    {
        HUD.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

     public void TalkOptions()
    {
        HUD.SetActive(false);
        TalkHUD.SetActive(true);

    }
     public void StealOptions()
    {
        HUD.SetActive(false);
        GiveOrStealHUD.SetActive(true);
        this.giveOrSteal = false;
    }
     public void GiveOptions()
    {
        HUD.SetActive(false);
        GiveOrStealHUD.SetActive(true);
        this.giveOrSteal = true;
    }

    public void TalkRude()
    {
        currentNPC.DispatchPlayerState("is_not_talking_politely");
        TalkHUD.SetActive(false);
        resumeGame();
        //dont know what happens for now
    }

    public void TalkPolite()
    {

        currentNPC.DispatchPlayerState("is_talking_politely");
        TalkHUD.SetActive(false);
        resumeGame();
        //dont know what happens for now
    }

    public void GiveOrStealItem()
    {
        if (this.giveOrSteal)
        {
            Debug.Log("Gave Item");
            currentNPC.DispatchPlayerState("is_giving_item");
        }
        else
        {
            Debug.Log("Stole Item");
            currentNPC.DispatchPlayerState("is_stealing_item");
        }
        GiveOrStealHUD.SetActive(false);
        resumeGame();
        //dont know what happens for now
    }

     public void GiveOrStealMoney()
    {
        if (this.giveOrSteal)
        {
            Debug.Log("Gave Money");
            currentNPC.DispatchPlayerState("is_giving_money");
        }
        else
        {
            Debug.Log("Stole Money");
            currentNPC.DispatchPlayerState("is_stealing_money");
        }
        GiveOrStealHUD.SetActive(false);
        resumeGame();
        //dont know what happens for now
    }

    void resumeGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void activate(NPC current)
    {
        this.currentNPC = current;
        this.gameObject.SetActive(true);
        //string[] actions = currentNPC.getActions();
    }
}
