using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Player
{
    public class PlayerCollisions : MonoBehaviour
    {
        private GameManager _gameManager;
        
        public GameObject platformContainer;

        private const int ObstacleLayer = 8;
        private const int CollectableCubeLayer = 11;
        private const int PlayerLayer = 12;
        private const int PurpleScoreLayer = 13;
        
        private const float CollectableCubeYOffset = .25f;
        private const float PlayerYOffset = .5f;

        private List<GameObject> _collectedCubes;
        private Dictionary<GameObject, float> _obstacleDict;

        private GameObject _veryFirstCollectedCube;
        private bool _isCollided;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _veryFirstCollectedCube = transform.GetChild(0).gameObject;
            _collectedCubes = new List<GameObject> { _veryFirstCollectedCube };
            _obstacleDict = new Dictionary<GameObject, float>();
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.layer)
            {
                case CollectableCubeLayer:
                    CollectableCube(other.gameObject);
                    break;
                case ObstacleLayer:
                    Obstacle(other.gameObject);
                    break;
            }
        }

        private void CollectableCube(GameObject collectableCube)
        {
            if (collectableCube.transform.childCount > 0)
            {
                foreach (Transform collectableCubeChild in collectableCube.transform)
                {
                    collectableCubeChild.transform.parent = null;
                    collectableCube.transform.DetachChildren();
                }
            }
            else
            {
                collectableCube.transform.parent = null;
            }
            
            collectableCube.gameObject.layer = PlayerLayer;

            Vector3 lastCubePos = new Vector3(
                _collectedCubes.Last().transform.position.x,
                CollectableCubeYOffset,
                _collectedCubes.Last().transform.position.z);

            Transform player = transform;
            Vector3 playerPosition = player.position;

            collectableCube.transform.SetParent(player);

            playerPosition = new Vector3(playerPosition.x, playerPosition.y + PlayerYOffset, playerPosition.z);
            player.position = playerPosition;
            collectableCube.transform.position = lastCubePos;

            _collectedCubes.Add(collectableCube.gameObject);
        }
        
        

        private void Obstacle(GameObject obstacle)
        {
            if (!_isCollided)
            {
                obstacle.GetComponent<BoxCollider>().enabled = false;
                
                _obstacleDict.Add(obstacle,Vector3.Distance(transform.position,obstacle.transform.position));

                if (_obstacleDict.Count < 2)
                {
                    return;
                }

                float nearestObstacleZPosition = _obstacleDict.Values.Min();
                
                Vector3 firstCollidedCubePosition = new Vector3();
                int collectedCubesChildCount = 0;

                foreach (KeyValuePair<GameObject, float> obstacleDictKey in _obstacleDict)
                {
                    if (obstacleDictKey.Value == nearestObstacleZPosition)
                    {
                        GameObject firstCollidedCube = obstacleDictKey.Key.gameObject;

                        collectedCubesChildCount = obstacleDictKey.Key.gameObject.transform.childCount;
                        firstCollidedCubePosition = firstCollidedCube.transform.position;
                    }
                }

                for (int i = 0; i < collectedCubesChildCount; i++)
                {
                    if (_collectedCubes.Count < collectedCubesChildCount)
                    {
                        _gameManager.isGameStarted = false;
                        Debug.Log("game over");
                        Debug.Log("place game over materials");
                    }
                    else
                    {
                        Transform collectedCubesTransform = _collectedCubes[_collectedCubes.Count - 1].transform;

                        collectedCubesTransform.SetParent(platformContainer.transform);

                        float collectedCubesYOffset = collectedCubesTransform.position.y + (PlayerYOffset * i);

                        collectedCubesTransform.position = new Vector3(
                            firstCollidedCubePosition.x,
                            collectedCubesYOffset,
                            firstCollidedCubePosition.z - PlayerYOffset);

                        _collectedCubes.RemoveAt(_collectedCubes.Count - 1);

                        transform.position -= new Vector3(0f, PlayerYOffset, 0f);
                    }
                }

                _obstacleDict.Clear();
                StartCoroutine(nameof(ObstacleCollisionPause));
            }
        }


        private IEnumerator ObstacleCollisionPause()
        {
            _isCollided = true;
            yield return new WaitForSecondsRealtime(1f);
            _isCollided = false;
        }
    }
}
