using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerCollisions : MonoBehaviour
    {
        private GameManager _gameManager;
        
        public GameObject platformContainer, obstacleGameOver, stairsGameOver;

        public InGameUIManager inGameUIManager;

        private enum Layers
        {
            Obstacle = 8,
            Finish = 9,
            Stairs = 10,
            CollectableCube = 11,
            Player = 12,
            PurpleScore = 13
        };

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
                case (int) Layers.CollectableCube:
                    CollectableCube(other.gameObject);
                    break;
                case (int) Layers.Obstacle:
                    Obstacle(other.gameObject);
                    break;
                case (int) Layers.Stairs:
                    Stairs(other.gameObject);
                    break;
                case (int) Layers.Finish:
                    Finish();
                    break;
                case (int) Layers.PurpleScore:
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
            
            collectableCube.gameObject.layer = (int) Layers.Player;

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
                        obstacleGameOver.SetActive(true);
                        transform.DetachChildren();
                        transform.GetComponent<Rigidbody>().useGravity = true;
                        transform.GetComponent<Rigidbody>().AddForce(0f,5f,-25f);
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
            for (int i = 0; i < 1; i++)
            {
                stair.GetComponent<BoxCollider>().enabled = false;
                stairCount++;
                Transform player = transform;

                if (player.childCount <= 1)
                {
                    _gameManager.isGameStarted = false;
                    Finish();
                }

                Transform collectedCubesTransform = _collectedCubes[_collectedCubes.Count - 1].transform;
                
                collectedCubesTransform.transform.parent = null; 
                collectedCubesTransform.SetParent(stair.transform);

                Vector3 playerPosition = player.position;
                
                collectedCubesTransform.position = new Vector3(
                    playerPosition.x,
                    stair.transform.position.y,
                    playerPosition.z -.2f);

                _collectedCubes.RemoveAt(_collectedCubes.Count - 1);
            }
        }

        private void Finish()
        {
            inGameUIManager.SetEndGameResults(stairCount, purpleScoreCount, transform.childCount);
            StartCoroutine(nameof(FinishPostpone));
        }

        private IEnumerator FinishPostpone()
        {
            yield return new WaitForSecondsRealtime(.2f);
            _gameManager.isGameStarted = false;
            stairsGameOver.SetActive(true);
        }

        private void PurpleScore(GameObject purpleScore)
        {
            purpleScoreCount++;
            purpleScore.gameObject.SetActive(false);
        }
    }
}
