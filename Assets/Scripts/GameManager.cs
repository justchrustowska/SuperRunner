using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject failWindow;
    public TextMeshProUGUI _distanceTxt;
    public TextMeshProUGUI _collectedCoinsTxt;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    void OnEnable()
    {
        EventManager.OnPlayerDeath += ShowFailWindow;
        PlayerScoreSystem.OnPlayerDeathWithDistance += SaveFinalDistance;
    }

    void OnDisable()
    {
        EventManager.OnPlayerDeath -= ShowFailWindow;
        PlayerScoreSystem.OnPlayerDeathWithDistance -= SaveFinalDistance;
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

    private void SaveFinalDistance(double roundedDistance)
    {
        _distanceTxt.text = "Distance: " + roundedDistance.ToString();
    }
}
