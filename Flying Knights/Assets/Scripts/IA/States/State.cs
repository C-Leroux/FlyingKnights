using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State<Agent_Type>
{
    // Called when the state is entered
    void Enter(Agent_Type agent);

    // Called each frame when the state is active
    void Execute(Agent_Type agent);

    // Called when the state is exited
    void Exit(Agent_Type agent);

    // Called upon receiving a message while the state is active
    // public bool OnMessage(Agent_Type agent, const Telegram&);
}
