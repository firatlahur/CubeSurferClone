using System;
using ScriptableObjects.CollectableCube;
using ScriptableObjects.Obstacle;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class GameManager : GenericSingleton<GameManager>
    {
        [HideInInspector]public bool isGameStarted;
        
        public int currentLevel, totalPurpleScore;
        public ObstacleSkin obstacleSkin;
        public CollectableCubeSkin collectableCubeSkin;
        
    }
}
