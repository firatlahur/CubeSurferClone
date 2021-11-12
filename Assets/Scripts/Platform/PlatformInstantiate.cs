using System;
using Core;
using UnityEngine;

namespace Platform
{
    public class PlatformInstantiate : MonoBehaviour
    {
        private GameManager _gameManager;

        public GameObject platformPrefab, platformContainer;
        public GameObject finishPlatformPrefab;

        private float _platformScaleModifier;
        private float _platformPositionModifier;
        [HideInInspector] public Transform finishLineTransform;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            
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
            
            finishLineTransform = platform.transform.GetChild(0);

            InstantiateFinishPlatform();
        }
        
        private void InstantiateFinishPlatform()
        {
            Vector3 spawnPos = new Vector3(0f,0f,finishLineTransform.position.z + 5f);
            GameObject finishPlatform = Instantiate(finishPlatformPrefab, spawnPos, Quaternion.identity);
            finishPlatform.transform.SetParent(platformContainer.transform);
        }
    }
}
