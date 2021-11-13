using System.Collections.Generic;
using Core;
using ScriptableObjects.Obstacle;
using UnityEngine;

namespace Obstacle
{
    public class ObstacleInstantiate : MonoBehaviour
    {
        public GameObject platformContainer;
        
        private GameManager _gameManager;
        private ObstacleSkin _obstacle;
        
        private Vector3 _startPos;
        public List<Vector3> obstacleList;

        private int _obstacleSpawnAmount;
        
        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();

            _startPos = new Vector3(-2.25f, .25f, 20f);
            obstacleList = new List<Vector3>();

            _obstacleSpawnAmount = _gameManager.currentLevel * 2 * 10;
        }

        private void Start()
        {
            _obstacle = _gameManager.obstacleSkin;
            InstantiateObstacles();
        }

        private void InstantiateObstacles()
        {
            for (int i = 0; i < _obstacleSpawnAmount; i++)
            {
                GameObject obstacle = Instantiate(_obstacle.obstacleSkinList[Random.Range(0, _obstacle.obstacleSkinList.Count)],
                    _startPos, Quaternion.identity);
                
                obstacleList.Add(obstacle.transform.position);
                obstacle.name = "i: " + i;

                if (i > 0)
                {
                    obstacle.transform.position = CalculateObstaclePosition(i);
                }

                obstacle.transform.SetParent(platformContainer.transform);
            }
        }

        private Vector3 CalculateObstaclePosition(int i)
        {
            const float xOffset = .5f;
            const float zOffset = 20f;
            
            _startPos.x += xOffset;

            switch (i % 10)
            {
                case 0:
                    _startPos.z += zOffset;
                    _startPos.x = -2.25f;
                    break;
            }

            return _startPos;
        }
    }
}
