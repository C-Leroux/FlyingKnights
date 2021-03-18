using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : MonoBehaviour
{
    private static DeathState instance = null;

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

    }

    public void Exit(Colossus colossus)
    {

    }

    // public bool OnMessage(Colossus colossus, const Telegram& msg);
}
