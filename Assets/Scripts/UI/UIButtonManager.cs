﻿using System.Collections;
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

        [Header("Start Panel")]
        [SerializeField] private GameObject startBackground;
        [SerializeField] private GameObject gameNameText;
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject goldText;
        [SerializeField] private TextMeshProUGUI goldAmountText;
        [SerializeField] private GameObject backButton;
        [SerializeField] private TextMeshProUGUI actualGameLevelText;
        [SerializeField] private TextMeshProUGUI currentLevelText;

        [Header("Skin Panel")]
        [SerializeField] private GameObject skinsButton;
        [SerializeField] private GameObject skinTypeListImage;
        [SerializeField] private GameObject obstacleSkins;
        [SerializeField] private GameObject collectableCubeSkins;

        public void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _inGameUIManager = FindObjectOfType<InGameUIManager>();
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            goldAmountText.text = _gameManager.totalGold.ToString();
            actualGameLevelText.text = _gameManager.currentLevel.ToString();
        }

        public void StartGame()
        {
            _gameManager.isGameStarted = true;
            
            startBackground.SetActive(false);
            backButton.SetActive(false);
            TurnOffStartingScreenItems();
            
            _inGameUIManager.InitiateInGameUI();
        }

        private void TurnOffStartingScreenItems()
        {
            gameNameText.SetActive(false);
            startButton.SetActive(false);
            goldText.SetActive(false);
            goldAmountText.gameObject.SetActive(false);
            skinsButton.SetActive(false);
            currentLevelText.gameObject.SetActive(false);
            actualGameLevelText.gameObject.SetActive(false);
        }

        public void Back()
        {
            gameNameText.SetActive(true);
            startButton.SetActive(true);
            goldText.SetActive(true);
            goldAmountText.gameObject.SetActive(true);
            skinsButton.SetActive(true);
            currentLevelText.gameObject.SetActive(true);
            actualGameLevelText.gameObject.SetActive(true);
            
            collectableCubeSkins.SetActive(false);
            backButton.SetActive(false);
            skinTypeListImage.SetActive(false);
            obstacleSkins.SetActive(false);
        }

        public void Skins()
        {
            TurnOffStartingScreenItems();
            skinsButton.SetActive(false);
            
            skinTypeListImage.SetActive(true);
            backButton.SetActive(true);
        }

        public void ObstacleSkins()
        {
            skinTypeListImage.SetActive(false);
            
            obstacleSkins.SetActive(true);
            goldText.SetActive(true);
            goldAmountText.gameObject.SetActive(true);
        }
        
        public void CollectableCubeSkins()
        {
            skinTypeListImage.SetActive(false);
            
            collectableCubeSkins.SetActive(true);
            goldText.SetActive(true);
            goldAmountText.gameObject.SetActive(true);
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
