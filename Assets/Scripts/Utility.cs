using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility {  

    public static LayerMask LayerNumberToMask(int layerNum) {
       return 1 << layerNum;
    }

    public static Vector3 TruncateFromSteering(Vector3 vec, float maxMagnitude) {
        var magnitude = vec.magnitude;
        return vec * Mathf.Min(1f, maxMagnitude / magnitude);
    }

    public static Vector3 RandomDirection() {
		var theta = Random.Range(0f, 2f*Mathf.PI);
		var phi = Random.Range(0f, Mathf.PI);
		var u = Mathf.Cos(phi);
		return new Vector3(Mathf.Sqrt(1-u*u)*Mathf.Cos(theta), Mathf.Sqrt(1-u*u)*Mathf.Sin(theta), u);
	}

    public static bool InRange(Vector3 position,Vector3 target, float distance) {
        return Vector3.Distance(position, target) < distance;
    }

    //position - transform.position).sqrMagnitude<radius* radius;
}
