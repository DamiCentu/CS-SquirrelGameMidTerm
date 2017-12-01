using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCinematicStrategy : ICameraStrategy {
    string _name = "Cinematic";
    Waypoint _current;
    Transform _camTransform;
    Transform _objToLookAtInCinematic;

    public string Name { get { return _name; } }
    public Vector3 TargetOfTransition { get { return _current.transform.position; } }

    public CameraCinematicStrategy(string name) {
        _name = name;
    }

    public CameraCinematicStrategy setTransforms(Transform camTransform, Transform objToLookAtInCinematic) {
        _camTransform = camTransform;
        _objToLookAtInCinematic = objToLookAtInCinematic;
        return this;
    } 

    public void setPath(Waypoint start) {
        _current = start;
    }
    //hacer bool que devuelva que se esta en cinematica transisionando, hacer un manager para cambiar de strategy
    public void OnUpdate () {
        if (Utility.InRange(_camTransform.position, _current.transform.position, _current.radius))
            _current = _current.next;
    }

    public void OnLateUpdate() {
        if(_current != null)
            CameraTransition.MakeTransition(_current.transform.position, _camTransform, _current.speedToNextWP);
        _camTransform.LookAt(_objToLookAtInCinematic);
    }

    public bool isInCinematic() {
        if (_current != null)
            return true;
        return false;
    }
}
