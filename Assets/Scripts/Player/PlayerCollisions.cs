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
        private Dictionary<GameObject, float> _obstacleDict;

        private GameObject _veryFirstCollectedCube;
        private int _valuePair;
        private bool _isCollided;

        private const float YOffsetCollectableCube = .25f;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _veryFirstCollectedCube = transform.GetChild(0).gameObject;
            _collectedCubes = new List<GameObject> { _veryFirstCollectedCube };
            _obstacleDict = new Dictionary<GameObject, float>();
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

                foreach (GameObject gObj in _obstacleList) // digeri de child counta gore value = childCount
                {
                    _obstacleDict.Add(gObj, Vector3.Distance(transform.position,gObj.transform.position));
                }

                float min = _obstacleDict.Values.Min();
                
                
                Debug.Log("max: " + min);

                Vector3 offset = new Vector3();
                int childCount = 0;

                foreach (KeyValuePair<GameObject, float> obstacleDictKey in _obstacleDict)
                {
                    if (obstacleDictKey.Value == min)
                    {
                        GameObject other = obstacleDictKey.Key.gameObject;

                        childCount = obstacleDictKey.Key.gameObject.transform.childCount;
                        offset = other.transform.position;
                    }
                }

                for (int i = 0; i < childCount; i++)
                {
                    if (_collectedCubes.Count < childCount)
                    {
                        _gameManager.isGameStarted = false;
                        Debug.Log("game over");
                        Debug.Log("place game over materials");
                    }
                    else
                    {
                        Transform go = _collectedCubes[_collectedCubes.Count - 1].gameObject.transform;
                        
                        go.SetParent(platformContainer.transform);

                        float y = go.position.y + (.5f * i);
                        
                        go.position = new Vector3(
                            offset.x,
                            y,
                            offset.z - .5f);

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
