using System.Collections;
using Core;
using Obstacle;
using Platform;
using ScriptableObjects.CollectableCube;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CollectableCube
{
    public class CollectableCubeInstantiate : MonoBehaviour
    {
        public GameObject platformContainer;

        private GameManager _gameManager;
        
        private CollectableCubeSkin _collectableCube;
        private PlatformInstantiate _finishLine;
        public ObstacleInstantiate obstacleInstantiate;

        private Vector3 _startPos;

        private int _collectableCubeSpawnAmount;
        private float _finishLinePosition, _zOffset;

        private const int OneRowOfWall = 10;
        
        private const float PlatformScale = 2.25f;
        private const float SpawnDistanceOffset = 2.5f;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _startPos = new Vector3(0f, .25f, 0f);
            _finishLine = GetComponent<PlatformInstantiate>();

            _collectableCubeSpawnAmount = _gameManager.currentLevel * 2 * 10;
            _zOffset = 5f;
        }

        private IEnumerator Start() //_finishLinePosition needs to have platform spawned already
        {
            yield return new WaitForEndOfFrame();
            _finishLinePosition = _finishLine.finishLineTransform.position.z - 10f;

            _collectableCube = _gameManager.collectableCubeSkin;
            InstantiateCollectableCube();
        }

        private void InstantiateCollectableCube()
        {
            for (int i = 0; i < _collectableCubeSpawnAmount; i++)
            {
                Vector3 positionTest = new Vector3();

                GameObject collectableCube = Instantiate(
                    _collectableCube.collectableCubeSkin[
                        Random.Range(0, _collectableCube.collectableCubeSkin.Count)], Vector3.zero,
                    Quaternion.identity);

                collectableCube.transform.position = CalculateCollectableCubePosition(i,positionTest);
                collectableCube.transform.SetParent(platformContainer.transform);

                obstacleInstantiate.instantiatePositionCheckList.Add(collectableCube.transform.position.z);
            }
        }

        private Vector3 CalculateCollectableCubePosition(int i, Vector3 positionTest)
        {
            bool isSpawnNearAnotherObj = false;

            foreach (float zPos in obstacleInstantiate.instantiatePositionCheckList)
            {
                float spawnDistance = Vector3.Distance(positionTest,
                    new Vector3(0f, 0f, zPos));

                if (spawnDistance < SpawnDistanceOffset)
                {
                    isSpawnNearAnotherObj = true;
                    break;
                }
            }

            if (isSpawnNearAnotherObj)
            {
                _collectableCubeSpawnAmount--;
            }

            int totalWallCount = _gameManager.currentLevel * 2 + 1;
            const float zOffset = 20f;
            _startPos.x = Random.Range(-PlatformScale, PlatformScale);
            
            if (i / totalWallCount == OneRowOfWall)
            {
                _zOffset += zOffset;
            }

            _startPos.z = Random.Range(_zOffset, _finishLinePosition);

            return _startPos;
        }
    }
}
