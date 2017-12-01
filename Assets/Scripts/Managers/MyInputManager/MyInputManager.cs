using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputManager : MonoBehaviour
{
    public static MyInputManager instance = null;
    public bool useJoystick=true;
    public List<KeyCode> jumpKeyboard = new List<KeyCode>();
    public List<KeyCode> sneakKeyboard = new List<KeyCode>();
    public List<KeyCode> climbKeyboard = new List<KeyCode>();
    public List<KeyCode> GlideKeyboard = new List<KeyCode>();
    public List<KeyCode> LookKeyboard = new List<KeyCode>();
    public List<KeyCode> FireModeKeyboard = new List<KeyCode>();


    public KeyCode cameraXKeyboard;

    public List<KeyCode> jumpJoystick = new List<KeyCode>();
    public List<KeyCode> sneakJoystick = new List<KeyCode>();
    public List<KeyCode> climbJoystick = new List<KeyCode>();
    public List<KeyCode> GlideJoystick = new List<KeyCode>();
    public List<KeyCode> LookJosyick = new List<KeyCode>();
    public List<KeyCode> FireModeJoystick = new List<KeyCode>();

    public Dictionary<string, string> _axis = new Dictionary<string, string>();
    public  Dictionary<string, string> _AxisJoystick = new Dictionary<string, string>();
    private  Dictionary<string, string> _AxisKeyBoard = new Dictionary<string, string>();
    private Dictionary<string, bool> _axisValues = new Dictionary<string, bool>();


    public Dictionary<string, List<KeyCode>> _dicJoystick = new Dictionary<string, List<KeyCode>>();
    private Dictionary<string, List<KeyCode>> _dicKeyBoard = new Dictionary<string, List<KeyCode>>();
    private Dictionary<string, List<KeyCode>> _dic = new Dictionary<string, List<KeyCode>>();



    public delegate bool funGetKey(KeyCode key);

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) {
            useJoystick = !useJoystick;
            if (useJoystick)
            {
                EventManager.instance.ExecuteEvent("usingJoystick");
            }
            else
            {
                EventManager.instance.ExecuteEvent("usingKeyboard");
            }
        }
    }
    private void Start()
    {
        this._dicKeyBoard.Add("Jump", jumpKeyboard);
        this._dicKeyBoard.Add("Sneak", sneakKeyboard);
        this._dicKeyBoard.Add("Climb", climbKeyboard);
        this._dicKeyBoard.Add("Glide", GlideKeyboard);
        this._dicKeyBoard.Add("Look", LookKeyboard);
        this._dicKeyBoard.Add("FireMode", FireModeKeyboard);
        _AxisKeyBoard.Add("CameraX", "Mouse X");
        _AxisKeyBoard.Add("CameraY", "Mouse Y");
        _AxisKeyBoard.Add("Aim", "Fire2");
        _AxisKeyBoard.Add("Shoot", "Fire1");

        this._dicJoystick.Add("Jump", jumpJoystick);
        this._dicJoystick.Add("Sneak", sneakJoystick);
        this._dicJoystick.Add("Climb", climbJoystick);
        this._dicJoystick.Add("Glide", GlideJoystick);
        this._dicJoystick.Add("Look", LookJosyick);
        this._dicJoystick.Add("FireMode", FireModeJoystick);
        _AxisJoystick.Add("CameraX", "STICKLHOR");
        _AxisJoystick.Add("CameraY", "STICKLVER");
        _AxisJoystick.Add("Aim", "Aim Joystick");
        _AxisJoystick.Add("Shoot", "Shoot Joystick");
    }

    public void InDictionary(string keyName)
    {
        if (!_dic.ContainsKey(keyName))
        {;
            throw new System.ArgumentException("El nombre de la clave "+keyName+  " no existe en el input");
        }
    }

    private void SetCurrentInputMode()
    {
        _dic = useJoystick ? _dicJoystick : _dicKeyBoard;
    }

    public bool GetKeyDown(string keyName)
    {
        SetCurrentInputMode();
        InDictionary(keyName);
        return GetKeyFunction(keyName, Input.GetKeyDown);
    }

    public bool GetKeyUp(string keyName)
    {
        SetCurrentInputMode();
        InDictionary(keyName);
        return GetKeyFunction(keyName, Input.GetKeyUp);
    }

    public bool GetKey(string keyName)
    {
        SetCurrentInputMode();
        InDictionary(keyName);
        return GetKeyFunction(keyName, Input.GetKey);
    }

    public float GetAxis(string keyName)
    {
        SetCurrentInputModeAxis();
        InDictionaryAxis(keyName);
        return GetAxisFunction(_axis[keyName], Input.GetAxis);
    }

    public bool GetButton(string keyName)
    {

        SetCurrentInputModeAxis();
        InDictionaryAxis(keyName);
        return GetButtonFunction(_axis[keyName], Input.GetButton);
    }

    public bool GetButtonDown(string keyName)
    {

        SetCurrentInputModeAxis();
        InDictionaryAxis(keyName);
        if (!useJoystick)
        {
            return GetButtonFunction(_axis[keyName], Input.GetButtonDown);
        }
        else {
            bool value = GetAxisFunction(_axis[keyName], Input.GetAxis) > 0;
            if (!_axisValues.ContainsKey(keyName))
            {
                _axisValues.Add(keyName, value);
                return value;
            }
            else {
                if (!_axisValues[keyName] && value)
                {
                    _axisValues[keyName] = value;
                    return true;
                }
                else {
                    _axisValues[keyName] = value;
                    return false;
                }

            }
        }
    }

    private void InDictionaryAxis(string keyName)
    {
        if (!_axis.ContainsKey(keyName))
        {
            throw new System.ArgumentException("El nombre del axis no existe en el input manager");
        }
    }

    private void SetCurrentInputModeAxis()
    {
        _axis = useJoystick ? _AxisJoystick : _AxisKeyBoard;
    }

    private float GetAxisFunction(string keyName, Func<string, float> fun)
    {
        return fun(keyName);
    }

    private bool GetButtonFunction(string keyName, Func<string, bool> fun)
    {
        return fun(keyName);
    }

    public int GetFireMode()
    {
        SetCurrentInputMode();
        InDictionary("FireMode");
        List<KeyCode> codes = _dic["FireMode"];
        for (int i = 0; i < codes.Count; i++)
        {
            bool keyPressed = Input.GetKeyDown(_dic["FireMode"][i]);
            if (keyPressed)
            {
                return i;
            }
        }
        return -1;
    }

    public bool GetKeyFunction(string keyName, funGetKey fun)
    {
        if (_dic[keyName].Count == 0) {
            return false;
        }
        foreach (var item in _dic[keyName])
        {
            bool keyPressed = fun(item);
            if (!keyPressed)
            {
                return false;
            }
        }
        
        return true;
    }
}
