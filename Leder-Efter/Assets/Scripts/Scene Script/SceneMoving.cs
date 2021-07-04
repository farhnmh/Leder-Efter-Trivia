using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneMoving : MonoBehaviour
{
    public void GoToScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }
}
