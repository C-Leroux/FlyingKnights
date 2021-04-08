using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [Tooltip("An object whose children will be the spawnPoints")]
    [SerializeField] private GameObject spawnList = null;

    [Tooltip("If set to true spawn will not be random and instead the player will spawn here it lies in the editor")]
    [SerializeField] private bool skipSpawn = false;

    void Awake()
    {
        if(!skipSpawn)
        {
            transform.position = spawnList.transform.GetChild(Random.Range(0,spawnList.transform.childCount)).transform.position;
        }
    }

}
