using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreSystem : MonoBehaviour
{
   public TextMeshProUGUI distanceTxt;

   private float _distanceTravelled;
   private Vector3 _startPos;
   private float _previousRecord;
   private float _currentRecord;
   public DistanceRecordConfig _newRecordConfig;

   public static event Action<double> OnPlayerDeathWithDistance;
   public static event Action OnNewRecordDistance;
   public static event Action OnFailNewRecord;

    private void Awake()
    {
        _startPos = transform.position;
        
        if (PlayerPrefs.HasKey("HighScore"))
        {
            _newRecordConfig.newRecord = PlayerPrefs.GetFloat("HighScore");
        }
        
        _previousRecord = _newRecordConfig.newRecord;
    }
    void Start()
    {
        EventManager.OnPlayerDeath += PlayerDeath;
    }

    void Update()
    {
        _distanceTravelled = Vector3.Distance(_startPos, transform.position);

        double roundedDistance = Mathf.Round(_distanceTravelled);

        distanceTxt.text = "Distance: " + roundedDistance;
        
    }

    private void PlayerDeath()
    {
        double roundedDistance = Mathf.Round(_distanceTravelled);
        OnPlayerDeathWithDistance?.Invoke(roundedDistance);
        CheckNewRecord();
    }

    private void CheckNewRecord()
    {
        var currentDistance = Mathf.Round(_distanceTravelled);
        if (currentDistance > _newRecordConfig.newRecord)
        {
            _previousRecord = _newRecordConfig.newRecord;
            _newRecordConfig.newRecord = currentDistance;
            PlayerPrefs.SetFloat("HighScore", currentDistance);
            _newRecordConfig.newRecord = currentDistance;
            OnNewRecordDistance?.Invoke();
        }
        else
        {
            OnFailNewRecord?.Invoke();
            Debug.LogError("fail record invoke event");
        }
    }
}
