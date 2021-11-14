using System;
using Player;
using UnityEngine;

namespace Core
{
    public class CameraFollow : MonoBehaviour
    {
        private GameManager _gameManager;
        private PlayerCollisions _player;
        private float _movementSpeed;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _player = FindObjectOfType<PlayerCollisions>();
            _movementSpeed = 1.5f;
        }

        void Update()
        {
            if (_gameManager.isGameStarted && _player.stairCount >= 5 && _player.stairCount != 20)
            {
                Vector3 position = transform.position;
                position = Vector3.MoveTowards(position,
                    new Vector3(position.x, position.y + _movementSpeed * Time.deltaTime, position.z),
                    1f);
                transform.position = position;
            }
        
        }
    }
}
