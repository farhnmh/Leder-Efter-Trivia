using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignCharacterPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find(Client.instance.myUname);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.SetParent(player.transform);
        transform.position = new Vector3(player.transform.position.x,
                                         player.transform.position.y + offset.y,
                                         0);
    }
}
