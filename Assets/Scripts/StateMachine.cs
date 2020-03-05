using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class StateMachine : System.Object {
    private Dictionary<string, System.Action<StateType>> states = new Dictionary<string, System.Action<StateType>>();
    private string stateId = string.Empty;
    private System.Action<StateType> current = null;

    public string StateId {
        get {
            //return the stateId
            return this.stateId;
        } set {
            //attempt to transition to the requested state
            this.Transition(value);
        }
    }

    protected virtual void Transition(string state) {
        //if the state requested is null or empty, return
        if (string.IsNullOrEmpty(state)) return;

        //if the current state and the requested state are the same, return
        if (string.Compare(this.StateId, state) == 0) return;

        //if the requested state has not been registered with this state machine, return
        if (this.states.ContainsKey(state) == false) return;

        /*
        if (string.IsNullOrEmpty(this.StateId) == false) {
            //notify the current state that it is about to exit
            this.states[this.StateId].Invoke(StateType.Exit);
        } */

        //notify the current state it is about to exist
        this.current?.Invoke(StateType.Exit);

        //lowercase or else we'll hit an infinite loop
        //change the internal stateId to the requested /valid/ state
        this.stateId = state;

        //grab the current state
        this.current = this.states[this.StateId];

        //this.states[this.StateId].Invoke(StateType.Enter);
        //notify the current state we are about to enter
        this.current.Invoke(StateType.Enter);
    }

    public virtual void Add(string stateId, System.Action<StateType> state) {
        if (this.states.ContainsKey(stateId) == false) {
            this.states.Add(stateId, null);
        }

        this.states[stateId] = state;
    }

    public virtual void remove(string stateId) {
        this.states.Remove(stateId);
    }

    public virtual void Update() {
        /*
        if (this.current == null) return;
        this.current.Invoke(StateType.Update); */

        //update the state machine
        this.current?.Invoke(StateType.Update);
    }

    public enum StateType {
        Unknown = 0,
        Enter = 1,
        Update = 2,
        Exit = 3
    }
}