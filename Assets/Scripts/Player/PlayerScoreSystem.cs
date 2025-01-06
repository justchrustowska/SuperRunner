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
   public DistanceRecordConfig _newRecordConfig;

   public static event Action<double> OnPlayerDeathWithDistance;
   public static event Action OnNewRecordDistance;

    private void Awake()
    {
        _startPos = transform.position;
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
            _newRecordConfig.newRecord = currentDistance;
            NewRecordDistance?.Invoke();
        }
        Debug.LogError(currentDistance); //dodac warunek gdy zostanie ustanowiony nowy rekord 
    }
}
