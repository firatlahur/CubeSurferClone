using System;
using System.Collections;
using Core;
using Platform;
using Player;
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
        public GameObject levelProgress;
        public Image levelProgressFill;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            _fullDistance = GetPathDistance();
        }

        private void Update()
        {
            if (_gameManager.isGameStarted)
            {
                CalculateDistance();
            }
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

        public void InitiateInGameUI()
        {
            currentLevel.text = "Level " + _gameManager.currentLevel;
            currentLevel.gameObject.SetActive(true);
            levelProgress.SetActive(true);
        }
    }
}
