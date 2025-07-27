using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distanceText;
    public static PlayerScoreSystem Instance { get; private set; }

    public float CurrentDistance { get; private set; }
    public float BestDistance { get; private set; }

    private Vector3 _startPosition;
    private bool _isTracking = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        BestDistance = PlayerPrefs.GetFloat("BestDistance", 0f);
    }

    public void StartTracking(Transform player)
    {
        _startPosition = player.position;
        CurrentDistance = 0f;
        _isTracking = true;
    }

    public void StopTracking()
    {
        _isTracking = false;
    }

    private void Update()
    {
        if (!_isTracking) return;

        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            CurrentDistance = Vector3.Distance(_startPosition, player.transform.position);
            
            if (CurrentDistance > BestDistance)
            {
                BestDistance = CurrentDistance;
            }
        }
        
        double roundedDistance = Mathf.Round(CurrentDistance);

        distanceText.text = "Distance: " + roundedDistance;
    }

    public void SaveBestDistance(float distance)
    {
        if (distance > BestDistance)
        {
            BestDistance = distance;
            PlayerPrefs.SetFloat("BestDistance", distance);
            PlayerPrefs.Save();
        }
    }
}
