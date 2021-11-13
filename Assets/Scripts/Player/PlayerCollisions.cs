using System;
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
        
        public GameObject platformContainer, go;

        public int count;

        private const int ObstacleLayer = 8;
        private const int CollisionTrackerLayer = 9;
        private const int CollectableCubeLayer = 11;
        private const int PlayerLayer = 12;
        private const int PurpleScoreLayer = 13;

        public List<GameObject> _collectedCubes;
        private List<GameObject> _obstacleList;
        private Dictionary<GameObject, int> _obstacleDict;

        private GameObject _veryFirstCollectedCube;
        private int _valuePair;
        private bool _isCollided;

        private const float YOffsetCollectableCube = .25f;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _veryFirstCollectedCube = transform.GetChild(0).gameObject;
            _collectedCubes = new List<GameObject> { _veryFirstCollectedCube };
            _obstacleDict = new Dictionary<GameObject, int>();
            _obstacleList = new List<GameObject>();
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
                YOffsetCollectableCube,
                _collectedCubes.Last().transform.position.z);

            Transform player = transform;
            Vector3 playerPosition = player.position;

            collectableCube.transform.SetParent(player);
            //collectableCube.transform.SetSiblingIndex(0);

            playerPosition = new Vector3(playerPosition.x, playerPosition.y + .5f, playerPosition.z);
            player.position = playerPosition;
            collectableCube.transform.position = lastCubePos;

            _collectedCubes.Add(collectableCube.gameObject);
        }
        
        
        private bool col;

        private void Obstacle(GameObject obstacle)
        {
            if (obstacle.gameObject.layer == ObstacleLayer && !col)
            {
                obstacle.GetComponent<BoxCollider>().enabled = false;
                _obstacleList.Add(obstacle);

                if (_obstacleList.Count < 2)
                {
                    return;
                }

                foreach (GameObject gObj in _obstacleList)
                {
                    _obstacleDict.Add(gObj, gObj.transform.childCount);
                }

                int max = _obstacleDict.Values.Max();


                Debug.Log("max: " + max);

                float xOffset = 0;

                foreach (KeyValuePair<GameObject, int> obstacleDictKey in _obstacleDict)
                {
                    if (obstacleDictKey.Value == max)
                    {
                        xOffset = obstacleDictKey.Key.gameObject.transform.position.x;
                    }
                }

                for (int i = 0; i < max; i++)
                {
                    if (_collectedCubes.Count < max)
                    {
                        _gameManager.isGameStarted = false;
                        Debug.Log("game over");
                        Debug.Log("place game over materials");
                    }
                    else
                    {
                        _collectedCubes[_collectedCubes.Count - 1].gameObject.transform
                            .SetParent(platformContainer.transform);
                        
                        _collectedCubes[_collectedCubes.Count - 1].transform.position += new Vector3(0f, .5f * i, 0f);

                        _collectedCubes[_collectedCubes.Count - 1].transform.position = new Vector3(
                            xOffset,
                            _collectedCubes[_collectedCubes.Count - 1].transform.position.y,
                            _collectedCubes[_collectedCubes.Count - 1].transform.position.z);

                        _collectedCubes.RemoveAt(_collectedCubes.Count - 1);
                        
                        transform.position -= new Vector3(0f, .5f, 0f);
                    }
                }
                _obstacleDict.Clear();
                _obstacleList.Clear();
                StartCoroutine(nameof(TurnOffCollision));
            }
        }


        private IEnumerator TurnOffCollision()
        {
            col = true;
            yield return new WaitForSecondsRealtime(1f);
            col = false;
        }
    }
}
