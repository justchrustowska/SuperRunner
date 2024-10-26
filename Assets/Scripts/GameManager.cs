using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject failWindow;
    public TextMeshProUGUI _distanceTxt;
    public TextMeshProUGUI _collectedCoinsTxt;
    public Image dashCooldownIcon;

    private float _cooldownDuration = 3f;
    private float _cooldownTimer;

    public bool isCooldownActive = false;


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
                dashCooldownIcon.fillAmount = 0f;
                isCooldownActive = false;
            }
        }
    }

    void OnEnable()
    {
        EventManager.OnPlayerDeath += ShowFailWindow;
        EventManager.OnPlayerDash += DashCooldown;
        PlayerScoreSystem.OnPlayerDeathWithDistance += SaveFinalDistance;
    }

    void OnDisable()
    {
        EventManager.OnPlayerDeath -= ShowFailWindow;
        EventManager.OnPlayerDash -= DashCooldown;
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

    public void DashCooldown()
    {
        _cooldownTimer = _cooldownDuration;
        isCooldownActive = true;
        dashCooldownIcon.fillAmount = 1.0f;
    }

   

    /* IEnumerator ColldownAnimation()
     {
         float cooldownTime = _cooldownDuration;

         while (cooldownTime > 0)
         {
             cooldownTime -= Time.deltaTime;
             dashCooldownIcon.fillAmount = cooldownTime / _cooldownDuration;
             yield return null;
         }

         dashCooldownIcon.fillAmount = 0f;
     }*/
}
