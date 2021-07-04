using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 2f;
    public Text soal;
    public Vector3 tr;
    public bool move, hide;
    public GameObject objek;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (hide == false)
        //    tr = gameObject.transform.position;
        var x = gameObject.transform.position.x;
        var y = gameObject.transform.position.y;

        //ClientSend.SendPos(x,y);

        if (move == true)
            Movement();
        if (hide == true)
            Henshin();
        
    }

    public void Henshin()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            move = false;
            //gameObject.transform.position = objek.transform.position;
            gameObject.SetActive(false);
            Debug.Log("");
        }
    }

    public void Movement()
    {
        float h = Input.GetAxis("Horizontal") * speed;
        float v = Input.GetAxis("Vertical") * speed;

        rb.velocity = new Vector2(h, v);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.CompareTag("Object"))
        //{
        //    gameObject.GetComponent<Image>().color = collision.GetComponent<Image>().color;
        //    hide = true;
        //    objek = collision.gameObject;
        //}
        //if (collision.tag == soal.text)
        //{
        //    Debug.Log("Bennr");
        //}

        //if (collision.CompareTag("Answer"))
        //{
        //    var score = GameObject.Find("Score").GetComponent<ScoreManager>();

        //    if (collision.gameObject.name == "1")
        //    {
        //        Debug.Log("Jawaban Benar");
        //        score.scorePlayer++;
        //    }
        //    else
        //    {
        //        Debug.Log("Jawaban Salah");
        //        score.scorePlayer--;
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Answer"))
        {
            var score = GameObject.Find("Score").GetComponent<ScoreManager>();

            if (collision.gameObject.name == "1")
            {
                Debug.Log("Jawaban Benar");
                score.scorePlayer++;
            }
            else
            {
                Debug.Log("Jawaban Salah");
                score.scorePlayer--;
            }
        }
    }
}
