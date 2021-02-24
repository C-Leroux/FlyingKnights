using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------
//  In this state, the colossus simply wander around in a predefined Navmesh.
//  If it senses the player around, it'll become agressive and enter in
//  Attack state.
//------------------------------------------------------------------------
public class WanderState : State<Colossus>
{
    private static WanderState instance = null;

    public static WanderState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new WanderState();
            }
            return instance;
        }
    }

    public void Enter(Colossus colossus)
    {
        // Return to a valid point on the navmesh if necessary

        // Start Wandering
        colossus.StartWandering();
    }

    public void Execute(Colossus colossus)
    {
        // The colossus moves according to the Navmesh

        // If a player is detected nearby, enter Attack state
        if (colossus.DetectPlayer())
            colossus.FSM.ChangeState(AttackState.Instance);
    }

    public void Exit(Colossus colossus)
    {
        // Stop Wandering
        colossus.StopWandering();
    }

    // public bool OnMessage(Colossus colossus, const Telegram& msg);
}
