using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerScript : MonoBehaviour
{
    Text time;
    float waktu = 10;
    bool cl;
    SpawnScript ss;

    // Start is called before the first frame update
    void Start()
    {
        ss = GameObject.Find("Spawn").GetComponent<SpawnScript>();
        time = gameObject.GetComponent<Text>();
        cl = GameObject.Find("Question").GetComponent<ColorManager>().checkWarna;
        ClientSend.ColorRequest();
    }

    // Update is called once per frame
    void Update()
    {
        if (waktu >= 0)
        {
            Timer();
        }
    }

    void Timer()
    {
        float a = waktu -= Time.deltaTime;
        int b = (int)a;
        //int minutes = Mathf.FloorToInt(waktu / 60);
        //int seconds = Mathf.FloorToInt(waktu - minutes * 60);
        //string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        //time.text = niceTime;
        time.text = "TIME : " + b;

        if(waktu <= 0)
        {
            time.text = "TIME : 0";
            ss.playerAnswer = true;
            StartCoroutine(AnswerDelay());
            //ClientSend.ColorRequest();
            //waktu = 10;
            //Debug.Log("Second");
        }
    }

    IEnumerator AnswerDelay()
    {
        yield return new WaitForSeconds(3);
        ClientSend.ColorRequest();
        waktu = 10;
        //ss.playerAnswer = false;
        Debug.Log("Second");
    }
}
