using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class InstantiateManager : MonoBehaviour
    {
        private GameManager _gameManager;

        [Header("Platform Related")]
        public GameObject platformPrefab;
        public GameObject platformContainer;
        
        private const int InstantiateModifier = 10;
        private int _platformSpawnAmount;
        
        private Vector3 _platformSpawnOffset;
        private List<GameObject> _platformList;

        [Header("Obstacle Related")]
        public GameObject obstaclePrefab;

        private List<GameObject> _obstacleList;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            
            _platformList = new List<GameObject>();
            _obstacleList = new List<GameObject>();
            
            _platformSpawnOffset = new Vector3(0f, 0f, 12f);
            _platformSpawnAmount = _gameManager.currentLevel * InstantiateModifier;
            
            InstantiatePlatforms();
        }

        private void InstantiatePlatforms()
        {
            for (int i = 0; i < _platformSpawnAmount; i++)
            {
                GameObject platform = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);
                if (i > 0)
                {
                    platform.transform.position += _platformSpawnOffset;
                    _platformSpawnOffset.z += 12f;
                }
                platform.transform.SetParent(platformContainer.transform);
               _platformList.Add(platform);
            }
            InstantiateObstacles();
        }
        

        private void InstantiateObstacles()
        {
            const int width = 10;
            const float xOffset = .5f;
            const float yOffset = .5f;

            Vector3 startPos = new Vector3(.3f, .8f, 10f);
            
            for (int i = 0; i < width * 3; i++)
            {
                GameObject obstacle = Instantiate(obstaclePrefab, startPos, Quaternion.identity);

                if (i > 0)
                {
                    startPos.x += xOffset;
                    obstacle.transform.position = startPos;
                }

                if (i % width == 0)
                {
                    startPos.x = .3f;
                    startPos.y += yOffset;
                    obstacle.transform.position = startPos;
                }
                _obstacleList.Add(obstacle);
            }
        }
    }
}
