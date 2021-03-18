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
    [SerializeField] private GameObject attackAOE = null;
    [SerializeField] private float attackDelay = 0f;
    [SerializeField] private float attackEndDelay = 0f;

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

    public void AttackAnim()
    {
        colossusAnim.SetTrigger("Attack");
        Invoke("activateAOE",attackDelay);
        Invoke("deActivateAOE",attackEndDelay);
        curReaction = reactionTime;
    }

    private void activateAOE()
    {
        attackAOE.SetActive(true);
        
    }
    private void deActivateAOE()
    {
        attackAOE.SetActive(false);

    }

    public bool IsRecovered()
    {
        return curReaction < 0;
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
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    public bool IsDead()
    {
        return HP == 0;
    }

    #endregion
}
