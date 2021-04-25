using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemySpawn : MonoBehaviour
{
    [Tooltip("An object whose children will be the spawnPoints")]
    [SerializeField] private GameObject spawnList = null;

    [Tooltip("If set to true spawn will not be random and instead the colossus will spawn here it lies in the editor, only for first spawn")]
    [SerializeField] private bool skipSpawn = false;
    
    [Tooltip("The random radius around the spawn points where the ennemy might spawn")]
    [SerializeField] private float spawnRadius = 50f;
    
    [Tooltip("The minimum distance a colossus can spawn from the player")]
    [SerializeField] private float safeDistance = 500f;

    private NavMeshAgent localAgent;
    // Start is called before the first frame update
    void Start()
    {
        localAgent = GetComponent<NavMeshAgent>();
        if(!skipSpawn)
        {
            Spawn();
        }
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

    // Update is called once per frame
    public void Spawn()
    {
        Vector3 pointToSpawn = spawnList.transform.GetChild(Random.Range(0,spawnList.transform.childCount)).transform.position;
        Vector3 playerPosition = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        while((pointToSpawn-playerPosition).sqrMagnitude < safeDistance*safeDistance)
        {
            pointToSpawn = spawnList.transform.GetChild(Random.Range(0,spawnList.transform.childCount)).transform.position;
        }
        localAgent.Warp(pointToSpawn);
        localAgent.Warp(RandomNavmeshLocation(spawnRadius));
    }
}
