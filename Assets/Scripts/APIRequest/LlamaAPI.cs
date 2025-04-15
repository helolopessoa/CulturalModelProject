using System.Collections;
using UnityEngine;
using System.Net;
using System.IO;
using System;
using System.Text;
using environments;

public static class LlamaAPI
{

        public static LlamaResponse getLlamaResponse()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(environments.apiLlamaURL + "/generate");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();
            return JsonUtility.FromJson<LlamaResponse>(json);

      }

    public static void postLlamaAction()
    {
        // string[] emotionKeys = { "axeAF", "axeDT", "axeSJ", "axeAS" };
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(environment.apiURL + "/llamaapi");
        request.Method = "POST";

        var postData = "{\"prompt\": \"Write a phrase about mountains\"}";
        // var data = Encoding.ASCII.GetBytes(postData);
        
        // request.ContentLength = data.Length;

        using (var stream = request.GetRequestStream())
        {
            stream.Write(postData);
            stream.Flush();
        }

        var response = (HttpWebResponse)request.GetResponse();

        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        
        Debug.Log("Llama response: " + response);
        return JsonUtility.FromJson<LlamaResponse>(response);
        

    }
}
