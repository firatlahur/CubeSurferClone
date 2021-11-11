using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class InstantiateManager : MonoBehaviour
    {
        private GameManager _gameManager;

        [Header("Platform")]
        public GameObject platformPrefab;
        public GameObject finishPlatformPrefab;

        private float _platformScaleModifier;
        private float _platformPositionModifier;


        [Header("Obstacle")]
        public GameObject obstaclePrefab;

        [Header("Collectable")]
        public GameObject collectableCubePrefab;

        private const int ObstacleHeight = 3;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            
            _platformScaleModifier = _gameManager.currentLevel * 5f;
            _platformPositionModifier = _platformScaleModifier * 5f;
        }

        private void Start()
        {
            InstantiatePlatform();
        }

        private void InstantiatePlatform()
        {
            GameObject platform = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);

            platform.transform.localScale = new Vector3(.5f, 1f, _platformScaleModifier);
            platform.transform.position = new Vector3(0f, 0f, _platformPositionModifier);
            platform.name = "Platform";

            InstantiateObstacles(platform);
            InstantiateFinishPlatform(platform);
        }

        private void InstantiateObstacles(GameObject platform)
        {
            const int width = 10;
            const int eachWallCount = width * 3;
            const float xOffset = .5f;
            const float yOffset = .5f;

            int obstacleAmount = (_gameManager.currentLevel * eachWallCount * 2 + _gameManager.currentLevel * eachWallCount) - eachWallCount;
            
            Vector3 startPos = new Vector3(-2.25f, 0f, 15f);

            for (int i = 0; i < obstacleAmount; i++)
            {
                GameObject obstacle = Instantiate(obstaclePrefab, startPos, Quaternion.identity);
                Debug.Log("a");

                if (i > 0)
                {
                  startPos.x += xOffset;
                  obstacle.transform.position = startPos;
                }

                if (i % width == 0)
                {
                    startPos.x = -2.25f;
                    startPos.y += yOffset;
                    obstacle.transform.position = startPos;
                }

                if (i % (width * ObstacleHeight) == 0)
                {
                    startPos.z += 15f;
                    startPos.y = -.25f + yOffset;
                    obstacle.transform.position = startPos;
                }

                obstacle.transform.SetParent(platform.transform);
            }
        }

        private void InstantiateFinishPlatform(GameObject platform)
        {
            Vector3 spawnPos = new Vector3(0f,0f,platform.transform.GetChild(0).position.z + 5f);
            GameObject finishPlatform = Instantiate(finishPlatformPrefab, spawnPos, Quaternion.identity);
            finishPlatform.transform.SetParent(platform.transform);
        }

        private void InstantiateCollectableCube()
        {
            
        }
    }
}
