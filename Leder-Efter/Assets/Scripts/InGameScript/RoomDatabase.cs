using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDatabase
{
    public int id;
    public string username;
    public int typeChar;
    public PlayerCharManager character;

    public PlayerDatabase(int _id, string _uname, int _type)
    {
        id = _id;
        username = _uname;
        typeChar = _type;
    }
}

public class RoomDatabase : MonoBehaviour
{
    public static RoomDatabase instance;

    public string roomCode;
    public bool isPlay;
    public List<PlayerDatabase> playerDatabase = new List<PlayerDatabase>();

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

    public void AddPlayerToDatabase(int id, string uname)
    {
        bool found = false;
        for (int i = 0; i < playerDatabase.Count; i++)
        {
            if (id == playerDatabase[i].id)
                found = true;
        }

        if (!found)
            playerDatabase.Add(new PlayerDatabase(id, uname, 0));
    }

    public void RemovePlayerFromDatabase(string uname)
    {
        for (int i = 0; i < playerDatabase.Count; i++)
        {
            if (uname == playerDatabase[i].username)
            {
                playerDatabase.RemoveAt(i);
            }
        }
    }

    public void RemoveDatabase()
    {
        roomCode = "";
        playerDatabase.RemoveRange(0, playerDatabase.Count);
        Client.instance.isPlay = false;
        Client.instance.isHost = false;
        UILobbyManager.instance.GoToScene("MainMenu");
    }
}
