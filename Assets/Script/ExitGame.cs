using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public void ExitGameEntair()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void HomeGame()
    {
        SceneManager.LoadScene(0);
    }
}
