using UnityEngine;

namespace Core
{
    public class TouchControl : MonoBehaviour
    {
        private GameManager _gameManager;

        private Vector3 _movementDirection;

        private float _movementSpeed;

        private const float PlatformOffset = 2.5f;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _movementSpeed = 2f;
            _movementDirection = new Vector3();
        }

        private void Update()
        {
            if (Input.touchCount > 0 && _gameManager.isGameStarted)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
                {
                    _movementDirection = new Vector3(Input.GetTouch(0).deltaPosition.x, 0f, 0f);
                    transform.Translate(_movementDirection * _movementSpeed * Time.deltaTime);

                    Vector3 player = transform.position;
                    
                    float xOffset = Mathf.Clamp(player.x, -PlatformOffset, PlatformOffset);
                    player = new Vector3(xOffset, player.y, 0f);
                    transform.position = player;
                }
            }
        }
    }
}
