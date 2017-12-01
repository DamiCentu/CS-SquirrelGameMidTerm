using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDollyPathStrategy : ICameraStrategy {
    string _name = "DollyPath";

    Waypoint _cameraCurrentWP;
    Waypoint _playerCurrentWP;

    //bool _canGoBackwards;

    Transform _camTransform;
    Transform _objToLookAtDollyPath;

    float _rangeToChangeWaypoint = 0.03f;
    float _smoothLookAtValue = 5f;

    //float _fullDistanceBetweenCurrentAndNext;
    float _currentProgressBetweenCurrentAndNext = 10f;
    //float _currentProgressBetweenCurrentAndPrev = 0f;

    public string Name { get { return _name; } }
    public Vector3 TargetOfTransition { get { return _cameraCurrentWP.transform.position; } }

    public CameraDollyPathStrategy(string name) {
        _name = name;
    }

    public CameraDollyPathStrategy setTransforms(Transform camTransform, Transform objToLookAtDollyPath) {
        _camTransform = camTransform;
        _objToLookAtDollyPath = objToLookAtDollyPath;
        return this;
    }

    public CameraDollyPathStrategy SetSmoothLookAt(float smoothLookAtValue) {
        _smoothLookAtValue = smoothLookAtValue;
        return this;
    }

    public CameraDollyPathStrategy setRangesToChangeWaypoints(float rangeToChangeWaypoint) {
        _rangeToChangeWaypoint = rangeToChangeWaypoint;
        return this;
    }

    public void setPath(Waypoint cameraStart, Waypoint playerWaypointStart) {//, bool canGoBackwards) {
        _cameraCurrentWP = cameraStart;
        _playerCurrentWP = playerWaypointStart;
    }

    pos aheadOrBehindCurrent;
    pos aheadOrBehindNext;
    //pos aheadOrBehindPrev;
    Vector3 lookAtTarget = new Vector3();

    public void OnUpdate() {
        if (_playerCurrentWP.next != null) {
            var dirToNextWP = _playerCurrentWP.next.transform.position - _playerCurrentWP.transform.position;

            //saco un vector perpendicular a la direccion del current al next y el up del mundo
            var cross = Vector3.Cross(dirToNextWP, Vector3.up);

            //saco un punto para el current y el next sobre el vector cross en el cual el player esta parado y sacamos de la ecuacion el eje Y
            var nearestPointOnLineCurrent = NearestPointOnLine(cross, _objToLookAtDollyPath.position, _playerCurrentWP.transform.position);
            nearestPointOnLineCurrent.y = 0f;
            var nearestPointOnLineNext = NearestPointOnLine(cross, _objToLookAtDollyPath.position, _playerCurrentWP.next.transform.position);
            nearestPointOnLineNext.y = 0f;

            //saco la distancia entre el current y el next
            var _fullDistanceBetweenCurrentAndNext = Vector3.Distance(nearestPointOnLineCurrent, nearestPointOnLineNext);
            //sacamos aca tambien de la ecuacion el eje Y
            var posPlayerWhitowY = new Vector3(_objToLookAtDollyPath.position.x, 0f, _objToLookAtDollyPath.position.z);
            //saco la distancia entre el player y el punto sobre el current waypoint y no la normalizo
            var distanceNoNormalized = Vector3.Distance(nearestPointOnLineCurrent, posPlayerWhitowY);

            //normalizo la distancia recorrida para que me de un numero entre 0 y 1
            _currentProgressBetweenCurrentAndNext = distanceNoNormalized / _fullDistanceBetweenCurrentAndNext;

            //seteo cual va a ser el punto a mirar sobre la linea de waypoints del player(NO DE LA CAMARA)


            if (aheadOrBehindCurrent == pos.Ahead)
                lookAtTarget = _playerCurrentWP.transform.position + dirToNextWP.normalized * distanceNoNormalized;

            //me fijo de que lado del waypoint esta si delante o detras
            aheadOrBehindCurrent = AngleDir(cross, _playerCurrentWP.transform.position - _objToLookAtDollyPath.transform.position, Vector3.up);
            aheadOrBehindNext = AngleDir(cross, _playerCurrentWP.next.transform.position - _objToLookAtDollyPath.transform.position, Vector3.up); 
        }
        else {
            //saco la direccion para atras y pregunto si es null para poder hacer el camino hacia atras
            if(_playerCurrentWP.prev != null) { 
                var dirToPrevWP = _playerCurrentWP.prev.transform.position - _playerCurrentWP.transform.position;
                var cross = Vector3.Cross(dirToPrevWP, Vector3.up);
                aheadOrBehindCurrent = AngleDir(cross, _playerCurrentWP.transform.position - _objToLookAtDollyPath.transform.position, Vector3.up);
            }
        }
    }
    
    public void OnLateUpdate() { 
        var targetRotation = Quaternion.LookRotation((lookAtTarget + _objToLookAtDollyPath.transform.position)/2 - _camTransform.position);
        // Smoothly rotate towards the target point.
        _camTransform.rotation = Quaternion.Slerp(_camTransform.rotation, targetRotation, _smoothLookAtValue * Time.deltaTime);

        if (_cameraCurrentWP.next != null && _playerCurrentWP.next != null) {

            //_currentProgressBetweenCurrentAndNext = Mathf.Sin(_currentProgressBetweenCurrentAndNext * Mathf.PI * 0.5f); 
            //if(leftOrRight != 1)
            if(aheadOrBehindCurrent != pos.Behind)
                _camTransform.position = Vector3.Lerp(_cameraCurrentWP.transform.position, _cameraCurrentWP.next.transform.position, _currentProgressBetweenCurrentAndNext);

            if (aheadOrBehindNext == pos.Ahead) {
                _cameraCurrentWP = _cameraCurrentWP.next;
                _playerCurrentWP = _playerCurrentWP.next;
                _currentProgressBetweenCurrentAndNext = 0f;
            }
        }

        if (_cameraCurrentWP.prev != null && _playerCurrentWP.prev != null) {
            if (aheadOrBehindCurrent == pos.Behind) {
                _cameraCurrentWP = _cameraCurrentWP.prev;
                _playerCurrentWP = _playerCurrentWP.prev;
                _currentProgressBetweenCurrentAndNext = 1f;
            }
        } 
    }

    // lineDirection - unit vector in direction of line
    // pointOnLine - a point on the line (allowing us to define an actual line in space)
    // point - the point to find nearest on line for

    Vector3 NearestPointOnLine(Vector3 lineDirection, Vector3 point, Vector3 pointOnLine) {
        lineDirection.Normalize();
        var d = Vector3.Dot(point - pointOnLine, lineDirection);
        return pointOnLine + (lineDirection * d);
    }

    //You need to supply a forward direction, the direction that you want to check for left/right of forward and an up direction. 
    //returns -1 when to the left, 1 to the right, and 0 for forward/backward
    public pos AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0f) 
            return pos.Behind; 

        else if (dir < 0.0f)
            return pos.Ahead;

        else
            return pos.Middle;
    }

    public void onGizmos() {
        Gizmos.color = Color.blue; 
        Gizmos.DrawWireSphere(lookAtTarget,1f);

        if(_playerCurrentWP.transform.position != null) { 
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_playerCurrentWP.transform.position, 2f);
        }
    }

    public enum pos {
        Ahead,
        Behind,
        Middle
    }
}
