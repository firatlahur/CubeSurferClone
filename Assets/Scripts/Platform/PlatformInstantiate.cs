using Core;
using UI;
using UnityEngine;

namespace Platform
{
    public class PlatformInstantiate : MonoBehaviour
    {
        private GameManager _gameManager;
        [SerializeField] private InGameUIManager inGameUI;

        public GameObject platformPrefab, finishPlatformPrefab, platformContainer, stairPrefab, finishBonusScorePrefab;

        private float _platformScaleModifier;
        private float _platformPositionModifier;
        [HideInInspector] public Transform finishLineTransform;

        private enum Layers
        {
            Default = 0,
            Finish = 9,
            Stairs = 10
        };

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            InstantiatePlatform();
        }

        private void InstantiatePlatform()
        {
            GameObject platform = Instantiate(platformPrefab, Vector3.zero, Quaternion.identity);
            
            _platformScaleModifier = _gameManager.currentLevel * 4.5f;
            _platformPositionModifier = _platformScaleModifier * 5f;

            platform.transform.localScale = new Vector3(.5f, 1f, _platformScaleModifier);
            platform.transform.position = new Vector3(0f, 0f, _platformPositionModifier);
            platform.transform.SetParent(platformContainer.transform);
            platform.gameObject.layer = (int) Layers.Default;
            
            finishLineTransform = platform.transform.GetChild(0);

            InstantiateFinishPlatform();
        }

        private void InstantiateFinishPlatform()
        {
            Vector3 spawnPos = new Vector3(0f, 0f, finishLineTransform.position.z + 5f);
            
            GameObject finishPlatform = Instantiate(finishPlatformPrefab, spawnPos, Quaternion.identity);
            finishPlatform.transform.SetParent(platformContainer.transform);
            finishPlatform.gameObject.layer = (int) Layers.Default;

            
            GameObject stair = Instantiate(stairPrefab, finishPlatform.transform.position + new Vector3(0f, .25f, 0f),
                Quaternion.identity);
            stair.transform.SetParent(platformContainer.transform);
            stair.layer = (int) Layers.Stairs;

            
            GameObject finishBonusScore = Instantiate(finishBonusScorePrefab,
                stair.transform.position + new Vector3(-2.5f, 9.25f, 79f), Quaternion.identity);
            finishBonusScore.transform.SetParent(platformContainer.transform);
            finishBonusScore.layer = (int) Layers.Finish;
            
            inGameUI.platform = finishBonusScore; // to calculate level progress bar
        }
    }
}
