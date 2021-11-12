using System.Collections;
using Core;
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
                GameObject collectableCube = Instantiate(_collectableCube.collectableCubeSkin[Random.Range(0,_collectableCube.collectableCubeSkin.Count)], Vector3.zero,
                    Quaternion.identity);
                
                collectableCube.transform.position = CalculateCollectableCubePosition(i);
                collectableCube.transform.SetParent(platformContainer.transform);
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
