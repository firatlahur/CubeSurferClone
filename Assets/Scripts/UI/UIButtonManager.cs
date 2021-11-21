using System;
using System.Collections;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace UI
{
    public class UIButtonManager : MonoBehaviour
    {
        private GameManager _gameManager;
        private InGameUIManager _inGameUIManager;

        #region StartPanel
        [Serializable]
        public struct StartPanel
        {
            [SerializeField] internal GameObject startBackground;
            [SerializeField] internal GameObject gameNameText;
            [SerializeField] internal GameObject startButton;
            [SerializeField] internal GameObject goldText;
            [SerializeField] internal TextMeshProUGUI goldAmountText;
            [SerializeField] internal GameObject backButton;
            [SerializeField] internal TextMeshProUGUI actualGameLevelText;
            [SerializeField] internal TextMeshProUGUI currentLevelText;
        }
        #endregion

        #region SkinPanel
        [Serializable]
        public struct SkinPanel
        {
            [SerializeField] internal GameObject skinsButton;
            [SerializeField] internal GameObject skinTypeListImage;
            [SerializeField] internal GameObject obstacleSkins;
            [SerializeField] internal GameObject collectableCubeSkins;
        }
        #endregion

        public StartPanel startPanel;
        public SkinPanel skinPanel;
        
        public void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _inGameUIManager = FindObjectOfType<InGameUIManager>();
        }

        private void Start()
        {
           startPanel.goldAmountText.text = _gameManager.totalGold.ToString();
           startPanel.actualGameLevelText.text = _gameManager.currentLevel.ToString();
        }

        public void StartGame()
        {
            _gameManager.isGameStarted = true;
            
            startPanel.startBackground.SetActive(false);
            startPanel.backButton.SetActive(false);
            TurnOffStartingScreenItems();
            
            _inGameUIManager.InitiateInGameUI();
        }

        private void TurnOffStartingScreenItems()
        {
            startPanel.gameNameText.SetActive(false);
            startPanel.startButton.SetActive(false);
            startPanel.goldText.SetActive(false);
            startPanel.goldAmountText.gameObject.SetActive(false);
            startPanel.currentLevelText.gameObject.SetActive(false);
            startPanel.actualGameLevelText.gameObject.SetActive(false);
            
            skinPanel.skinsButton.SetActive(false);
        }

        public void Back()
        {
            startPanel.gameNameText.SetActive(true);
            startPanel.startButton.SetActive(true);
            startPanel.goldText.SetActive(true);
            startPanel.goldAmountText.gameObject.SetActive(true);
            startPanel.currentLevelText.gameObject.SetActive(true);
            startPanel.actualGameLevelText.gameObject.SetActive(true);
            startPanel.backButton.SetActive(false);

            skinPanel.collectableCubeSkins.SetActive(false);
            skinPanel.skinTypeListImage.SetActive(false);
            skinPanel.obstacleSkins.SetActive(false);
            skinPanel.skinsButton.SetActive(true);
        }

        public void Skins()
        {
            TurnOffStartingScreenItems();
            skinPanel.skinsButton.SetActive(false);
            skinPanel.skinTypeListImage.SetActive(true);
            
            startPanel.backButton.SetActive(true);
        }

        public void ObstacleSkins()
        {
            skinPanel.skinTypeListImage.SetActive(false);
            skinPanel.obstacleSkins.SetActive(true);
            
            startPanel.goldText.SetActive(true);
            startPanel.goldAmountText.gameObject.SetActive(true);
        }
        
        public void CollectableCubeSkins()
        {
            skinPanel.skinTypeListImage.SetActive(false);
            skinPanel.collectableCubeSkins.SetActive(true);
            
            startPanel.goldText.SetActive(true);
            startPanel.goldAmountText.gameObject.SetActive(true);
        }

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void SkipLevel()
        {
            if (_gameManager.currentLevel < 5)
            {
                _gameManager.currentLevel++;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                _gameManager.currentLevel = Random.Range(1, 5);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
