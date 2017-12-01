using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShootingStrategy : ICameraStrategy {

	string _name = "Shooting";
    float _currentX = 0f;
    float _currentY = 0f;
    Quaternion _rotation;
    Vector3 _offsetShootingPosition;

    //Seteables 

    //float _distanceOffset;
    //float _heightOffset;
    //float _xOffset;

    float _YangleMin = 0f;
    float _YangleMax = 0f;

    float _sensivityXMouse = 0f; 
    float _sensivityYMouse = 0f;

    float _sensivityXJoystick = 0f;
    float _sensivityYJoystick = 0f;

    Transform _camTransform;
    Transform _positionOfCameraShooting;


    public string Name { get { return _name; } }

    public Vector3 TargetOfTransition { get { return _positionOfCameraShooting.position + _rotation * _offsetShootingPosition; } }

    public CameraShootingStrategy(string name) {
        _name = name;
    }

    public CameraShootingStrategy SetTransforms(Transform camTransform, Transform positionCameraShooting) {
        _camTransform = camTransform;
        _positionOfCameraShooting = positionCameraShooting;
        return this;
    }

    public CameraShootingStrategy SetSensi(float sentivityXMouse, float sensivityYMouse, float sensivityXJoystick, float sensivityYJoystick) {
        _sensivityXMouse = sentivityXMouse;
        _sensivityYMouse = sensivityYMouse;
        _sensivityXJoystick = sensivityXJoystick;
        _sensivityYJoystick = sensivityYJoystick;
        return this;
    }

    //public CameraShootingStrategy SetOffset(float xOffset, float heightOffset, float distanceOffset) {
    //    //_xOffset = xOffset;
    //    //_heightOffset = heightOffset;
    //    //_distanceOffset = distanceOffset;

    //    _offsetShootingPosition = new Vector3(xOffset, heightOffset, -distanceOffset);
    //    return this;
    //} 

    public CameraShootingStrategy SetYAnglesMaxAndMin(float YangleMin, float YangleMax) {
        _YangleMin = YangleMin;
        _YangleMax = YangleMax;
        return this;
    }

    public void OnUpdate() {
        if (!MyInputManager.instance.useJoystick)
            UpdateXYInputs(_sensivityXMouse, _sensivityYMouse, _YangleMax, _YangleMin); 
        else
            UpdateXYInputs(_sensivityXJoystick, _sensivityYJoystick, _YangleMax, _YangleMin);

        //if (MyInputManager.instance.GetAxis("Aim") <= 0.0f)
            //MakeTransitionTo(TransitionType.ChangeToThird);
    }

    public void OnLateUpdate() {
        _camTransform.position = _positionOfCameraShooting.position + _rotation * _offsetShootingPosition;
        _camTransform.localEulerAngles = new Vector3(-_currentY, _currentX, 0);
    }

     void UpdateXYInputs(float sensiX, float sensiY,float angleMaxY,float angleMinY) {
        _currentX += MyInputManager.instance.GetAxis("CameraX") * sensiX;
        _currentY += MyInputManager.instance.GetAxis("CameraY") * sensiY; 

        _currentY = Mathf.Clamp(_currentY, angleMinY, angleMaxY);
    }
} 
