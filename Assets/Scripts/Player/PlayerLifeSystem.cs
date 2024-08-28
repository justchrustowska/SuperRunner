using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerLifeSystem : MonoBehaviour
{
    private Collider _pCollider;
    private Renderer _renderer;
    private bool _isBlinking = false;

    [SerializeField]
    private float _blinkDuration = 1f;
    [SerializeField]
    private float _blinkInterval = 0.1f;


    void Start()
    {
         _pCollider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
    }


    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.CompareTag("Obstacle") && !_isBlinking)
        {
            StartCoroutine(IgnoreCollisionForSeconds(collision.collider, 0.5f));
            StartCoroutine(Blink());
            Debug.LogError("zderzenie");
        }
    }

    private IEnumerator IgnoreCollisionForSeconds(Collider otherCollider, float seconds)
    {
        Physics.IgnoreCollision(_pCollider, otherCollider, true);
        yield return new WaitForSeconds(seconds);
        Physics.IgnoreCollision(_pCollider, otherCollider, false);
    }
    private IEnumerator Blink()
    {
        _isBlinking = true;
        float elapsedTime = 0f;

        while (elapsedTime < _blinkDuration)
        {
            // Zmiana stanu widocznoœci
            _renderer.enabled = !_renderer.enabled;

            // Czekaj przez interwa³ migotania
            yield return new WaitForSeconds(_blinkInterval);

            // Aktualizacja czasu
            elapsedTime += _blinkInterval;
        }

        // Upewnij siê, ¿e gracz jest widoczny po zakoñczeniu migotania
        _renderer.enabled = true;

        _isBlinking = false;
    }
}
