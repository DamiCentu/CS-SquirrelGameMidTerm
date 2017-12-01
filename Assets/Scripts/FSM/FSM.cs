using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<I>
{
    State<I> _current;

    public FSM()
    {
    }

    public void SetInitial(State<I> initial)
    {
        _current = initial;
        _current.OnEnter();
    }

    public State<I> Current
    {
        get { return _current; }
    }

    public void ProcessInput(I input)
    {
        if (_current.transitions.ContainsKey(input))
        {
            ChangeState(_current.transitions[input]);
        }
    }


    public void ChangeState(State<I> newState)
    {
        _current.OnExit();
        _current = newState;
        _current.OnEnter();
    }

    public void Update()
    {
        _current.OnUpdate();
    }

}