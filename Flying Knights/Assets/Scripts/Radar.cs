using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private GameObject nearestColossus;
    private float distToNearest;

    // Start is called before the first frame update
    void Start()
    {
        FindNearestColossus();
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestColossus();
    }

    private void FindNearestColossus()
    {
        GameObject[] colossusList = GameObject.FindGameObjectsWithTag("Colossus");
        GameObject curNearest = null;
        float curDist = Mathf.Infinity;
        foreach (GameObject colossus in colossusList)
        {
            if (colossus.GetComponent<Colossus>().IsDead())
                continue;
            float dist = Vector3.Distance(colossus.transform.position, player.transform.position);
            if (dist < curDist)
            {
                curDist = dist;
                curNearest = colossus;
            }
        }
        nearestColossus = curNearest;
        distToNearest = curDist;
    }

    public Vector3 GetDirVector()
    {
        Vector3 colossusPos = nearestColossus.transform.position;
        Vector3 playerPos = player.transform.position;
        colossusPos.y = 0;
        playerPos.y = 0;
        return (colossusPos - playerPos).normalized;
    }

    public Vector3 GetColossusPos()
    {
        return nearestColossus.transform.position + new Vector3(0, nearestColossus.transform.localScale.y * 2, 0);
    }

    public float GetDist()
    {
        return distToNearest;
    }

    public bool IsColossus()
    {
        return nearestColossus != null;
    }
}
