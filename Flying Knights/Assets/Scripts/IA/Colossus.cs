using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colossus : MonoBehaviour
{
    private StateMachine<Colossus> fsm;

    [SerializeField]
    private Wandering wandering;

    [SerializeField]
    private float rangeDetection;

    public StateMachine<Colossus> FSM
    {
        get
        {
            return fsm;
        }
        set
        {
            fsm = value;
        }
    }

    private void Start()
    {
        fsm = new StateMachine<Colossus>(this); ;
        fsm.ChangeState(WanderState.Instance);
    }

    // Return true if the player is within the sphere detection of the colossus
    public bool DetectPlayer()
    {
        if (Physics.OverlapSphere(transform.position, rangeDetection, LayerMask.GetMask("Player"))[0])
        {
            return true;
        }

        return false;
    }

    public void StartWander()
    {
        wandering.StartWandering();
    }

    public void StopWandering()
    {
        wandering.StopWandering();
    }
}
