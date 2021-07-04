using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriviaDatabase : MonoBehaviour
{
    public static TriviaDatabase instance;
    public int categoryTotal;

    public List<TriviaQuestion> triviaHewan;
    public List<TriviaQuestion> triviaTumbuhan;
    public List<TriviaQuestion> triviaNegara;
    public List<TriviaQuestion> triviaDunia;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
    }
}
