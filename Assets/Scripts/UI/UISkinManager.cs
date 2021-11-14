using System;
using System.Collections;
using Core;
using ScriptableObjects.Obstacle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UISkinManager : MonoBehaviour
    {
        private GameManager _gameManager;
        public TextMeshProUGUI errorText;

        [Header("Red Obstacle Skin")]
        public Button redSkinPurchaseButton;
        public Button redSkinUseButton;
        public TextMeshProUGUI redSkinPurchaseButtonText;
        public TextMeshProUGUI redSkinUseButtonText;
        
        [Header("Green Obstacle Skin")]
        public Button greenSkinPurchaseButton;
        public Button greenSkinUseButton;
        public TextMeshProUGUI greenSkinPurchaseButtonText;
        public TextMeshProUGUI greenSkinUseButtonText;
        
        public void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
        
        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            SkinCheck();
        }

        private void SkinCheck()
        {
            if (_gameManager.isPurchasedRedObstacleSkin)
            {
                redSkinPurchaseButton.interactable = false;
                redSkinPurchaseButtonText.text = "Owned";
            }

            if (_gameManager.obstacleSkin.name == "RedSkin")
            {
                redSkinUseButtonText.text = "In Use";
                redSkinUseButton.interactable = false;
            }
            
            if (_gameManager.isPurchasedGreenObstacleSkin)
            {
                greenSkinPurchaseButton.interactable = false;
                greenSkinPurchaseButtonText.text = "Owned";
            }

            if (_gameManager.obstacleSkin.name == "GreenSkin")
            {
                greenSkinUseButtonText.text = "In Use";
                greenSkinUseButton.interactable = false;
            }
        }

        public void PurchaseRedSkin()
        {
            if (_gameManager.totalGold >= 250 && !_gameManager.isPurchasedRedObstacleSkin)
            {
                _gameManager.obstacleSkin = Resources.Load("Obstacle/RedSkin") as ObstacleSkin;
                _gameManager.totalGold -= 250;
                redSkinPurchaseButton.interactable = false;
                redSkinPurchaseButtonText.text = "Owned";
                _gameManager.isPurchasedRedObstacleSkin = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                StartCoroutine(Error("Not enough gold."));
            }
        }

        public void PurchaseGreenSkin()
        {
            if (_gameManager.totalGold >= 250 && !_gameManager.isPurchasedGreenObstacleSkin)
            {
                _gameManager.obstacleSkin = Resources.Load("Obstacle/GreenSkin") as ObstacleSkin;
                _gameManager.totalGold -= 250;
                greenSkinPurchaseButton.interactable = false;
                greenSkinPurchaseButtonText.text = "Owned";
                _gameManager.isPurchasedGreenObstacleSkin = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                StartCoroutine(Error("Not enough gold."));
            }
        }

        public void UseRedSkin()
        {
            if (_gameManager.isPurchasedRedObstacleSkin && _gameManager.obstacleSkin.name != "RedSkin")
            {
                _gameManager.obstacleSkin = Resources.Load("Obstacle/RedSkin") as ObstacleSkin;
                redSkinUseButtonText.text = "In Use";
                redSkinUseButton.interactable = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                StartCoroutine(Error("You do not own this."));
            }
        }
        
        public void UseGreenSkin()
        {
            if (_gameManager.isPurchasedGreenObstacleSkin && _gameManager.obstacleSkin.name != "GreenSkin")
            {
                _gameManager.obstacleSkin = Resources.Load("Obstacle/GreenSkin") as ObstacleSkin;
                greenSkinUseButtonText.text = "In Use";
                greenSkinUseButton.interactable = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                StartCoroutine(Error("You do not own this."));
            }
        }

        private IEnumerator Error(string message)
        {
            errorText.gameObject.SetActive(true);
            errorText.text = message;
            yield return new WaitForSecondsRealtime(1f);
            errorText.gameObject.SetActive(false);
        }
    }
}
