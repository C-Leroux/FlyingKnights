using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wandering : MonoBehaviour
{
    float walkSpeed = 5f;
    bool enable = false;
    bool isWandering = false;
    private NavMeshAgent navmeshAgent;
    

    // Start is called before the first frame update
    void Start()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        walkSpeed = navmeshAgent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWandering && enable)
        {
            StartCoroutine(Wander());
        }
    }

    IEnumerator Wander()
    {
        isWandering = true;
        float walkingTime = Random.Range(0, 15);
        navmeshAgent.SetDestination(RandomNavmeshLocation(walkingTime * walkSpeed));
        yield return new WaitForSeconds(walkingTime);
        isWandering = false;
    }

    Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public void StartWandering()
    {
        enable = true;
    }

    public void StopWandering()
    {
        enable = false;
        isWandering = false;
        StopCoroutine(Wander());
    }
}
