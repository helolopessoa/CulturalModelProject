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

public class NPCSelected : MonoBehaviour
{
    public HVRPlayerController Player;
    public HVRCameraRig CameraRig;
    public HVRPlayerInputs Inputs;

    //public HVRForceGrabber LeftForce;
    //public HVRForceGrabber RightForce;

    public HVRJointHand LeftHand;
    public HVRJointHand RightHand;

    private Transform leftparent;
    private Transform rightParent;


    //private GameObject TalkHUD;
    //[SerializeField]
    //private GameObject HUD;

    // Start is called before the first frame update

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
}
