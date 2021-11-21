using System.Collections;
using Core;
using UnityEngine;

namespace Player
{
    public class PlayerAdjustSkin : MonoBehaviour
    {
        public GameObject veryFirstCollectedCube;
        
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            veryFirstCollectedCube.GetComponent<Renderer>().material = _gameManager.collectableCubeSkin
                .collectableCubeSkin[0].GetComponent<Renderer>().sharedMaterial;
        }
    }
}
