using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using HoloToolkit.Unity;
using System;

public class Placement : Singleton<Placement>
{
    private bool IsPlaced = false;

    private int MoveDistance = 2;

    void Start()
    {

    }

    public void InitPlacement()
    {
        IsPlaced = false;
    }

    void Update()
    {
        if(!IsPlaced)
        {
            //Debug.LogFormat("Enter Update Else");
            transform.position = Vector3.Lerp(transform.position, ProposeTransformPosition(), 0.2f);
        }
    }

    Vector3 ProposeTransformPosition()
    {
        // Put the anchor 2m in front of the user.
        Vector3 retval = Camera.main.transform.position + Camera.main.transform.forward * MoveDistance;
        transform.rotation = GameObject.Find("Main Camera").transform.rotation;

        return retval;
    }

    void OnSelect()
    {
        IsPlaced = !IsPlaced;
    }

    /// <summary>
    /// When a remote system has a transform for us, we'll get it here.
    /// </summary>
    /// <param name="msg"></param>
    //void OnStageTransfrom(NetworkInMessage msg)
    //{
    //    // We read the user ID but we don't use it here.
    //    msg.ReadInt64();

    //    transform.localPosition = CustomMessages.Instance.ReadVector3(msg);
    //    transform.localRotation = CustomMessages.Instance.ReadQuaternion(msg);

    //    // The first time, we'll want to send the message to the anchor to do its animation and
    //    // swap its materials.
    //    if (GotTransform == false)
    //    {
    //        GetComponent<InitializeGraph>().Place();
    //    }

    //    GotTransform = true;
    //}
}
