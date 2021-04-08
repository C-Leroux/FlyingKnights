using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State<Colossus>
{
    private static DeathState instance = null;

    private GameObject playerObject = null;

    public static DeathState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DeathState();
            }
            return instance;
        }
    }

    public void Enter(Colossus colossus)
    {
        // Start Dying
        colossus.Die();
    }

    public void Execute(Colossus colossus)
    {
        if(!playerObject)
        {
            playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
        }
        if((playerObject.transform.position - colossus.transform.position).sqrMagnitude > (colossus.despawnDistance)*(colossus.despawnDistance))
        {
            colossus.FSM.ChangeState(WanderState.Instance);
        }
    }

    public void Exit(Colossus colossus)
    {
        colossus.Ressurect();
    }

    // public bool OnMessage(Colossus colossus, const Telegram& msg);
}
