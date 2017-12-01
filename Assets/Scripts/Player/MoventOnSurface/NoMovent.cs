using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMovent : MonoBehaviour, IMoventOnSurface
{
    void IMoventOnSurface.Active()
    {

    }

    Vector3 IPositionPredictable.currentDirection()
    {
        return Vector3.zero;
    }

    void IMoventOnSurface.Move()
    {
    }
}
