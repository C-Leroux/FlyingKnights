using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<Agent_Type> : MonoBehaviour
{
    // The agent that owns this instance
    [SerializeField]
    private Agent_Type Owner
    {
        get;
    }

    // Current state of the agent
    public State<Agent_Type> CurrentState
    {
        get; set;
    }
    // Previous state of the agent
    public State<Agent_Type> PreviousState
    {
        get; set;
    }
    // Global state of the agent
    public State<Agent_Type> GlobalState
    {
        get; set;
    }

    // Called in order to update the FSM
    public void UpdateFSM()
    {
        //if a global state exists, call its execute method, else do nothing
        if (GlobalState != null)
            GlobalState.Execute(Owner);

        //same for the current state
        if (CurrentState != null)
            CurrentState.Execute(Owner);
    }

    // Change the current state
    public void ChangeState(State<Agent_Type> pNewState)
    {
        if (pNewState == null)
            return;

        // Record the previous state
        PreviousState = CurrentState;

        // Exit the previous state
        PreviousState.Exit(Owner);

        // Set the new state
        CurrentState = pNewState;

        // Enter the new state
        CurrentState.Enter(Owner);
    }

    // Return to the previous state
    public void RevertToPreviousState()
    {
        ChangeState(PreviousState);
    }

    // Check if the given state is the current state of the FSM
    public bool IsInState(State<Agent_Type> s)
    {
        if (CurrentState.GetType() == s.GetType())
            return true;
        return false;
    }
}
