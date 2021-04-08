using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class Colossus : MonoBehaviour
{
    [SerializeField] private float rangeDetection;
    [SerializeField] private DetectPlayer leftDetect;
    [SerializeField] private DetectPlayer rightDetect;
    [SerializeField] private ParticleSystem HitFX;

    [HideInInspector] public bool isAttacking = false;
   

    [HideInInspector] private bool frozen = false;
    [HideInInspector] private float frozenTimer = 0;
    [HideInInspector] private State<Colossus> OldState;
    Rigidbody rb;

    [SerializeField] private Score scoreCounter;
    [SerializeField] private GameObject attackAOE = null;
    [SerializeField] private float attackDelay = 0f;
    [SerializeField] private float attackEndDelay = 0f;

    private float curReaction = 0;
    private StateMachine<Colossus> fsm;

    #region Statistiques
    [SerializeField]
    private float maxHP;
    [SerializeField]
    private float Attack;
    [SerializeField]
    private float reactionTime;
    #endregion

    private Animator colossusAnim = null;
    private Wandering wandering;
    private Attacking attacking;
    private EnnemySpawn spawner;
    private float HP;
    [SerializeField] public readonly float despawnDistance = 250f;

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
        colossusAnim = this.GetComponent<Animator>();  
        wandering = this.GetComponent<Wandering>();  
        attacking = this.GetComponent<Attacking>();
        spawner = this.GetComponent<EnnemySpawn>();
        HP = maxHP;

        ///
        fsm = new StateMachine<Colossus>(this);
        fsm.ChangeState(WanderState.Instance);
    }

    private void FixedUpdate()
    {
       

        if (frozen)
        {
            frozenTimer += Time.deltaTime;
            if (frozenTimer > 5)
            {
                Unfreeze();
            }
        }
        else
        {
            FSM.UpdateFSM();
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
        Debug.Log(FSM.CurrentState);
        
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
        colossusAnim.SetTrigger("Die");
        colossusAnim.SetBool("Dead",true);
        scoreCounter.addScore(500);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Ressurect()
    {
        HP = maxHP;
        colossusAnim.ResetTrigger("Die");
        colossusAnim.SetBool("Dead",false);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        spawner.Spawn();
    }

    public bool IsDead()
    {
        return HP == 0;
    }

    #endregion

    public void Freeze()
    {
        if (!frozen)
        {
            frozen = true;
            colossusAnim.enabled = false;
           
            OldState = fsm.CurrentState;
            if (OldState == WanderState.Instance)
            {
                StopWandering();
                wandering.StopComplete();
            }
            if (OldState == AttackState.Instance)
            {
                StopAttacking();
            }

            frozenTimer = 0;
            Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
            m_Rigidbody.freezeRotation = true;
            m_Rigidbody.velocity = Vector3.zero;
            //Debug.Log(FSM.CurrentState);
        }   
    }

    private void Unfreeze()
    {
        frozen = false;

        colossusAnim.enabled = true;
        if (OldState == WanderState.Instance)
        {
            StartWandering();
        }
        if (OldState == AttackState.Instance)
        {
            StartAttacking();
        }
        OldState = null;
        frozenTimer = 0;
    }
}

