using System.Collections;
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
        
        [HideInInspector]public List<float> instantiatePositionCheckList;

        private int _obstacleSpawnAmount;
        
        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();

            _startPos = new Vector3(-2.25f, .25f, 20f);
            instantiatePositionCheckList = new List<float>();

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
            const float xStartPoint = -2.25f;
            
            _startPos.x += xOffset;

            if (i == 10) //first walls
            {
                instantiatePositionCheckList.Add(_startPos.z);
            }
            
            switch (i % 10) //other walls
            {
                case 0:
                    _startPos.z += zOffset;
                    _startPos.x = xStartPoint;
                    instantiatePositionCheckList.Add(_startPos.z);
                    break;
            }

            return _startPos;
        }
    }
}
