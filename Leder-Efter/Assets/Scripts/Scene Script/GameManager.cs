using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool sendIdle;

    public List<GameObject> playerCharacterTemp;
    public List<GameObject> player;

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
        for (int i = 0; i < RoomDatabase.instance.playerDatabase.Count; i++)
        {
            var player = Instantiate(playerCharacterTemp[RoomDatabase.instance.playerDatabase[i].typeChar],
                                     new Vector3(0, 0, 0),
                                     Quaternion.identity);

            player.transform.parent = gameObject.transform;
            player.name = RoomDatabase.instance.playerDatabase[i].username;
            RoomDatabase.instance.playerDatabase[i].character = player.GetComponent<PlayerCharManager>();
        }
    }

    void Update()
    {
        if (UITriviaManager.instance.isPlay)
            MoveRequest();
    }

    public void MoveRequest()
    {
        int horizontal = Convert.ToInt32(Input.GetAxis("Horizontal"));
        int vertical = Convert.ToInt32(Input.GetAxis("Vertical"));

        if (horizontal == 0 && vertical == 0 && !sendIdle) {
            ClientSend.PlayerPosition(RoomDatabase.instance.roomCode,
                                      Client.instance.myId, horizontal, vertical);
            
            sendIdle = true;
        }
        
        if (horizontal != 0 || vertical != 0)
        {
            ClientSend.PlayerPosition(RoomDatabase.instance.roomCode,
                                      Client.instance.myId, horizontal, vertical);

            sendIdle = false;
        }
    }

    public void UpdatePlayerPosition(int id, int _controlHorizontal, int _controlVertical)
    {
        for (int i = 0; i < RoomDatabase.instance.playerDatabase.Count; i++)
        {
            if (RoomDatabase.instance.playerDatabase[i].id == id)
            {
                RoomDatabase.instance.playerDatabase[i].character.horizontal = _controlHorizontal;
                RoomDatabase.instance.playerDatabase[i].character.vertical = _controlVertical;
            }
        }
    }
}
