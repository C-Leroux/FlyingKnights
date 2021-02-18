using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------
//  In this state, the colossus simply wander around in a predefined Navmesh.
//  If it senses the player around, it'll become agressive and enter in
//  Attack state.
//------------------------------------------------------------------------
public class Wander : State<Colossus>
{
    private static Wander instance = null;

    public static Wander Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Wander();
            }
            return instance;
        }
    }

    public void Enter(Colossus colossus)
    {
        // Return to a valid point on the navmesh if necessary
    }

    public void Execute(Colossus colossus)
    {
        // The colossus moves according to the Navmesh

        // If a player is detected nearby, enter Attack state
        if (colossus.DetectPlayer())
            colossus.StateMachine.ChangeState(Attack.Instance);
    }

    public void Exit(Colossus colossus)
    {

    }

    // public bool OnMessage(Colossus colossus, const Telegram& msg);
}
