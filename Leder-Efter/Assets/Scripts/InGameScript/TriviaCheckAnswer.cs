using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriviaCheckAnswer : MonoBehaviour
{
    public string thisAnswer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == Client.instance.myUname)
        {
            if (thisAnswer == UITriviaManager.instance.answerFix)
            {
                UITriviaManager.instance.answer = true;
                Debug.Log("Jawabanmu Benar");
            }
            else
            {
                UITriviaManager.instance.answer = false;
                Debug.Log("Jawabanmu Salah");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == Client.instance.myUname)
        {
            UITriviaManager.instance.answer = false;
            Debug.Log("Kembali Ke Jawabanmu!");
        }
    }
}
