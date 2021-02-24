using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attacking : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent navmeshAgent;

    public GameObject target;

    [SerializeField]
    private float attackRange;

    bool isChasing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing)
        {
            navmeshAgent.SetDestination(target.transform.position);
        }
    }

    public void EnterAttackMode()
    {
        isChasing = true;
    }

    public void QuitAttackMode()
    {
        isChasing = false;
    }
}
