using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTreePath : MonoBehaviour {

    public TreePath tp;
    private bool activate = false;

    private void OnCollisionStay(Collision collision)
    {
        if ( !activate)
        {
            activate = true;
          //  tp.StopAnimation();
        }
    }
}

