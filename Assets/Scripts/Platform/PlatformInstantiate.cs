using System;
using Core;
using UnityEngine;

namespace Platform
{
    public class PlatformInstantiate : MonoBehaviour
    {
        private GameManager _gameManager;

        private PlatformMovement _platformMovement;

        public GameObject platformPrefab, finishPlatformPrefab, platformContainer, stairPrefab, finishBonusScorePrefab;

        private float _platformScaleModifier;
        private float _platformPositionModifier;
        [HideInInspector] public Transform finishLineTransform;

        private const int DefaultLayer = 0;
        private const int FinishLayer = 9;
        private const int StairLayer = 10;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _platformMovement = FindObjectOfType<PlatformMovement>();
            
            _platformScaleModifier = _gameManager.currentLevel * 4.5f;
            _platformPositionModifier = _platformScaleModifier * 5f;
        }

        private void Start()
        {
            InstantiatePlatform();
        }

        private void InstantiatePlatform()
        {
            GameObject platform = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);

            platform.transform.localScale = new Vector3(.5f, 1f, _platformScaleModifier);
            platform.transform.position = new Vector3(0f, 0f, _platformPositionModifier);
            platform.name = "Platform";
            platform.transform.SetParent(platformContainer.transform);
            platform.gameObject.layer = DefaultLayer;
            
            finishLineTransform = platform.transform.GetChild(0);

            InstantiateFinishPlatform();
        }

        private void InstantiateFinishPlatform()
        {
            Vector3 spawnPos = new Vector3(0f, 0f, finishLineTransform.position.z + 5f);
            
            GameObject finishPlatform = Instantiate(finishPlatformPrefab, spawnPos, Quaternion.identity);
            finishPlatform.transform.SetParent(platformContainer.transform);
            finishPlatform.gameObject.layer = DefaultLayer;

            GameObject stair = Instantiate(stairPrefab, finishPlatform.transform.position + new Vector3(0f, .25f, 0f),
                Quaternion.identity);
            stair.transform.SetParent(platformContainer.transform);
            stair.layer = StairLayer;

            GameObject finishBonusScore = Instantiate(finishBonusScorePrefab,
                stair.transform.position + new Vector3(-2f, 9.25f, 79f), Quaternion.identity);
            finishBonusScore.transform.SetParent(platformContainer.transform);
            finishBonusScore.layer = FinishLayer;
        }
    }
}
