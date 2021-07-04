using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIRandomizeManager : MonoBehaviour
{
    public static UIRandomizeManager instance;
    public TextMeshProUGUI stuffText;
    public TextMeshProUGUI colorText;
    public TextMeshProUGUI totalText;
    public string statusReady;
    public string timer;

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

    private void Update()
    {
        if (statusReady == "ready")
            SceneManager.LoadScene("PropHunt");
    }

    public void GetReady()
    {
        ClientSend.RandomizeRequest();
    }

    public void Gas()
    {
        ClientSend.Gaskeun();
    }
}