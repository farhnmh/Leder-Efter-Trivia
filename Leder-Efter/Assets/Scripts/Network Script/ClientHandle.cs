using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeRequest();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SignInValidation(Packet _packet)
    {
        string _msg = _packet.ReadString();
        UILoginManager.instance.notif.text = _msg;

        if (_msg == "login was successful")
            Client.instance.isLogin = true;
    }

    public static void SignUpValidation(Packet _packet)
    {
        string _msg = _packet.ReadString();
        UILoginManager.instance.notif.text = _msg;
    }

    public static void HostRoomValidation(Packet _packet)
    {
        string _msg = _packet.ReadString();
        UIMenuManager.instance.notifText.text = _msg;

        if (_msg == "room created succesfully")
        {
            Client.instance.isHost = true;
        }
    }

    public static void JoinRoomValidation(Packet _packet)
    {
        string _msg = _packet.ReadString();
        UIMenuManager.instance.notifText.text = _msg;

        if (_msg == "joined succesfully")
        {
            Client.instance.isPlay = true;
        }
    }

    public static void LeaveRoomValidation(Packet _packet)
    {
        string _codeRoom = _packet.ReadString();
        string _uname = _packet.ReadString();

        if (_codeRoom == RoomDatabase.instance.roomCode)
        {
            RoomDatabase.instance.RemovePlayerFromDatabase(_uname);
            UILobbyManager.instance.playerLeft = true;
        }
    }

    public static void DestroyRoomValidation(Packet _packet)
    {
        string _codeRoom = _packet.ReadString();

        if (_codeRoom == RoomDatabase.instance.roomCode)
        {
            RoomDatabase.instance.RemoveDatabase();
        }
    }

    public static void AddPlayerToDatabase(Packet _packet)
    {
        string _codeRoom = _packet.ReadString();
        int _id = _packet.ReadInt();
        string _uname = _packet.ReadString();

        if (_codeRoom == RoomDatabase.instance.roomCode)
        {
            RoomDatabase.instance.AddPlayerToDatabase(_id, _uname);
        }
    }

    public static void StartMatchValidation(Packet _packet)
    {
        string _codeRoom = _packet.ReadString();
        string _gameType = _packet.ReadString();

        if (_codeRoom == RoomDatabase.instance.roomCode)
        {
            UILobbyManager.instance.gameType = _gameType;
            RoomDatabase.instance.isPlay = true;
        }
    }

    public static void PlayerPositionValidation(Packet _packet)
    {
        string _codeRoom = _packet.ReadString();
        int _id = _packet.ReadInt();
        int _controlHorizontal = _packet.ReadInt();
        int _controlVertical = _packet.ReadInt();

        if (_codeRoom == RoomDatabase.instance.roomCode)
        {
            GameManager.instance.UpdatePlayerPosition(_id, _controlHorizontal, _controlVertical);
        }
    }

    public static void ChatboxValidation(Packet _packet)
    {
        ClientData.Chatbox _chatbox = _packet.ReadObject<ClientData.Chatbox>();
        UIChatboxManager.instance.chatbox.PrintMessageOnChatBox($"{_chatbox.username}: {_chatbox.message}");
    }

    public static void RandomizeValidation(Packet _packet)
    {
        string stuff = "", color = "";
        int totalReady = _packet.ReadInt();
        int totalPlayer = _packet.ReadInt();

        if (totalReady == totalPlayer)
        {
            stuff = _packet.ReadString();
            color = _packet.ReadString();
        }

        UIRandomizeManager.instance.totalText.text = $"Total player're ready: {totalReady}/{totalPlayer}";
        UIRandomizeManager.instance.stuffText.text = $"You have to find some: {stuff}";
        UIRandomizeManager.instance.colorText.text = $"Your team's color: {color}";
    }

    public static void ReadyDong(Packet _packet)
    {
        string kondisi = "";
        int totalReady = _packet.ReadInt();
        int totalPlayer = _packet.ReadInt();

        if (totalReady == totalPlayer)
        {
            kondisi = _packet.ReadString();
        }

        UIRandomizeManager.instance.totalText.text = $"Total player're ready: {totalReady}/{totalPlayer}";
        UIRandomizeManager.instance.statusReady = kondisi;
    }

    public static void ColorHandler(Packet _packet)
    {
        var a = GameObject.Find("Question");
        var b = a.GetComponent<ColorManager>();
        a.GetComponent<Text>().text = _packet.ReadString();
        var soal = _packet.ReadString();
        b.questionCounter++;

        for (int i = 0; i < b.warnaSoal.Count; i++)
        {
            if (soal == b.warnaSoal[i].Word)
            {
                a.GetComponent<Text>().color = b.warnaSoal[i].Value;
                b.checkWarna = true;
            }
        }

    }

    public static void PlayerPos(Packet _packet)
    {
        int id = _packet.ReadInt();
        string uname = _packet.ReadString();
        Vector3 pos = _packet.ReadVector3();
        Quaternion rot = _packet.ReadQuaternion();

        SpawnPlayer.sp.Spawn(id, uname, pos, rot);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int id = _packet.ReadInt();
        Vector3 pos = _packet.ReadVector3();
        Debug.Log("UDP TEST");
        SpawnPlayer.players[id].transform.position = pos;
    }

    public static void PlayerRotation(Packet _packet)
    {
        int id = _packet.ReadInt();
        Quaternion rot = _packet.ReadQuaternion();

        SpawnPlayer.players[id].transform.rotation = rot;
    }

    public static void TriviaQuestionValidation(Packet _packet)
    {
        string codeRoom = _packet.ReadString();
        int questionResult = _packet.ReadInt();
        UITriviaManager.instance.SetQuestion(codeRoom, questionResult);
    }

    public static void TriviaDatabaseValidation(Packet _packet)
    {
        string codeRoom = _packet.ReadString();
        int categoryResult = _packet.ReadInt();
        int questionResult = _packet.ReadInt();
        UITriviaManager.instance.SetDatabase(codeRoom, categoryResult, questionResult);
    }

    public static void ScorePlayReceiver(Packet _packet)
    {
        int score = _packet.ReadInt();
        int play = _packet.ReadInt();
        Client.instance.myScore = score;
        Client.instance.myPlay = play;
    }
}