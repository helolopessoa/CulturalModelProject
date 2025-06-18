using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LLMChatBox : MonoBehaviour
{
    public GameObject LLMChat;
    public TMP_InputField messageInput;
    public TMP_Text messageOutput;

    public Transform chatContent;
    [HideInInspector]
    string response;
    [HideInInspector]
    NPC currentNPC;

    void Start()
    {
        messageInput.onEndEdit.AddListener(onSendMessage);
        // messageInput.gameObject.SetActive(false); 
    }
    void Update()
    {

        // if (messageInput.isFocused && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        // {
        //     Debug.Log("onSendMessage called from Update");
        //     onSendMessage();
        // }

        if(!string.IsNullOrWhiteSpace(response))
        {
            messageOutput.text = response;
        }
        else
        {
            messageOutput.text = " Hi! I'm "  + currentNPC?.nameString + ". Nice to meet you!";
        }
    }

    public void onNPCSelected(NPC npc)
    {
        currentNPC = npc;
        // sendButton.onClick.AddListener(onSendMessage);
        EventSystem.current.SetSelectedGameObject(messageInput.gameObject);
        messageInput.ActivateInputField();
    }

    public void onSendMessage(string message)
    {
        // Debug.Log(messageInput.isFocused);
        // if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        // {
            // string message = messageInput.text;
            Debug.Log("onSendMessage");
            Debug.Log(message);
            if (!string.IsNullOrWhiteSpace(message))
            {
                PostLlamaAction(message);
                messageInput.text = "";
            }
            // this.currentNPC?.DispatchPlayerState(action.Value);
            GetLlamaResponse();
            EventSystem.current.SetSelectedGameObject(messageInput.gameObject);
            messageInput.ActivateInputField();
        // }

    }    

    public void GetLlamaResponse()
    {
        LlamaResponse lr = LlamaAPI.getLlamaResponse();
        response = lr.answer;
    }

    public void PostLlamaAction(string message)
    {
        LlamaAPI.postLlamaAction(message);
    }
}