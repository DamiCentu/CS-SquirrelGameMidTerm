using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFirstPersonStrategy : ICameraStrategy {
    string _name = "FirstPerson";
    float _currentX = 0f;
    float _currentY = 0f;
    Quaternion _rotation;

    //Seteables 

    //float _distanceOffset;
    //float _heightOffset;
    //float _xOffset;

    float _YangleMin = 0f;
    float _YangleMax = 0f;

    float _XangleMin = 0f;
    float _XangleMax = 0f;

    float _sensivityXMouse = 0f;
    float _sensivityYMouse = 0f;

    float _sensivityXJoystick = 0f;
    float _sensivityYJoystick = 0f;

    Transform _camTransform;
    Transform _positionOfFirstPersonCamera;
    Transform _player;


    public string Name { get { return _name; } }

    public Vector3 TargetOfTransition { get { return _positionOfFirstPersonCamera.position; } }

    public CameraFirstPersonStrategy(string name) {
        _name = name;
    }

    public CameraFirstPersonStrategy SetTransforms(Transform camTransform, Transform positionFirstPersonCamera, Transform player) {
        _camTransform = camTransform;
        _positionOfFirstPersonCamera = positionFirstPersonCamera;
        _player = player;
        return this;
    }

    public CameraFirstPersonStrategy SetSensi(float sentivityXMouse, float sensivityYMouse, float sensivityXJoystick, float sensivityYJoystick) {
        _sensivityXMouse = sentivityXMouse;
        _sensivityYMouse = sensivityYMouse;
        _sensivityXJoystick = sensivityXJoystick;
        _sensivityYJoystick = sensivityYJoystick;
        return this;
    }

    public CameraFirstPersonStrategy SetYAnglesMaxAndMin(float YangleMin, float YangleMax) {
        _YangleMin = YangleMin;
        _YangleMax = YangleMax;
        return this;
    }

     public CameraFirstPersonStrategy SetXAnglesMaxAndMin(float XangleMin, float XangleMax) {
        _XangleMin = XangleMin;
        _XangleMax = XangleMax;
        return this;
    }

    public void OnUpdate() {
        if (!MyInputManager.instance.useJoystick)
            UpdateXYInputs(_sensivityXMouse, _sensivityYMouse, _YangleMax, _YangleMin, _XangleMax, _XangleMin);
        else
            UpdateXYInputs(_sensivityXJoystick, _sensivityYJoystick, _YangleMax, _YangleMin, _XangleMax, _XangleMin);

        //Debug.Log(_currentX);
        //Debug.Log(_camTransform.localEulerAngles.x);
        //Debug.Log(_player.localEulerAngles.y);
    }

    public void OnLateUpdate() {
        _camTransform.position = _positionOfFirstPersonCamera.position;
        _camTransform.localEulerAngles = new Vector3(-_currentY, _currentX, 0);
    }

    void UpdateXYInputs(float sensiX, float sensiY, float angleMaxY, float angleMinY, float angleMaxX, float angleMinX) {
        _currentX += MyInputManager.instance.GetAxis("CameraX") * sensiX;
        _currentY += MyInputManager.instance.GetAxis("CameraY") * sensiY;

        _currentY = Mathf.Clamp(_currentY, angleMinY, angleMaxY);


        //_camTransform.rotation
        _currentX = Mathf.Clamp(_currentX, _player.localEulerAngles.y + angleMinX, Mathf.Min( _player.localEulerAngles.y + angleMaxX , 359f));

        _currentX = _currentX % 360;
    }
}
