using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Message
{
    public string text;
    public TextMeshProUGUI textObject;
}

public class ChatBoxSystem : MonoBehaviour
{
    public int maxMessage = 25;
    public GameObject chatPanel, textObject;
    public List<Message> messageList = new List<Message>();

    public void PrintMessageOnChatBox(string msg)
    {
        if (messageList.Count >= maxMessage)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();
        newMessage.text = msg;

        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<TextMeshProUGUI>();
        newMessage.textObject.text = newMessage.text;

        messageList.Add(newMessage);
    }
}
