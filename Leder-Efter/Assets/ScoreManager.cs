using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    Text score;
    public int scorePlayer;

    // Start is called before the first frame update
    void Start()
    {
        score = gameObject.GetComponent<Text>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(scorePlayer < 0)
        {
            scorePlayer = 0;
        }

        score.text = "Score : " + scorePlayer;
    }
}
