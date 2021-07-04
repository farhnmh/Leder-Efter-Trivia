using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UILoginManager : MonoBehaviour
{
    public static UILoginManager instance;

    public GameObject startMenu;
    public GameObject loginPage;
    public TMP_InputField ipField;
    public TextMeshProUGUI notif;
    public string toScene;
    public string ipAddressExternal;

    [Header("SignIn Attribute")]
    public TMP_InputField usernameSignIn;
    public TMP_InputField passwordSignIn;

    [Header("SignUp Attribute")]
    public TMP_InputField usernameSignUp;
    public TMP_InputField passwordSignUp;
    public TMP_InputField confirmSignUp;

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

    private void Start()
    {
        loginPage.SetActive(false);
        ConnectToServer();
    }

    private void Update()
    {
        if (Client.instance.isLogin)
        {
            SceneManager.LoadScene(toScene);
        }
    }

    public void ConnectToServer()
    {
        ipField.interactable = false;

        if (ipField.text == "")
            ipField.text = "127.0.0.1";

        Client.instance.ConnectToServer();

        startMenu.SetActive(false);
        loginPage.SetActive(true);
    } 

    public void SignIn()
    {
        if (usernameSignIn.text == "" || passwordSignIn.text == "")
            notif.text = "please fill the blank field!";
        else
        {
            Client.instance.myUname = usernameSignIn.text;
            Client.instance.myPass = passwordSignIn.text;
            ClientSend.SignInRequest(usernameSignIn.text, passwordSignIn.text);
            ClientSend.ScorePlayRequest(Client.instance.myUname);
            ValueReset();
        }
    }

    public void SignUp()
    {
        if (usernameSignUp.text == "" || passwordSignUp.text == "" || confirmSignUp.text == "")
            notif.text = "please fill the blank field!";
        else if (passwordSignUp.text != confirmSignUp.text)
            notif.text = "please re-check the confirm password field!";
        else
        {
            ClientSend.SignUpRequest(usernameSignUp.text, passwordSignUp.text);
            ValueReset();
        }
    }

    public void ValueReset()
    {
        usernameSignIn.text = "";
        passwordSignIn.text = "";
        usernameSignUp.text = "";
        passwordSignUp.text = "";
        confirmSignUp.text = "";
    }
}
