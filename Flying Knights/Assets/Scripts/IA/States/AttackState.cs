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
        else
        {
            // If the player enter a collider, initiate an attack
            if (colossus.DetectLeft())
            {
                // Attack left
                Attack(colossus);
            }
            else if (colossus.DetectRight())
            {
                // Attack right
                Attack(colossus);
            }

            // Check if colossus is dead
            if (colossus.IsDead())
                colossus.FSM.ChangeState(DeathState.Instance);
        }
    }


    // If the colossus is not in an attack animation, start a new one
    // If dir == 0 : left
    // If dir == 1 : right
    public void Attack(Colossus colossus)
    {
        colossus.FSM.ChangeState(AttackAnimState.Instance);
    }

    public void Exit(Colossus colossus)
    {
        // Animation indicating that the colossus has lost track of the player
        colossus.StopAttacking();
    }

    // public bool OnMessage(Colossus colossus, const Telegram& msg);
}