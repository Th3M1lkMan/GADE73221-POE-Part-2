using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public void Scene1()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void UnPause()
    {
        Time.timeScale = 1f;
    }
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
    
    public void LeaderBoard()
    {
        SceneManager.LoadScene(2);
    }

    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
