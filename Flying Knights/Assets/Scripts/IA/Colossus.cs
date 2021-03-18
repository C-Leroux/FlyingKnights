using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class Colossus : MonoBehaviour
{

    [SerializeField] private Wandering wandering;
    [SerializeField] private Attacking attacking;
    [SerializeField] private float rangeDetection;
    [SerializeField] private DetectPlayer leftDetect;
    [SerializeField] private DetectPlayer rightDetect;
    [SerializeField] private ParticleSystem HitFX;

    [HideInInspector] public bool isAttacking = false;
    [SerializeField] private Animator colossusAnim;

    [SerializeField] private Score scoreCounter;

    private float curReaction = 0;
    private StateMachine<Colossus> fsm;

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
        if(HP == 0)
        {
            Die();
            HP = -1;
        }

        if (curReaction > 0)
            curReaction -= Time.deltaTime;
    }

    #region Detection
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

    // Return true if the player enter the left collider
    public bool DetectLeft()
    {
        return leftDetect.IsPlayerDetected();
    }

    // Return true if the player enter the left collider
    public bool DetectRight()
    {
        return rightDetect.IsPlayerDetected();
    }
    #endregion

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
        colossusAnim.SetBool("Walk", true);
        attacking.EnterAttackMode();
    }

    public void StopAttacking()
    {
        colossusAnim.SetBool("Walk", false);
        attacking.QuitAttackMode();
    }

    // If the colossus is not in an attack animation, start a new one
    // If dir == 0 : left
    // If dir == 1 : right
    public void TryAttack(int dir)
    {
        if (curReaction > 0)
            return;
        if (dir == 0)
        {
            Debug.Log("Attack left");
            colossusAnim.SetTrigger("Attack");
            curReaction = reactionTime;
        }
        if (dir == 1)
        {
            Debug.Log("Attack right");
            colossusAnim.SetTrigger("Attack");
            curReaction = reactionTime;
        }
    }
    #endregion

    #region Setters

    //Fonction appelee quand le colosse recoit une attaque
    public void TakeDamage(float v)
    {
        HP -= v;
        HitFX.Play();
        if(HP < 0)
        {
            HP = 0;
        }
    }

    //Fonction appelee a la mort du colosse
    public void Die()
    {
        //TODO Apply Death function
        //Destroy(this.gameObject);     
        colossusAnim.SetTrigger("Die");
        scoreCounter.addScore(500);
    }

    #endregion
}
