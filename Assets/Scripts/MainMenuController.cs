using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject UIHolder;

    public void StartGame()
    {
        SceneManager.LoadScene("RooksMoveCueMath");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
