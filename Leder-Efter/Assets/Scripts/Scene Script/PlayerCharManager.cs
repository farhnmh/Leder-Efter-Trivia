using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharManager : MonoBehaviour
{
	public int horizontal = 0;
	public int vertical = 0;
	public float maxSpeed = 5f;

	public Animator anim;
	public Rigidbody2D rb;

	public bool faceRight = true;
	public bool isDead = false;
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
		anim.SetBool("walk", false);
		anim.SetBool("dead", false);
		anim.SetBool("jump", false);
	}

    void FixedUpdate()
    {
		Move(horizontal, vertical);
	}

	void Move(int horizontal, int vertical)
    {
		if (horizontal > 0)
		{
			anim.SetBool("walk", true);
			if (faceRight == false)
			{
				Flip();
			}
		}

		if ((horizontal < 0))
		{
			anim.SetBool("walk", true);
			if (faceRight == true)
			{
				Flip();
			}
		}

		if (vertical > 0)
			anim.SetBool("walk", true);

		if (vertical < 0)
			anim.SetBool("walk", true);

		if (horizontal == 0 && vertical == 0)
			anim.SetBool("walk", false);

		rb.velocity = new Vector3(horizontal * maxSpeed, vertical * maxSpeed, 0);

		//Vector3 tempVect = new Vector3(horizontal, vertical, 0);
		//tempVect = tempVect.normalized * maxSpeed * Time.deltaTime;
		//rb.MovePosition(rb.transform.position + tempVect);
	}

	void Flip()
	{
		faceRight = !faceRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Answer"))
    //    {
    //        var score = GameObject.Find("Score").GetComponent<ScoreManager>();

    //        if (collision.gameObject.name == "1")
    //        {
    //            Debug.Log("Jawaban Benar");
    //            score.scorePlayer++;
    //        }
    //        else
    //        {
    //            Debug.Log("Jawaban Salah");
    //            score.scorePlayer--;
    //        }
    //    }
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Answer"))
    //    {
    //        var score = GameObject.Find("Score").GetComponent<ScoreManager>();
    //        var kondisi = GameObject.Find("Spawn").GetComponent<SpawnScript>();

    //        if (collision.gameObject.name == "1" && kondisi.playerAnswer == true)
    //        {
    //            Debug.Log("Jawaban Benar");
    //            score.scorePlayer++;
    //        }
    //        else if (collision.gameObject.name == "2" && kondisi.playerAnswer == true)
    //        {
    //            Debug.Log("Jawaban Salah");
    //            score.scorePlayer--;
    //        }
    //    }
    //}
}
