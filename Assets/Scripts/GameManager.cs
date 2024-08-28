using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject failWindow;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    void OnEnable()
    {
       
        EventManager.OnPlayerDeath += ShowFailWindow;
    }

    void OnDisable()
    {
        EventManager.OnPlayerDeath -= ShowFailWindow;
    }

    void ShowFailWindow()
    {
        failWindow.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
