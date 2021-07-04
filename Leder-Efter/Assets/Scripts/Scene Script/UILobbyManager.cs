using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PlayerPrint 
{
    public GameObject background;
    public Text playerName;

    public PlayerPrint(GameObject gameobject, Text text)
    {
        background = gameobject;
        playerName = text;
    }
}

public class UILobbyManager : MonoBehaviour
{
    public static UILobbyManager instance;

    [Header("Lobby Attribute")]
    public TextMeshProUGUI helloText;
    public GameObject colorButton;
    public GameObject triviaButton;
    public Button color;
    public Button trivia;
    public string gameType;

    [Header("Player Data Attribute")]
    public bool playerLeft = false;
    public List<PlayerPrint> playerPrint;

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

    void Start()
    {
        helloText.text = $"you're in room #{RoomDatabase.instance.roomCode}";
        color = colorButton.GetComponent<Button>();
        trivia = triviaButton.GetComponent<Button>();

        if (Client.instance.isHost)
        {
            //colorButton.SetActive(true);
            triviaButton.SetActive(true);
        }
    }

    private void Update()
    {
        if (playerLeft)
        {
            for (int i = 0; i < playerPrint.Count; i++)
            {
                playerPrint[i].background.SetActive(false);
            }

            playerLeft = false;
        }
        else if (!playerLeft)
        {
            for (int i = 0; i < RoomDatabase.instance.playerDatabase.Count; i++)
            {
                playerPrint[i].background.SetActive(true);
                
                if (RoomDatabase.instance.playerDatabase[i].username == Client.instance.myUname)
                    playerPrint[i].playerName.text = $"(you) {RoomDatabase.instance.playerDatabase[i].username}";
                else
                    playerPrint[i].playerName.text = RoomDatabase.instance.playerDatabase[i].username;
            }
        }

        if (RoomDatabase.instance.isPlay)
            GoToScene(gameType + "Scene");
    }

    public void LeaveDestroyRoom()
    {
        if (!Client.instance.isHost)
        {
            ClientSend.LeaveRoomRequest(RoomDatabase.instance.roomCode, Client.instance.myUname);
            RoomDatabase.instance.RemoveDatabase();
        }
        else
        {
            ClientSend.DestroyRoomRequest(RoomDatabase.instance.roomCode);
            GoToScene("MainMenu");
        }

        Client.instance.isPlay = false;
    }

    public void StartMatch(string gameType)
    {
        ClientSend.StartMatch(RoomDatabase.instance.roomCode, gameType);
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
