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
        

        [Header("Red Skin")]
        public Button redSkinPurchaseButton;
        public Button redSkinUseButton;
        public TextMeshProUGUI redSkinPurchaseButtonText;
        public TextMeshProUGUI redSkinUseButtonText;

        public void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
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

        public void UseRedSkin()
        {
            if (_gameManager.isPurchasedRedObstacleSkin && _gameManager.obstacleSkin.name != "RedSkin")
            {
                _gameManager.obstacleSkin = Resources.Load("Obstacle/RedSkin") as ObstacleSkin;
                redSkinUseButtonText.text = "In Use";
                redSkinUseButton.interactable = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
