using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public CameraBehaviur _camB;

    public static CameraManager instance { get; private set; }

    void Awake() {
        Debug.Assert(FindObjectsOfType<CameraManager>().Length == 1);
        if (instance == null)
            instance = this;
    }

    
    public void OnLoadCheckpoint(Vector3 localEulerAngles) {
        _camB.OnCheckpointLoad(localEulerAngles);
    }

    public void SetCinematic(Waypoint start) {
        _camB.ChangeToCinematic(start);
    }

    public void SetDollyPath(Waypoint start, Waypoint playerStart) { //, bool canGoBackwards) {
        _camB.ChangeToDollyPath(start, playerStart); //, canGoBackwards);
    }

    public void ChangeToThirdPerson() {
        _camB.ReturnToThirdPerson();
    }

    public void ChangeToFirstPerson() {
        _camB.ChangeStrategyFromThirdTo("FirstPerson");
    }

    public void ChangeToShooting() {
        _camB.ChangeStrategyFromThirdTo("Shooting");
    }

    public void ChangeToDeathFall() {
        _camB.ChangeToDeathFall();
    }
}
