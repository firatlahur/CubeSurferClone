using System.Collections;
using Core;
using Platform;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PinkScore
{
    public class PinkScoreInstantiate : MonoBehaviour
    {
        private GameManager _gameManager;

        public GameObject platformContainer, pinkScorePrefab;
        
        private PlatformInstantiate _finishLine;
        
        private Vector3 _startPos;
        private float _finishLinePosition, _zOffset;
        private int _pinkScoreSpawnAmount;

        private const float PlatformOffset = 2.25f;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _startPos = new Vector3(0f, .25f, 0f);
            _finishLine = GetComponent<PlatformInstantiate>();
            _zOffset = 5f;
            _pinkScoreSpawnAmount = _gameManager.currentLevel * 2 * 3;
        }

        private IEnumerator Start() //_finishLinePosition needs to have platform spawned already
        {
            yield return new WaitForEndOfFrame();
            _finishLinePosition = _finishLine.finishLineTransform.position.z - 10f;
            
            InstantiatePinkScore();
        }

        private void InstantiatePinkScore()
        {
            for (int i = 0; i < _pinkScoreSpawnAmount; i++)
            {
                GameObject pinkScore = Instantiate(pinkScorePrefab, Vector3.zero, Quaternion.identity);
                
                pinkScore.transform.position = CalculatePinkScorePosition(i);
                pinkScore.transform.SetParent(platformContainer.transform);
            }
        }

        private Vector3 CalculatePinkScorePosition(int i)
        {
            _startPos.x = Random.Range(-PlatformOffset, PlatformOffset);
            _startPos.z = Random.Range(_zOffset, _finishLinePosition);
            return _startPos;
        }
    }
}
