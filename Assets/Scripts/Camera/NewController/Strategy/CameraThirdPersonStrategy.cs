using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraThirdPersonStrategy : ICameraStrategy {

    string _name = "ThirdPerson";
    float _currentX = 0f;
    float _currentY = 0f;
    Quaternion _rotation;
    Vector3 _offsetThirdPersonPosition;

    //Seteables
    float _maxDistanceOffset;
    float _minDistanceOffset;

    float _smoothAvoidance;
    LayerMask _maskToAvoid;

    float _distanceOffset; 

    float _YangleMin = 0f;
    float _YangleMax = 0f;

    float _sensivityXMouse = 0f; 
    float _sensivityYMouse = 0f;

    float _sensivityXJoystick = 0f;
    float _sensivityYJoystick = 0f;

    Transform _camTransform;
    Transform _squirrelThirdPersonLookObj;


    public string Name { get { return _name; } }

    public Vector3 TargetOfTransition { get { return _squirrelThirdPersonLookObj.position + _rotation * _offsetThirdPersonPosition; } }

    public CameraThirdPersonStrategy (string name) {
        _name = name;
    }

    public CameraThirdPersonStrategy SetTransforms(Transform camTransform, Transform squirrelThirdPersonLookObj) {
        _camTransform = camTransform;
        _squirrelThirdPersonLookObj = squirrelThirdPersonLookObj;
        return this;
    }

    public CameraThirdPersonStrategy SetSensi(float sentivityXMouse, float sensivityYMouse, float sensivityXJoystick, float sensivityYJoystick) {
        _sensivityXMouse = sentivityXMouse;
        _sensivityYMouse = sensivityYMouse;
        _sensivityXJoystick = sensivityXJoystick;
        _sensivityYJoystick = sensivityYJoystick;
        return this;
    }

    public CameraThirdPersonStrategy SetOffset(float xOffset, float heightOffset, float distanceOffset) { 
        _distanceOffset = distanceOffset;

        _offsetThirdPersonPosition = new Vector3(xOffset, heightOffset, -distanceOffset);
        return this;
    }

    public CameraThirdPersonStrategy SetAvoidanceProperties(float minDistanceToGetClose, float maxDistanceOffset, float smoothAvoidance, LayerMask maskToAvoid) { 
        _maxDistanceOffset = maxDistanceOffset;
        _minDistanceOffset = minDistanceToGetClose;
        _smoothAvoidance = smoothAvoidance;
        _maskToAvoid = maskToAvoid;
        return this;
    }

    public CameraThirdPersonStrategy SetYAnglesMaxAndMin(float YangleMin, float YangleMax) {
        _YangleMin = YangleMin;
        _YangleMax = YangleMax;
        return this;
    }

    public void OnUpdate() {
        if(!MyInputManager.instance.useJoystick)
            UpdateXYInputs(_sensivityXMouse, _sensivityYMouse, _YangleMax, _YangleMin); 
        else
            UpdateXYInputs(_sensivityXJoystick, _sensivityYJoystick, _YangleMax, _YangleMin);

        _offsetThirdPersonPosition = new Vector3(_offsetThirdPersonPosition.x, _offsetThirdPersonPosition.y, -_distanceOffset);
    }

    public void OnFixedUpdate() {
        Avoidance();

        if (_distanceOffset < _minDistanceOffset)
            _distanceOffset = _minDistanceOffset;

        if (_distanceOffset > _maxDistanceOffset)
            _distanceOffset = _maxDistanceOffset;
    }

    //float _timerDeactivateAvoidance = 0;
    //float _timeToDeativate = 0.1f;

    void Avoidance() {
        RaycastHit rh;
        if (Physics.Raycast(_camTransform.position, _camTransform.forward, out rh, Vector3.Distance(_camTransform.position, _squirrelThirdPersonLookObj.position), _maskToAvoid)
            || Physics.Raycast(_squirrelThirdPersonLookObj.position, -_camTransform.forward, out rh, _maxDistanceOffset, _maskToAvoid)) {
            //_timerDeactivateAvoidance = 0f;
            _distanceOffset -= Time.deltaTime * _smoothAvoidance;
        }
        else {
            //_timerDeactivateAvoidance += Time.deltaTime;
            //if(_timerDeactivateAvoidance > _timeToDeativate)
                _distanceOffset += Time.deltaTime * _smoothAvoidance;
        }
    }

    public void OnCheckPoint(Vector3 localEulerRot) {
        _currentY = localEulerRot.x;
        _currentX = localEulerRot.y;
    }

    public void OnLateUpdate() {
        //Avoidance();

        //if (_distanceOffset < _minDistanceOffset)
        //    _distanceOffset = _minDistanceOffset;

        //if (_distanceOffset > _maxDistanceOffset)
        //    _distanceOffset = _maxDistanceOffset;

        _rotation = Quaternion.Euler(-_currentY, _currentX, 0);
        _camTransform.position = _squirrelThirdPersonLookObj.position + (Vector3.one * 0.1f) + _rotation * _offsetThirdPersonPosition;
         
        _currentX = _currentX % 360;
        _camTransform.LookAt(_squirrelThirdPersonLookObj.position);
    }

    void UpdateXYInputs(float sensiX, float sensiY,float angleMaxY,float angleMinY) {
        _currentX += MyInputManager.instance.GetAxis("CameraX") * sensiX;
        _currentY += MyInputManager.instance.GetAxis("CameraY") * sensiY;

        _currentY = Mathf.Clamp(_currentY, angleMinY, angleMaxY);
    }
}
