using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var score = GameObject.Find("Score").GetComponent<ScoreManager>();
            var kondisi = GameObject.Find("Spawn").GetComponent<SpawnScript>();

            if (gameObject.name == "1" && kondisi.playerAnswer == true)
            {
                Debug.Log("Jawaban Benar");
                kondisi.playerAnswer = false;
                score.scorePlayer++;
            }
            else if (gameObject.name == "2" && kondisi.playerAnswer == true)
            {
                Debug.Log("Jawaban Salah");
                score.scorePlayer--;
            }
        }
    }
}
