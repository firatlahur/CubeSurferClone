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
        private GameObject _veryFirstCollectedCube;

        private const float YOffset = .25f;

        private void Awake()
        {
            _veryFirstCollectedCube = transform.GetChild(0).gameObject;
            _collectedCubes = new List<GameObject> { _veryFirstCollectedCube };
            Debug.Log(_collectedCubes.Count);
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.layer)
            {
                case CollectableCubeLayer:
                    other.transform.parent = null;
                    CollectableCube(other.gameObject);
                    break;
                case ObstacleLayer:
                    Obstacle(other.gameObject);
                    break;
            }
        }


        private void CollectableCube(GameObject collectableCube)
        {
            collectableCube.gameObject.layer = PlayerLayer;

            Vector3 lastCubePos = new Vector3(
                _collectedCubes.Last().transform.position.x,
                YOffset,
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
            int count = 0;
            count++;
            
            obstacle.GetComponent<BoxCollider>().enabled = false;

            for (int i = 0; i < count; i++)
            {
                _collectedCubes[i].transform.parent = null;
                _collectedCubes[i].transform.SetParent(obstacle.transform);
            }
            

        }
    }
}
