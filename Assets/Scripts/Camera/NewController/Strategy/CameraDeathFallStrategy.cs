using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDeathFallStrategy : ICameraStrategy {
    string _name = "DeathFall";

    Transform _camTransform;
    Transform _squirrel;

    public string Name { get { return _name; } }

    public Vector3 TargetOfTransition { get { return Vector3.zero; } }

    public CameraDeathFallStrategy(string name) {
        _name = name;
    }

    public CameraDeathFallStrategy SetTransforms(Transform camTransform, Transform squirrel) {
        _camTransform = camTransform;
        _squirrel = squirrel;
        return this;
    }

    public void OnUpdate() { }

    public void OnLateUpdate() { 
        //_camTransform.position = _squirrelThirdPersonLookObj.position + _rotation * _offsetThirdPersonPosition;

        _camTransform.LookAt(_squirrel.position);
    }
}
