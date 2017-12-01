using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateOnDying : MonoBehaviour, IState
{
    public FallingBehaviour _fall;
    public NoMovent _noMovent;
    public NoAction _noAction;
    private void Start()
    {
        StateMachine.instance.AddState(this);
    }

    void IState.setParam(object param)
    {
    }
    void IState.Begin()
    {
        PlayerBrain.instance.NormalPosition();
        PlayerBrain.instance.SetBehabiour(_noMovent, _fall, _noAction);
        PlayerAnimator.instance.StartDying();
      
        StartCoroutine(deathRoutine());
    }

    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.PlayerDie();
    }

    void IState.Finish()
    {
    }

    string IState.GetName()
    {
        return "Die";
    }

    string IState.NextState()
    {
        return null;
    }

    void IState.Process()
    {
    }
}
