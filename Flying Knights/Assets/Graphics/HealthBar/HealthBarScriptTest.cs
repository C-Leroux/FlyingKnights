using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScriptTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HealthBarScript.SetHealthBarValue(1);
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarScript.SetHealthBarValue(HealthBarScript.GetHealthBarValue() - 0.0001f);
    }
}


