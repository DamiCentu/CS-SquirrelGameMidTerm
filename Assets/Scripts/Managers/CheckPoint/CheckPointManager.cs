using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour {
    public Vector3? playerPosition=null;
    public int?  playerLife=null;
    public static CheckPointManager instance = null;
    private bool createEvent = false;
    public Quaternion playerRotation;
    public Vector3 cameraLocalEulerRot;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else  {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    public void CreateEvents()
    {
        EventManager.instance.SubscribeEvent("SaveCheckPoint", SaveCheckPoint);
    }

    internal void SaveCheckPoint(params object[] parametersWrapper)
    {
        //print("guardo checkpoint");
        Transform checkPointTransform = (Transform)parametersWrapper[0];
        PlayerBrain player = UnityEngine.Object.FindObjectOfType<PlayerBrain>();
        //CameraBehaviur camera = UnityEngine.Object.FindObjectOfType<CameraBehaviur>();
        cameraLocalEulerRot = checkPointTransform.eulerAngles; //camera.transform.localEulerAngles;
        playerPosition = checkPointTransform.position;
        playerRotation = checkPointTransform.rotation;
       // playerLife = player.life;
    }


}
