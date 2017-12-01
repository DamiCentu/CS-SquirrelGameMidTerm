using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTreePath : MonoBehaviour {
    public TreePath tp;
    private bool activate = false;

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetKeyDown(KeyCode.E)&& !activate) {
            activate = true;
            tp.StartAnimation();
        }
    }
}
