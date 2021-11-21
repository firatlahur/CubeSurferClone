using System.Collections;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGameUIManager : MonoBehaviour
    {
        private GameManager _gameManager;
        public GameObject player;
        [HideInInspector] public GameObject platform;

        private Vector3 _endLinePosition;
        private float _fullDistance;

        public TextMeshProUGUI currentLevel;
        [SerializeField] private GameObject levelProgress;
        public Image levelProgressFill;
        
        [Header("Game Over Panel")]
        [SerializeField] private TextMeshProUGUI collectedGold;
        [SerializeField] private TextMeshProUGUI stairsCount;
        [SerializeField] private TextMeshProUGUI purpleScoreCount;
        [SerializeField] private TextMeshProUGUI collectedCubesCount;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Start() => _fullDistance = GetPathDistance();
        
        public void InitiateInGameUI()
        {
            currentLevel.text = "Level " + _gameManager.currentLevel;
            currentLevel.gameObject.SetActive(true);
            levelProgress.SetActive(true);
        }

        private float GetPathDistance()
        {
            return Vector3.Distance(player.transform.position, platform.transform.position);
        }

        private void CalculateDistance()
        {
            float newDistance = GetPathDistance();
            float progressValue = Mathf.InverseLerp(_fullDistance, 0f, newDistance - _gameManager.currentLevel * 10f);
            levelProgressFill.fillAmount = progressValue;
        }
        
        private void Update()
        {
            if (_gameManager.isGameStarted)
            {
                CalculateDistance();
            }
        }
        
        public void SetEndGameResults(int stairCount, int purpleCount, int collectedCubeCount)
        {
            int grandTotal = (stairCount + purpleCount + collectedCubeCount) * 2;
            stairsCount.text = stairCount.ToString();
            purpleScoreCount.text = purpleCount.ToString();
            collectedCubesCount.text = collectedCubeCount.ToString();

            collectedGold.text = "Gold: " + "+" + grandTotal;

            _gameManager.totalGold += grandTotal;
        }
    }
}
