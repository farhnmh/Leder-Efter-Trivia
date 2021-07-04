using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    Client cl;
    public Text playerName;

    // Start is called before the first frame update
    void Start()
    {
        cl = GameObject.Find("ClientManager").GetComponent<Client>();
        playerName = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        playerName.text = cl.myUname;
    }
}
