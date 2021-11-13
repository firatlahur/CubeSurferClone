using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.CollectableCube;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

namespace Player
{
    public class PlayerCollisions : MonoBehaviour
    {
        public GameObject platformContainer, collisionTracker;

        private const int ObstacleLayer = 8;
        private const int CollectableCubeLayer = 11;
        private const int PlayerLayer = 12;
        private const int PurpleScoreLayer = 13;

        private List<GameObject> _collectedCubes;
        private GameObject _veryFirstCollectedCube;

        private const float YOffsetCollectableCube = .25f;

        private void Awake()
        {
            _veryFirstCollectedCube = transform.GetChild(0).gameObject;
            _collectedCubes = new List<GameObject> { _veryFirstCollectedCube };
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.layer)
            {
                case CollectableCubeLayer:
                    CollectableCube(other.gameObject);
                    break;
                // case ObstacleLayer:
                //     Obstacle(other.gameObject);
                //     break;
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

            playerPosition = new Vector3(playerPosition.x, playerPosition.y + .5f, playerPosition.z);
            player.position = playerPosition;
            collectableCube.transform.position = lastCubePos;

            _collectedCubes.Add(collectableCube.gameObject);
        }

        private void Obstacle(GameObject obstacle)
        {
            if (obstacle.transform.childCount > 0)
            {
                foreach (Transform childObstacle in obstacle.transform)
                {
                    childObstacle.GetComponent<BoxCollider>().enabled = false;
                }
            }
            else
            {
                obstacle.GetComponent<BoxCollider>().enabled = false;
            }
            
            int count = 0;
            count++;
            
            for (int i = 0; i < count; i++)
            {
                _collectedCubes[i].transform.parent = null;
                _collectedCubes[i].transform.SetParent(platformContainer.transform);
                _collectedCubes[i].transform.position = obstacle.transform.position + new Vector3(0f, 0f, -.5f);
                _collectedCubes.Remove(_collectedCubes[i]);
                //transform.position -= new Vector3(0f, -.5f, 0f);
            }
        }
    }
}
