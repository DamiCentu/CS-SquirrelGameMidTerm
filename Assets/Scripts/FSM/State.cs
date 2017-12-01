using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<I>
{
    public string Id { get; private set; }
    public FSM<I> Fsm { get; private set; }
    public Dictionary<I, State<I>> transitions = new Dictionary<I, State<I>>();
    public State(FSM<I> fsm, string id) { Fsm = fsm; Id = id; }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void OnUpdate() { }
}
