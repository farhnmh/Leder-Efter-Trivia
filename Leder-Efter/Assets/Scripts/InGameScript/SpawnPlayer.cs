using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public static SpawnPlayer sp;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject playerOri, local;

    private void Start()
    {
        ClientSend.Mintak();
        Debug.Log("KElUAR");
    }

    private void Awake()
    {
        if (sp == null)
            sp = this;
        else if (sp != this)
        {
            Debug.Log("Instance already exists, destroying object");
            Destroy(this);
        }
    }

    public void Spawn(int _id, string uname, Vector3 pos, Quaternion rotation)
    {
        GameObject _player;

        if (_id == Client.instance.myId)
        {
            _player = Instantiate(playerOri, pos, rotation);
            Debug.Log("TEST");
        }
        else
        {
            _player = Instantiate(local, pos, rotation);
            Debug.Log("TESTT2");
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = uname;
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }
}
