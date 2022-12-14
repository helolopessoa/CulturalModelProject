using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.Player;
using HurricaneVR.Framework.Shared;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionHUDVR : MonoBehaviour
{
    public HVRPlayerController Player;
    public HVRCameraRig CameraRig;
    public HVRPlayerInputs Inputs;

    public TextMeshProUGUI PauseText;

    //public HVRForceGrabber LeftForce;
    //public HVRForceGrabber RightForce;

    public HVRJointHand LeftHand;
    public HVRJointHand RightHand;

    private Transform leftparent;
    private Transform rightParent;

    private bool Paused;

    public VRNPC currentNPC;


    // Start is called before the first frame update

    public void activate(VRNPC current)
    {
        Debug.Log("VAMO TIME");
        this.currentNPC = current;
        //string[] actions = currentNPC.getActions();
    }
    void Start()
    {
        if (!Player)
            {
                Player = GameObject.FindObjectsOfType<HVRPlayerController>().FirstOrDefault(e => e.gameObject.activeInHierarchy);
            }

            if (Player)
            {
                if (!CameraRig)
                {
                    CameraRig = Player.GetComponentInChildren<HVRCameraRig>();
                }

                if (!Inputs)
                {
                    Inputs = Player.GetComponentInChildren<HVRPlayerInputs>();
                }

                if (!LeftHand) LeftHand = Player.Root.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Left)?.GetComponent<HVRJointHand>();
                if (!RightHand) RightHand = Player.Root.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Right)?.GetComponent<HVRJointHand>();
            }


            if (LeftHand) leftparent = LeftHand.transform.parent;
            if (RightHand) rightParent = RightHand.transform.parent;

            //UpdateSitStandButton();
            //UpdateForceGrabButton();

            //LeftForce = Player.transform.root.GetComponentsInChildren<HVRForceGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Left);
            //RightForce = Player.transform.root.GetComponentsInChildren<HVRForceGrabber>().FirstOrDefault(e => e.HandSide == HVRHandSide.Right);

            //UpdateLeftForceButton();
            //UpdateRightForceButton();

    }
    public void TalkRude()
    {
        currentNPC.DispatchPlayerState("is_not_talking_politely");
    }

    public void TalkPolite()
    {

        currentNPC.DispatchPlayerState("is_talking_politely");
    }


        //public void CalibrateHeight()
        //{
        //    if (CameraRig)
        //        CameraRig.Calibrate();
        //}

        //public void OnSitStandClicked()
        //{
        //    var index = (int)CameraRig.SitStanding;
        //    index++;
        //    if (index > 2)
        //    {
        //        index = 0;
        //    }

        //    CameraRig.SetSitStandMode((HVRSitStand)index);
        //    UpdateSitStandButton();
        //}

        public void TogglePause()
        {
            if (LeftHand && RightHand)
            {
                if (Paused)
                {
                    PauseText.text = "Pause";
                    Time.timeScale = 1f;
                    LeftHand.transform.parent = leftparent;
                    RightHand.transform.parent = rightParent;
                }
                else
                {
                    PauseText.text = "Unpause";
                    Time.timeScale = .00000001f;
                    LeftHand.transform.parent = LeftHand.Target;
                    RightHand.transform.parent = RightHand.Target;
                }

                Paused = !Paused;
            }
        }
}
