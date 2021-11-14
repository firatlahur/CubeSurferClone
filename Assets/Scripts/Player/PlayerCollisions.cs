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
        
        public GameObject platformContainer, platformMovement;

        private const int ObstacleLayer = 8;
        private const int FinishLayer = 9;
        private const int StairsLayer = 10;
        private const int CollectableCubeLayer = 11;
        private const int PlayerLayer = 12;
        private const int PurpleScoreLayer = 13;
        
        private const float CollectableCubeYOffset = .25f;
        private const float PlayerYOffset = .5f;

        private List<GameObject> _collectedCubes;
        private Dictionary<GameObject, float> _obstacleDict;

        private GameObject _veryFirstCollectedCube;
        private bool _isCollided;

        private int _collectedCubesChildCount;
        [HideInInspector] public int stairCount;
        [HideInInspector] public int purpleScoreCount;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _veryFirstCollectedCube = transform.GetChild(0).gameObject;
            _collectedCubes = new List<GameObject> { _veryFirstCollectedCube };
            _obstacleDict = new Dictionary<GameObject, float>();
            _collectedCubesChildCount = 1;
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
                case StairsLayer:
                    Stairs(other.gameObject);
                    break;
                case FinishLayer:
                    Finish();
                    break;
                case PurpleScoreLayer:
                    PurpleScore(other.gameObject);
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

                float nearestObstacleZPosition = _obstacleDict.Values.Min();
                
                Vector3 firstCollidedCubePosition = new Vector3();
                

                foreach (KeyValuePair<GameObject, float> obstacleDictKey in _obstacleDict)
                {
                    if (obstacleDictKey.Value == nearestObstacleZPosition)
                    {
                        GameObject firstCollidedCube = obstacleDictKey.Key.gameObject;

                        _collectedCubesChildCount = obstacleDictKey.Key.gameObject.transform.childCount;
                        firstCollidedCubePosition = firstCollidedCube.transform.position;
                    }
                }

                for (int i = 0; i < _collectedCubesChildCount; i++)
                {
                    if (transform.childCount <= 1)
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
                
                StartCoroutine(nameof(ObstacleCollisionPause));
            }
        }
        
        private IEnumerator ObstacleCollisionPause()
        {
            _isCollided = true;
            yield return new WaitForSecondsRealtime(.5f);
            _obstacleDict.Clear();
            _isCollided = false;
        }

        private void Stairs(GameObject stair)
        {
            foreach (KeyValuePair<GameObject, float> obstacleDictKey in _obstacleDict)
            {
                _collectedCubesChildCount = obstacleDictKey.Key.gameObject.transform.childCount;
            }
            
            
            for (int i = 0; i < 1; i++)
            {
                stair.GetComponent<BoxCollider>().enabled = false;
                stairCount++;
                
                if (transform.childCount <= 1)
                {
                    _gameManager.isGameStarted = false;
                    Debug.Log("game ENDED");
                    Debug.Log("place game ENDED materials");
                }

                Transform collectedCubesTransform = _collectedCubes[_collectedCubes.Count - 1].transform;
                
                collectedCubesTransform.transform.parent = null; 
                collectedCubesTransform.SetParent(stair.transform);

                collectedCubesTransform.position = new Vector3(
                    transform.position.x,
                    stair.transform.position.y,
                    transform.position.z -.2f);

                _collectedCubes.RemoveAt(_collectedCubes.Count - 1);
            }
        }

        private void Finish()
        {
            Debug.Log("GAME ENDED X20");
            StartCoroutine(nameof(FinishPostpone));
        }

        private IEnumerator FinishPostpone()
        {
            yield return new WaitForSecondsRealtime(.2f);
            _gameManager.isGameStarted = false;
            Debug.Log("current cubes: " + transform.childCount);
            Debug.Log("purples collected:" + purpleScoreCount);
        }

        private void PurpleScore(GameObject purpleScore)
        {
            purpleScoreCount++;
            purpleScore.gameObject.SetActive(false);
        }
    }
}
