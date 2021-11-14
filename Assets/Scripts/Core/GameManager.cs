﻿using System;
using ScriptableObjects.CollectableCube;
using ScriptableObjects.Obstacle;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class GameManager : GenericSingleton<GameManager>
    {
        public bool isGameStarted;
        
        public int currentLevel, totalGold;
        public ObstacleSkin obstacleSkin;
        public CollectableCubeSkin collectableCubeSkin;
        
    }
}
