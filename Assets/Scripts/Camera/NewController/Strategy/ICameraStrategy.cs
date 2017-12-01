using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraStrategy { 
    string Name {get;}
    void OnUpdate();
    void OnLateUpdate();
    Vector3 TargetOfTransition {get;}
}
