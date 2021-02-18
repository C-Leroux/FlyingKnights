using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wondering : MonoBehaviour
{
    float walkSpeed = 5f;
    bool isWondering = false;
    public NavMeshAgent navmeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWondering)
        {
            StartCoroutine(wonder());
        }
    }

    IEnumerator wonder()
    {
        isWondering = true;
        float walkingTime = Random.Range(0, 5);
        navmeshAgent.SetDestination(RandomNavmeshLocation(walkingTime * walkSpeed));
        yield return new WaitForSeconds(walkingTime);
        isWondering = false;
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
}
