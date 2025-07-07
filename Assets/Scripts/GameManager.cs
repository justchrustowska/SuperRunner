using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverWindow;
    public GameObject newRecordWindow;
    public GameObject failWindow;
    public TextMeshProUGUI distanceTxt;
    public TextMeshProUGUI distanceScoreTxt;
    public TextMeshProUGUI _collectedCoinsTxt;
    public Image dashCooldownIcon;
    public bool isCooldownActive = false;

    private float _cooldownDuration = 3f;
    private float _cooldownTimer;
    private int _distance;


    private void Awake()
    {
        Time.timeScale = 1f;
        dashCooldownIcon.fillAmount = 0f;
    }

    void Update()
    {
        if (isCooldownActive)
        {
            if (_cooldownTimer > 0)
            {
                _cooldownTimer -= Time.deltaTime;
                dashCooldownIcon.fillAmount = _cooldownTimer / _cooldownDuration;
            }
            else
            {
                dashCooldownIcon.fillAmount = 1f;
                isCooldownActive = false;
            }
        }
    }
         
    void OnEnable()
    {
        //EventManager.OnPlayerDeath += ShowGameOverlWindow;
        EventManager.OnPlayerDash += DashCooldown;
        PlayerScoreSystem.OnPlayerDeathWithDistance += SaveFinalDistance;
        PlayerScoreSystem.OnNewRecordDistance += NewRecordWindow;
        PlayerScoreSystem.OnFailNewRecord += FailWindow;
    }

    void OnDisable()
    {
        //EventManager.OnPlayerDeath -= ShowGameOverlWindow;
        EventManager.OnPlayerDash -= DashCooldown;
        PlayerScoreSystem.OnPlayerDeathWithDistance -= SaveFinalDistance;
        PlayerScoreSystem.OnNewRecordDistance -= NewRecordWindow;
        PlayerScoreSystem.OnFailNewRecord -= FailWindow;
    }

    void FailWindow()
    {
        gameOverWindow.SetActive(true);
        failWindow.SetActive(true);
        newRecordWindow.SetActive(false);
        Time.timeScale = 0f;
        Debug.LogError("FAIL");
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    private void SaveFinalDistance(double roundedDistance)
    {
        distanceTxt.text = "Distance: " + roundedDistance.ToString();
        distanceScoreTxt.text = "Distance: " + roundedDistance.ToString();
    }

    private void NewRecordWindow()
    {
        gameOverWindow.SetActive(true);
        failWindow.SetActive(false);
        newRecordWindow.SetActive(true);
        Time.timeScale = 0f;
        Debug.LogError("NEW RECORD DISTANCE");
    }

    public void DashCooldown()
    {
        if (!isCooldownActive)
        {
            _cooldownTimer = _cooldownDuration;
            isCooldownActive = true;
            dashCooldownIcon.fillAmount = 1.0f;
        }
    }
}
