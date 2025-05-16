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
    [HideInInspector]
    string response;
    [HideInInspector]
    NPC currentNPC;

    void Update()
    {

        if (messageInput.isFocused && Input.GetKeyDown(KeyCode.Return))
        {
            onSendMessage();
        }

        if(!string.IsNullOrWhiteSpace(this.response))
        {
            messageOutput.text = this.response;
        }
        else
        {
            messageOutput.text = " ...";
        }
    }

    public void onNPCSelected(NPC npc)
    {
        this.currentNPC = npc;
        // sendButton.onClick.AddListener(onSendMessage);
        EventSystem.current.SetSelectedGameObject(messageInput.gameObject);
        messageInput.ActivateInputField();
    }

    public void onSendMessage()
    {
        string message = messageInput.text;
        if (!string.IsNullOrWhiteSpace(message))
        {
            PostLlamaAction(message);
            messageInput.text = "";
        }
        // this.currentNPC?.DispatchPlayerState(action.Value);
        this.GetLlamaResponse();
        EventSystem.current.SetSelectedGameObject(messageInput.gameObject);
        messageInput.ActivateInputField();
    }    

    public void GetLlamaResponse()
    {
        LlamaResponse lr = LlamaAPI.getLlamaResponse();
        this.response = lr.answer;
    }

    public void PostLlamaAction(string message)
    {
        LlamaAPI.postLlamaAction(message);
    }
}