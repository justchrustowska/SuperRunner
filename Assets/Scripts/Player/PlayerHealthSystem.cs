using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    private Collider _pCollider; 
    private Renderer _renderer;
    private bool _isBlinking = false;

    [SerializeField]
    private float _blinkDuration = 1f;
    [SerializeField]
    private float _blinkInterval = 0.1f;
    [SerializeField]
    private int _currentHealth = 3;

    void Start()
    {
         _pCollider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
    }


    void Update()
    {
        PlayerDied();
    }

    private void PlayerDied()
    {
        if (_currentHealth == 0)
        {

            Debug.LogError("GAME OVER");

            EventManager.PlayerDied();
        }
    }

    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.CompareTag("Obstacle") && !_isBlinking)
        {
            StartCoroutine(IgnoreCollisionForSeconds(collision.collider, 0.5f));
            StartCoroutine(Blink());
            _currentHealth -= 1;
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
            _renderer.enabled = !_renderer.enabled;
            yield return new WaitForSeconds(_blinkInterval);
            elapsedTime += _blinkInterval;
        }

        _renderer.enabled = true;

        _isBlinking = false;
    }
}
