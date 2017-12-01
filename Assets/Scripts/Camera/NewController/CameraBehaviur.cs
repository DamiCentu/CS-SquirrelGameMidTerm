using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviur : MonoBehaviour { 
    [Header("OffsetOfPositionOfCameraThird")]
    public float thirdPersonDistance = 4f;
    public float thirdPersonHeight = 0f;
    public float thirdPersonOffsetX = 0f;

    [Header("AngleMaxToClampThirdPersonY")]
    public float thirdPersonYangleMin = -50f;
    public float thirdPersonYangleMax = 14f;

    [Header("MouseSensityThirdPerson")]
    [Range(0.3f, 20f)]
    public float thirdPersonMouseSensivityX = 3f;
    [Range(0.3f, 20f)]
    public float thirdPersonMouseSensivityY = 1f;

    [Header("JoystickSensityThirdPerson")]
    [Range(0.3f, 20f)]
    public float thirdPersonJoystickSensivityX = 3f;
    [Range(0.3f, 20f)]
    public float thirdPersonJoystickSensivityY = 1f;

    [Header("TransformToLookAt")]
    public Transform squirrelThirdPersonLookObj;

    [Header("AvoidanceProperties")]
    public LayerMask maskToAvoid;
    public float smoothOfAvoidance = 20f;
    public float minDistanceToGetClose = 1f;

    [Header("------------------------------------------------------------")]

    //[Header("OffsetOfPositionOfCameraShooting")]
    //public float distanceShooting = 0f;
    //public float heightShooting = 0f;
    //public float offsetXShooting = 0f;

    [Header("AngleMaxToClampShootingY")]
    public float YShootingAngleMin = -10f;
    public float YShootingAngleMax = 25f;

    [Header("SensityShootingMouse")]
    [Range(0.3f, 20f)]
    public float mouseSensivityShootingX = 0.6f;
    [Range(0.3f, 20f)]
    public float mouseSensivityShootingY = 0.6f;

    [Header("SensityShootingJoystick")]
    [Range(0.3f, 20f)]
    public float joystickSensivityShootingX = 0.6f;
    [Range(0.3f, 20f)]
    public float joystickSensivityShootingY = 0.6f;

    [Header("TransformToFollow")] 
    public Transform positionOfCameraShooting;

    [Header("------------------------------------------------------------")]

    [Header("AngleMaxToClampFirstPersonY")]
    public float YFirstPersonAngleMin = -50f;
    public float YFirstPersonAngleMax = 50f;

    [Header("AngleMaxToClampFirstPersonX")]
    public float firstPersonangleMinX = -50;
    public float firstPersonangleMaxX = 50;

    [Header("SensityFirstPerson")]
    [Range(0.3f, 20f)]
    public float mouseSensivityFirstPersonX = 1f;
    [Range(0.3f, 20f)]
    public float mouseSensivityFirstPersonY = 1f;

    [Header("SensityFirstPerson")]
    [Range(0.3f, 20f)]
    public float joystickSensivityFirstPersonX = 1f;
    [Range(0.3f, 20f)]
    public float joystickSensivityFirstPersonY = 1f;

    [Header("TransformToFollow")] 
    public Transform positionOfFirstPersonCamera;
    public Transform player;

    [Header("------------------------------------------------------------")]
    [Header("SpeedOfTransitions")]
    public float speedOfTransition = 20f;
    public float radiusOfPointOfTransition = 1f;

    [Header("CinematicTransformToLookAt")]
    public Transform cinematicTransformToLookAt;

    [Header("DollyPathValues")] 
    public Transform dollyPathTransformToLookAt;
    public float rangeToChangeWaypoint = 0.03f;
    public float smoothLookAt = 5f;

    //public delegate void transitionFunc(Vector3 target, Transform objToTransitionate, float speedOfTransition);

    //public transitionFunc func;

    ICameraStrategy _current;

    Dictionary<string, ICameraStrategy> _dicStrategies;

    CameraThirdPersonStrategy _thirdPerson;
    CameraCinematicStrategy _cinematic;
    CameraDollyPathStrategy _dollyPath;

    void Awake() {
        _dicStrategies = new Dictionary<string, ICameraStrategy>();
        _thirdPerson = new CameraThirdPersonStrategy("ThirdPerson").SetTransforms(transform, squirrelThirdPersonLookObj)
                                                                   .SetOffset(thirdPersonOffsetX, thirdPersonHeight, thirdPersonDistance)
                                                                   .SetYAnglesMaxAndMin(thirdPersonYangleMin,thirdPersonYangleMax)
                                                                   .SetSensi(thirdPersonMouseSensivityX, thirdPersonMouseSensivityY, thirdPersonJoystickSensivityX, thirdPersonJoystickSensivityY)
                                                                   .SetAvoidanceProperties(minDistanceToGetClose, thirdPersonDistance, smoothOfAvoidance, maskToAvoid);
        //setNewStrategyInDictionary("ThirdPerson", _thirdPerson);

        setNewStrategyInDictionary("Shooting", new CameraShootingStrategy("Shooting").SetTransforms(transform, positionOfCameraShooting)
                                                                                     //.SetOffset(offsetXShooting, heightShooting, distanceShooting)
                                                                                     .SetYAnglesMaxAndMin(YShootingAngleMin, YShootingAngleMax)
                                                                                     .SetSensi(mouseSensivityShootingX, mouseSensivityShootingY, joystickSensivityShootingX, joystickSensivityShootingY));

        setNewStrategyInDictionary("FirstPerson", new CameraFirstPersonStrategy("FirstPerson").SetTransforms(transform, positionOfFirstPersonCamera, player)
                                                                                     .SetXAnglesMaxAndMin(firstPersonangleMinX, firstPersonangleMaxX)
                                                                                     .SetYAnglesMaxAndMin(YFirstPersonAngleMin, YFirstPersonAngleMax)
                                                                                     .SetSensi(mouseSensivityFirstPersonX, mouseSensivityFirstPersonY, joystickSensivityFirstPersonX, joystickSensivityFirstPersonY));

        _cinematic = new CameraCinematicStrategy("Cinematic").setTransforms(transform, cinematicTransformToLookAt);

        setNewStrategyInDictionary("DeathFall", new CameraDeathFallStrategy("DeathFall").SetTransforms(transform, player));
        
        _dollyPath = new CameraDollyPathStrategy("DollyPath").setTransforms(transform, dollyPathTransformToLookAt).setRangesToChangeWaypoints(rangeToChangeWaypoint).SetSmoothLookAt(smoothLookAt);

        _current = _thirdPerson; 

        //_current = getStrategyInDictionary("Shooting");
        //_current = getStrategyInDictionary("FirstPerson");
    }

    void setNewStrategyInDictionary(string key, ICameraStrategy stra) {
        if (_dicStrategies.ContainsKey(key))
            throw new System.Exception("ya hay una key con ese nombre");
        else
            _dicStrategies[key] = stra;
    }

    ICameraStrategy getStrategyInDictionary(string key) {
        if (_dicStrategies.ContainsKey(key))
            return _dicStrategies[key];
        throw new System.Exception("No existe esa key");
    }
    
	 
	void Update () {
        //Debug.Log(_current);

        if(!_transitioning)
            _current.OnUpdate();

        if (_current.Name == _cinematic.Name && !_cinematic.isInCinematic())
            ReturnToThirdPerson();

    }

    void FixedUpdate() {
        if (_current.Name == _thirdPerson.Name && !_transitioning)
            _thirdPerson.OnFixedUpdate();
    }

    void LateUpdate() {
        if (!_transitioning)
            _current.OnLateUpdate();
        else { 
            CameraTransition.MakeTransition(_positionToChange, transform, speedOfTransition);
            if (Utility.InRange(transform.position, _positionToChange, radiusOfPointOfTransition))
                _transitioning = false;
        }
    }

    Vector3 _positionToChange;
    bool _transitioning;

    public void ChangeStrategyFromThirdTo(string name) {
        if (name == _cinematic.Name)
            return;
        if (_current.Name == _thirdPerson.Name) {
            var s = getStrategyInDictionary(name);
            //func = makeTransition; 
            _transitioning = true;
                
            _positionToChange = s.TargetOfTransition;
            _current = s;
        }
    }

    public void ChangeToDeathFall() {
        _current = getStrategyInDictionary("DeathFall");
    }

    public void ChangeToDollyPath(Waypoint start, Waypoint playerStart) {//, bool canGoBackWards) {
        _dollyPath.setPath(start, playerStart);//, canGoBackWards);
        _current = _dollyPath;
    }

    public void ChangeToCinematic(Waypoint start) {
        _cinematic.setPath(start);
        _current = _cinematic; 
    }

    public void ReturnToThirdPerson() {
        if (_current.Name == _cinematic.Name && _cinematic.isInCinematic())
            return;

        //_transitioning = true
        _positionToChange = _thirdPerson.TargetOfTransition;
        _current = _thirdPerson;
    }

    void OnDrawGizmos() {
        if (_dollyPath != null && _current == _dollyPath)
            _dollyPath.onGizmos();
    }

    public void OnCheckpointLoad(Vector3 localEulerAngles) {
        _thirdPerson.OnCheckPoint(localEulerAngles);
    }
}
