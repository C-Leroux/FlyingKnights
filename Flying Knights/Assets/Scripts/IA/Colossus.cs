using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colossus : MonoBehaviour
{
    [SerializeField]
    public StateMachine<Colossus> StateMachine
    {
        get; set;
    }

    // Return true if the player is within the sphere detection of the colossus
    public bool DetectPlayer()
    {
        return false;
    }
}
