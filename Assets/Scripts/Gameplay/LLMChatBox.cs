using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LLMChatBox : MonoBehaviour
{



    public GameObject LLMChat;

    public TMP_InputField messageInput;
    public TMP_Text messageOutput;
    public string response;

    void Update()
    {
        messageOutput.text = this.response;
    }    

    public LlamaResponse GetLlamaResponse()
    {
        LlamaResponse lr = LlamaAPI.getLlamaResponse();
        this.response = lr;
    }

    public void PostLlamaAction()
    {
        string message = messageInput.text;
        LlamaAPI.postLlamaAction(message);
    }
}