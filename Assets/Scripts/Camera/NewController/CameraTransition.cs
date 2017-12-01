using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraTransition { //: MonoBehaviour {

    //public static bool transitioning;
    //public static CameraTransition instance { get; private set; }

    //void Awake() {
    //    Debug.Assert(FindObjectsOfType<CameraTransition>().Length == 1);
    //    if (instance == null)
    //        instance = this;
    //}

    public static void MakeTransition(Vector3 target, Transform objToTransitionate, float speedOfTransition) {
        var toTransitionPoint = target - objToTransitionate.position;
        var direction = toTransitionPoint.normalized;
        var movementDelta = direction * speedOfTransition * Time.deltaTime;
        var adjustedMovementDelta = movementDelta.sqrMagnitude > toTransitionPoint.sqrMagnitude ? toTransitionPoint : movementDelta;

        objToTransitionate.position += adjustedMovementDelta;
    }
}
