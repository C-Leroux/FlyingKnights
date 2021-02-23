using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------
//  In this state, the colossus move in the direction of the player and
//  attack him if he enters a collider box. During an attack, the
//  colossus stop moving and analysing the player's position.
//  If the colossus lost track of the player, it'll enter in Wander state.
//------------------------------------------------------------------------
public class AttackState : State<Colossus>
{
    private static AttackState instance = null;

    public static AttackState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AttackState();
            }
            return instance;
        }
    }

    public void Enter(Colossus colossus)
    {
        // Animation indicating that the colossus has detected the player
        colossus.StartAttacking();
    }

    public void Execute(Colossus colossus)
    {
        // If not attacking, move in direction of the player

        // If player no longer detected, enter Wander state
        if (!colossus.DetectPlayer())
            colossus.FSM.ChangeState(WanderState.Instance);

        // If the player enter a collider, initiate an attack
    }

    public void Exit(Colossus colossus)
    {
        // Animation indicating that the colossus has lost track of the player
        colossus.StopAttacking();
    }

    // public bool OnMessage(Colossus colossus, const Telegram& msg);
}