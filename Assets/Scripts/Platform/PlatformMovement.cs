using System.Collections;
using Core;
using UnityEngine;

namespace Platform
{
    public class PlatformMovement : MonoBehaviour
    {
        private GameManager _gameManager;
        public GameObject platformContainer;

        private PlatformInstantiate _platformInstantiate;
        private Vector3 _destination;
        private float _movementSpeed;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _platformInstantiate = FindObjectOfType<PlatformInstantiate>();
            _movementSpeed = 10f;
        }

        private void Start()
        {
            platformContainer.transform.SetParent(transform);
            _destination = _platformInstantiate.finishLineTransform.position * -5;
        }

        private void Update()
        {
            if (_gameManager.isGameStarted)
            {
                transform.position = Vector3.MoveTowards(transform.position,_destination, _movementSpeed * Time.deltaTime);
            }
        }
    }
}
