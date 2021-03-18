using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public int scoreValue;
    public Text TextBox;

    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 0;
        TextBox.text = scoreValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        TextBox.text = scoreValue.ToString();
    }

    public void addScore(int adding)
    {
        scoreValue += adding;
    }

    public void subScore(int adding)
    {
        scoreValue -= adding;
    }
}
