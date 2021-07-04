using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public Text GoScore;
    public int scoreEnd;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GoScore.text = "Your Score : " + scoreEnd;
    }

    public void BacktoMainMenu()
    {
        RoomDatabase.instance.isPlay = false;
        Client.instance.isPlay = false;
        SceneManager.LoadScene("MainMenu");
    }
}
