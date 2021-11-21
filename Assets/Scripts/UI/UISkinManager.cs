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

        [Header("Red Obstacle Skin")] 
        [SerializeField] private Button redSkinPurchaseButton;
        [SerializeField] private Button redSkinUseButton;
        [SerializeField] private TextMeshProUGUI redSkinPurchaseButtonText;
        [SerializeField] private TextMeshProUGUI redSkinUseButtonText;
        
        [Header("Green Obstacle Skin")]
        [SerializeField] private Button greenSkinPurchaseButton;
        [SerializeField] private Button greenSkinUseButton;
        [SerializeField] private TextMeshProUGUI greenSkinPurchaseButtonText;
        [SerializeField] private TextMeshProUGUI greenSkinUseButtonText;
        
        [Header("Blue Collectable Skin")]
        [SerializeField] private Button blueSkinPurchaseButton;
        [SerializeField] private Button blueSkinUseButton;
        [SerializeField] private TextMeshProUGUI blueSkinPurchaseButtonText;
        [SerializeField] private TextMeshProUGUI blueSkinUseButtonText;
        
        [Header("Orange Collectable Skin")]
        [SerializeField] private Button orangeSkinPurchaseButton;
        [SerializeField] private Button orangeSkinUseButton;
        [SerializeField] private TextMeshProUGUI orangeSkinPurchaseButtonText;
        [SerializeField] private TextMeshProUGUI orangeSkinUseButtonText;
        
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
                    redSkinUseButtonText.text = "In Use";
                    redSkinUseButton.interactable = false;
                    break;
                case "GreenSkin":
                    greenSkinUseButtonText.text = "In Use";
                    greenSkinUseButton.interactable = false;
                    break;
            }

            switch (_gameManager.collectableCubeSkin.name)
            {
                case "BlueSkin":
                    blueSkinUseButtonText.text = "In Use";
                    blueSkinUseButton.interactable = false;
                    break;
                case "OrangeSkin":
                    orangeSkinUseButtonText.text = "In Use";
                    orangeSkinUseButton.interactable = false;
                    break;
            }
            
            if (_gameManager.isPurchasedRedObstacleSkin)
            {
                redSkinPurchaseButton.interactable = false;
                redSkinPurchaseButtonText.text = "Owned";
            }
            
            if (_gameManager.isPurchasedGreenObstacleSkin)
            {
                greenSkinPurchaseButton.interactable = false;
                greenSkinPurchaseButtonText.text = "Owned";
            }
            
            if (_gameManager.isPurchasedBlueCollectableCubeSkin)
            {
                blueSkinPurchaseButton.interactable = false;
                blueSkinPurchaseButtonText.text = "Owned";
            }
            
            if (_gameManager.isPurchasedOrangeCollectableCubeSkin)
            {
                orangeSkinPurchaseButton.interactable = false;
                orangeSkinPurchaseButtonText.text = "Owned";
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
        
        public void PurchaseBlueSkin()
        {
            if (_gameManager.totalGold >= 250 && !_gameManager.isPurchasedBlueCollectableCubeSkin)
            {
                _gameManager.collectableCubeSkin = Resources.Load("CollectableCube/BlueSkin") as CollectableCubeSkin;
                _gameManager.totalGold -= 250;
                blueSkinPurchaseButton.interactable = false;
                blueSkinPurchaseButtonText.text = "Owned";
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
                orangeSkinPurchaseButton.interactable = false;
                orangeSkinPurchaseButtonText.text = "Owned";
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
        
        public void UseBlueSkin()
        {
            if (_gameManager.isPurchasedBlueCollectableCubeSkin && _gameManager.collectableCubeSkin.name != "BlueSkin")
            {
                _gameManager.collectableCubeSkin = Resources.Load("CollectableCube/BlueSkin") as CollectableCubeSkin;
                blueSkinUseButtonText.text = "In Use";
                blueSkinUseButton.interactable = false;
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
                orangeSkinUseButtonText.text = "In Use";
                orangeSkinUseButton.interactable = false;
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
