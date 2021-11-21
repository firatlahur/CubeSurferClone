using System;
using System.Collections;
using Core;
using ScriptableObjects.CollectableCube;
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

        #region Red Obstacle Skin
        [Serializable]
        public struct RedObstacleSkin
        {
            [SerializeField] internal Button redSkinPurchaseButton;
            [SerializeField] internal Button redSkinUseButton;
            [SerializeField] internal TextMeshProUGUI redSkinPurchaseButtonText;
            [SerializeField] internal TextMeshProUGUI redSkinUseButtonText;
        }
        #endregion

        #region Green Obstacle Skin
        [Serializable]
        public struct GreenObstacleSkin
        {
            [SerializeField] internal Button greenSkinPurchaseButton;
            [SerializeField] internal Button greenSkinUseButton;
            [SerializeField] internal TextMeshProUGUI greenSkinPurchaseButtonText;
            [SerializeField] internal TextMeshProUGUI greenSkinUseButtonText;
        }
        #endregion

        #region Blue Collectable Skin
        [Serializable]
        public struct BlueCollectableSkin
        {
            [SerializeField] internal Button blueSkinPurchaseButton;
            [SerializeField] internal Button blueSkinUseButton;
            [SerializeField] internal TextMeshProUGUI blueSkinPurchaseButtonText;
            [SerializeField] internal TextMeshProUGUI blueSkinUseButtonText;
        }
        #endregion

        #region Orange Collectable Skin
        [Serializable]
        public struct OrangeCollectableSkin
        {
            [SerializeField] internal Button orangeSkinPurchaseButton;
            [SerializeField] internal Button orangeSkinUseButton;
            [SerializeField] internal TextMeshProUGUI orangeSkinPurchaseButtonText;
            [SerializeField] internal TextMeshProUGUI orangeSkinUseButtonText;
        }
        #endregion
        
        public RedObstacleSkin redObstacleSkin;
        public GreenObstacleSkin greenObstacleSkin;
        public BlueCollectableSkin blueCollectableSkin;
        public OrangeCollectableSkin orangeCollectableSkin;
        
        public void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Start() => SkinCheck();

        private void SkinCheck()
        {
            switch (_gameManager.obstacleSkin.name)
            {
                case "RedSkin":
                    redObstacleSkin.redSkinUseButtonText.text = "In Use";
                    redObstacleSkin.redSkinUseButton.interactable = false;
                    break;
                case "GreenSkin":
                    greenObstacleSkin.greenSkinUseButtonText.text = "In Use";
                    greenObstacleSkin.greenSkinUseButton.interactable = false;
                    break;
            }

            switch (_gameManager.collectableCubeSkin.name)
            {
                case "BlueSkin":
                    blueCollectableSkin.blueSkinUseButtonText.text = "In Use";
                    blueCollectableSkin.blueSkinUseButton.interactable = false;
                    break;
                case "OrangeSkin":
                    orangeCollectableSkin.orangeSkinUseButtonText.text = "In Use";
                    orangeCollectableSkin.orangeSkinUseButton.interactable = false;
                    break;
            }
            
            if (_gameManager.isPurchasedRedObstacleSkin)
            {
                redObstacleSkin.redSkinPurchaseButton.interactable = false;
                redObstacleSkin.redSkinPurchaseButtonText.text = "Owned";
            }
            
            if (_gameManager.isPurchasedGreenObstacleSkin)
            {
                greenObstacleSkin.greenSkinPurchaseButton.interactable = false;
                greenObstacleSkin.greenSkinPurchaseButtonText.text = "Owned";
            }
            
            if (_gameManager.isPurchasedBlueCollectableCubeSkin)
            {
                blueCollectableSkin.blueSkinPurchaseButton.interactable = false;
                blueCollectableSkin.blueSkinPurchaseButtonText.text = "Owned";
            }
            
            if (_gameManager.isPurchasedOrangeCollectableCubeSkin)
            {
                orangeCollectableSkin.orangeSkinPurchaseButton.interactable = false;
                orangeCollectableSkin.orangeSkinPurchaseButtonText.text = "Owned";
            }
        }

        public void PurchaseRedSkin()
        {
            if (_gameManager.totalGold >= 250 && !_gameManager.isPurchasedRedObstacleSkin)
            {
                _gameManager.obstacleSkin = Resources.Load("Obstacle/RedSkin") as ObstacleSkin;
                _gameManager.totalGold -= 250;
                redObstacleSkin.redSkinPurchaseButton.interactable = false;
                redObstacleSkin.redSkinPurchaseButtonText.text = "Owned";
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
                greenObstacleSkin.greenSkinPurchaseButton.interactable = false;
                greenObstacleSkin.greenSkinPurchaseButtonText.text = "Owned";
                _gameManager.isPurchasedGreenObstacleSkin = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                StartCoroutine(Error("Not enough gold."));
            }
        }
        
        public void PurchaseBlueSkin()
        {
            if (_gameManager.totalGold >= 250 && !_gameManager.isPurchasedBlueCollectableCubeSkin)
            {
                _gameManager.collectableCubeSkin = Resources.Load("CollectableCube/BlueSkin") as CollectableCubeSkin;
                _gameManager.totalGold -= 250;
                blueCollectableSkin.blueSkinPurchaseButton.interactable = false;
                blueCollectableSkin.blueSkinPurchaseButtonText.text = "Owned";
                _gameManager.isPurchasedBlueCollectableCubeSkin = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                StartCoroutine(Error("Not enough gold."));
            }
        }
        
        public void PurchaseOrangeSkin()
        {
            if (_gameManager.totalGold >= 250 && !_gameManager.isPurchasedOrangeCollectableCubeSkin)
            {
                _gameManager.collectableCubeSkin = Resources.Load("CollectableCube/OrangeSkin") as CollectableCubeSkin;
                _gameManager.totalGold -= 250;
                orangeCollectableSkin.orangeSkinPurchaseButton.interactable = false;
                orangeCollectableSkin.orangeSkinPurchaseButtonText.text = "Owned";
                _gameManager.isPurchasedOrangeCollectableCubeSkin = true;
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
                redObstacleSkin.redSkinUseButtonText.text = "In Use";
                redObstacleSkin.redSkinUseButton.interactable = false;
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
                greenObstacleSkin.greenSkinUseButtonText.text = "In Use";
                greenObstacleSkin.greenSkinUseButton.interactable = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                StartCoroutine(Error("You do not own this."));
            }
        }
        
        public void UseBlueSkin()
        {
            if (_gameManager.isPurchasedBlueCollectableCubeSkin && _gameManager.collectableCubeSkin.name != "BlueSkin")
            {
                _gameManager.collectableCubeSkin = Resources.Load("CollectableCube/BlueSkin") as CollectableCubeSkin;
                blueCollectableSkin.blueSkinUseButtonText.text = "In Use";
                blueCollectableSkin.blueSkinUseButton.interactable = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                StartCoroutine(Error("You do not own this."));
            }
        }
        
        public void UseOrangeSkin()
        {
            if (_gameManager.isPurchasedOrangeCollectableCubeSkin && _gameManager.collectableCubeSkin.name != "OrangeSkin")
            {
                _gameManager.collectableCubeSkin = Resources.Load("CollectableCube/OrangeSkin") as CollectableCubeSkin;
                orangeCollectableSkin.orangeSkinUseButtonText.text = "In Use";
                orangeCollectableSkin.orangeSkinUseButton.interactable = false;
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
