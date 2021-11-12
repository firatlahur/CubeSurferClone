using System;
using Core;
using UnityEngine;

namespace Player
{
    public class TouchControl : MonoBehaviour
    {
        private GameManager _gameManager;

        private Vector3 _movementDirection;

        private float _movementSpeed, _platformOffset;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _movementSpeed = 2f;
            _movementDirection = new Vector3();
            _platformOffset = 2.25f;
        }

        private void Update()
        {
            if (Input.touchCount > 0 && _gameManager.isGameStarted)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
                {
                    _movementDirection = new Vector3(Input.GetTouch(0).deltaPosition.x, 0f, 0f);
                    transform.Translate(_movementDirection * _movementSpeed * Time.deltaTime);

                    float xOffset = Mathf.Clamp(transform.position.x, -_platformOffset, _platformOffset);
                    transform.position = new Vector3(xOffset, transform.position.y, 0f);
                }
            }
        }
    }
}
