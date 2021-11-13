using System;
using System.Collections;
using System.Collections.Generic;
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
                Vector3 testPos = CalculateCollectableCubePosition(i);

                bool x = false;

                for (int j = 0; j < obstacleInstantiate.zOffsetList.Count; j++)
                {
                    float dist = Vector3.Distance(testPos, new Vector3(0f, 0f, obstacleInstantiate.zOffsetList[j]));
                    
                    if(dist < 2.5f)
                    {
                        x = true;
                        Debug.Log(dist);
                        break;
                    }
                }
                if (x)
                {
                    _collectableCubeSpawnAmount--;
                    continue;
                }

                GameObject collectableCube = Instantiate(
                    _collectableCube.collectableCubeSkin[
                        Random.Range(0, _collectableCube.collectableCubeSkin.Count)], Vector3.zero,
                    Quaternion.identity);
                collectableCube.transform.position = testPos;
                collectableCube.transform.SetParent(platformContainer.transform);
                obstacleInstantiate.zOffsetList.Add(collectableCube.transform.position.z);
            }
        }

        private Vector3 CalculateCollectableCubePosition(int i)
        {
            int wallCount = _gameManager.currentLevel * 2 + 1;
            _startPos.x = Random.Range(-2.25f, 2.25f);
            
            if (i / wallCount == 10)
            {
                _zOffset += 20f;
            }

            _startPos.z = Random.Range(_zOffset, _finishLinePosition);

            return _startPos;
        }
    }
}
