using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIChatboxManager : MonoBehaviour
{
    public static UIChatboxManager instance;
    public ChatBoxSystem chatbox;
    public TMP_InputField inputMessage;

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

    public void SendMessage()
    {
        ClientSend.ChatboxRequest(Client.instance.myUname, inputMessage.text);
        inputMessage.text = "";
    }
}
