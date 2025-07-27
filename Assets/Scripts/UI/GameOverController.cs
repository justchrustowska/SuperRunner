using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject successScreen;
    [SerializeField] private GameObject unluckyScreen;

    private void Start()
    {
        playerHealth.OnPlayerDied += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        float currentDistance = PlayerScoreSystem.Instance.CurrentDistance;
        float bestDistance = PlayerScoreSystem.Instance.BestDistance;

        if (currentDistance > bestDistance)
        {
            PlayerScoreSystem.Instance.SaveBestDistance(currentDistance);
            successScreen.SetActive(true);
        }
        else
        {
            unluckyScreen.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}