using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colossus : MonoBehaviour
{
    private StateMachine<Colossus> fsm;

    [SerializeField]
    private Wandering wandering;
    [SerializeField]
    private Attacking attacking;
    [SerializeField]
    private float rangeDetection;

    [SerializeField] 
    private Animator colossusAnim;

    [HideInInspector]
    public bool isAttacking = false;

    #region Statistiques
    [SerializeField]
    private float HP;
    [SerializeField]
    private float Attack;
    [SerializeField]
    private float mouvementSpeed;
    [SerializeField]
    private float reactionTime;
    #endregion
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
        colossusAnim = this.GetComponent<Animator>();    // bool: Walk; trigger: Die, Attack;
    }

    private void FixedUpdate()
    {
        FSM.UpdateFSM();
    }

    // Return true if the player is within the sphere detection of the colossus
    public bool DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, rangeDetection);
        foreach(Collider col in colliders)
        {
            if (col.tag == "Player")
            {
                attacking.target = col.gameObject;
                return true;
            }
        }
        return false;
    }

    #region Wandering
    public void StartWandering()
    {
        colossusAnim.SetBool("Walk", true);
        wandering.StartWandering();
    }

    public void StopWandering()
    {
        colossusAnim.SetBool("Walk", false);
        wandering.StopWandering();
    }

    #endregion

    #region Attacking

    public void StartAttacking()
    {
        attacking.EnterAttackMode();
    }

    public void StopAttacking()
    {
        attacking.QuitAttackMode();
    }
    #endregion
}
