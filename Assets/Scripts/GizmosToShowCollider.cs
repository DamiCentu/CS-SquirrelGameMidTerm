using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GizmosToShowCollider : MonoBehaviour {

    public bool showGizmosOfCollider = false;
    public Color colorOfGizmo = Color.blue;
    BoxCollider _box;

	void Awake () {
        _box = GetComponent<BoxCollider>();
	}

    void Start() {
        showGizmosOfCollider = true;
    }

    void OnDrawGizmos() {
        if (!showGizmosOfCollider)
            return;

        Gizmos.color = colorOfGizmo;
        if(_box != null)
            Gizmos.DrawWireCube(transform.position, _box.size);
    }
}
