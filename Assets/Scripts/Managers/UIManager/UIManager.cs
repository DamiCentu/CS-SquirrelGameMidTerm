using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    public Text tutorialText;
    public float timeToShowInstruction = 4f;
    private float _timer;

    public string MoveKey;
    public string shortJumpKey;
    public string longJumpKey;
    public string FlyKey;
    public string ClimbKey;
    public string SlowKey;

    public string MoveJoystcik;
    public string shortJumpJoystcik;
    public string longJumpJoystcik;
    public string FlyJoystcik;
    public string ClimbJoystcik;
    public string SlowJoystcik;

    private Dictionary<string, string> dic = new Dictionary<string, string>();
    private Dictionary<string, string> joystcikDic=new Dictionary<string, string>();
    private Dictionary<string, string> keyboardDic = new Dictionary<string, string>();
    // Use this for initialization
    void Start () {
        EventManager.instance.SubscribeEvent("EnableWalkText", MoveInstructions);
        EventManager.instance.SubscribeEvent("EnableShortJumpText", ShortJumpInstructions);
        EventManager.instance.SubscribeEvent("EnableLongJumpText", LongJumpInstructions);
        EventManager.instance.SubscribeEvent("EnableGlideText", GlideInstructions);
        EventManager.instance.SubscribeEvent("ClimbInstructions", ClimbInstructions);
        EventManager.instance.SubscribeEvent("SneakInstructions", ClimbInstructions);
        EventManager.instance.SubscribeEvent("usingJoystick", UseJoystick);
        EventManager.instance.SubscribeEvent("usingKeyboard", UseKeyboard);

        joystcikDic.Add("EnableWalk", MoveJoystcik);
        joystcikDic.Add("EnableShortJump", shortJumpJoystcik);
        joystcikDic.Add("EnableLongJump", longJumpJoystcik);
        joystcikDic.Add("EnableGlide", FlyJoystcik);
        joystcikDic.Add("ClimbInstructions", ClimbJoystcik);
        joystcikDic.Add("SneakInstructions", SlowJoystcik);

        keyboardDic.Add("EnableWalk", MoveKey);
        keyboardDic.Add("EnableShortJump", shortJumpKey);
        keyboardDic.Add("EnableLongJump", longJumpKey);
        keyboardDic.Add("GlideInstructions", FlyKey);
        keyboardDic.Add("ClimbInstructions", ClimbKey);
        keyboardDic.Add("SneakInstructions", SlowKey);

        dic = MyInputManager.instance.useJoystick? joystcikDic:keyboardDic;
    }

    private void UseKeyboard(params object[] parametersWrapper)
    {
        dic = keyboardDic;
    }

    private void UseJoystick(params object[] parametersWrapper)
    {
        dic = joystcikDic;
    }

    private void setText(String text)
    {
        if (this.tutorialText != null)
        {
            this.tutorialText.text = text;
            _timer = 0;
        }
    }

    private void ClimbInstructions(params object[] parametersWrapper)
    {
        this.setText(dic["ClimbInstructions"]);
    }

    private void GlideInstructions(params object[] parametersWrapper)
    {
        this.setText(dic["GlideInstructions"]);
    }

    private void LongJumpInstructions(params object[] parametersWrapper)
    {
        this.setText(dic["EnableLongJump"]);
    }

    private void ShortJumpInstructions(params object[] parametersWrapper)
    {
        this.setText(dic["EnableShortJump"]);
    }

    private void MoveInstructions(params object[] parametersWrapper)
    {
        this.setText(dic["EnableWalk"]);
    }
    
    void Update () {
        if (tutorialText != null) {
            _timer += Time.deltaTime;
            if (_timer > timeToShowInstruction)
            {
                tutorialText.gameObject.SetActive(false);
            }
            else {
                tutorialText.gameObject.SetActive(true);
            }
        }
	}
}
