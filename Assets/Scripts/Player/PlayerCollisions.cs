using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class PlayerCollisions : MonoBehaviour
    {
        private const int ObstacleLayer = 8;
        private const int CollectableCubeLayer = 11;
        private const int PlayerLayer = 12;
        private const int PurpleScoreLayer = 13;

        private List<GameObject> _collectedCubes;

        private void Awake()
        {
            _collectedCubes = new List<GameObject>();
            _collectedCubes.Add(transform.GetChild(0).gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == CollectableCubeLayer)
            {
                CollectableCube(other.gameObject);
            }
        }

        private void CollectableCube(GameObject collectableCube)
        {
            collectableCube.gameObject.layer = PlayerLayer;
            collectableCube.transform.parent = null;

            Vector3 lastCubePos = new Vector3(
                _collectedCubes.Last().transform.position.x,
                0f,
                _collectedCubes.Last().transform.position.z);

            collectableCube.transform.position = lastCubePos;
            collectableCube.transform.SetParent(transform);
            _collectedCubes.Add(collectableCube.gameObject);

            Transform player = transform;
            Vector3 playerPosition = player.position;

            playerPosition = new Vector3(playerPosition.x, playerPosition.y + .5f, playerPosition.z);
            player.position = playerPosition;
        }
    }
}
