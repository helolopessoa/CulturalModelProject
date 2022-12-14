using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation3DController : MonoBehaviour {

    private static Dictionary<KeyCode, string> keys = new Dictionary<KeyCode,string>
    {
        { KeyCode.Alpha1, "triggerJoy" },
        { KeyCode.Alpha2, "triggerSadness" },
        { KeyCode.Alpha3, "triggerAnger" },
        { KeyCode.Alpha4, "triggerFear" },
        { KeyCode.Alpha5, "triggerAnticipation" },
        { KeyCode.Alpha6, "triggerSurprise" },
        { KeyCode.Alpha7, "triggerDisgust" },
        { KeyCode.Alpha8, "triggerTrust" }
    };

    public Animator animComp;
	
	// Update is called once per frame
	void Update () {
		foreach(KeyValuePair<KeyCode,string> entry in keys)
        {
            if(Input.GetKeyDown(entry.Key))
            {
                animComp.SetTrigger(entry.Value);
                break;
            }
        }
	}
}
