﻿using System;
using System.Collections;
using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIButtonManager : MonoBehaviour
    {
        private GameManager _gameManager;

        [Header("Start Panel")]
        public GameObject startBackground;
        public GameObject gameNameText;
        public GameObject startButton;
        public GameObject goldText;
        public TextMeshProUGUI goldAmountText;
        public GameObject backButton;
        public TextMeshProUGUI actualGameLevelText;
        public TextMeshProUGUI currentLevelText;

        [Header("Skin Panel")]
        public GameObject skinsButton;
        public GameObject skinTypeListImage;
        public GameObject obstacleSkins;
        public GameObject collectableCubeSkins;

        public void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
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


            if (skinTypeListImage.activeInHierarchy)
            {
                skinTypeListImage.SetActive(false);
            }

            if (obstacleSkins.activeInHierarchy)
            {
                obstacleSkins.SetActive(false);
            }
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
    }
}
