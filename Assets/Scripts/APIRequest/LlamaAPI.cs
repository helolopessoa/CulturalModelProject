using System.Collections;
using UnityEngine;
using System.Net;
using System.IO;
using System;
using System.Text;

public static class LlamaAPI
{
    private static string apiURL = "http://localhost:11434";
    private static string apiLlamaURL = apiURL + "/llamaapi";


        public static LlamaResponse getLlamaResponse()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiLlamaURL + "/generate");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();
            return JsonUtility.FromJson<LlamaResponse>(json);

      }

    public static void postLlamaAction(string message)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiLlamaURL + "/llamaapi");
        request.Method = "POST";

        var postData = "{\"prompt\": \"" + message +"\"}";
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);


        using (var stream = request.GetRequestStream())
        {
            stream.Write(byteArray, 0, byteArray.Length);

            stream.Flush();
        }

        var response = (HttpWebResponse)request.GetResponse();

        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        
        Debug.Log("Llama response: " + response);
        // return JsonUtility.FromJson<LlamaResponse>(response);
        

    }
}
