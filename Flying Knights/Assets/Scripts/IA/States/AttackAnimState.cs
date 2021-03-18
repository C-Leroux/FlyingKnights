using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//------------------------------------------------------------------------
//  In this state, the colossus do an attack animation, and thus can't
//  move until it's done.
//------------------------------------------------------------------------
public class AttackAnimState : State<Colossus>
{
    private static AttackAnimState instance = null;

    public static AttackAnimState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AttackAnimState();
            }
            return instance;
        }
    }

    public void Enter(Colossus colossus)
    {
        colossus.AttackAnim();
        colossus.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

    }

    public void Execute(Colossus colossus)
    {
        if (colossus.IsRecovered())
            colossus.FSM.ChangeState(AttackState.Instance);
    }

    public void Exit(Colossus colossus)
    {
        colossus.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

    }

    // public bool OnMessage(Colossus colossus, const Telegram& msg);
}