using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class GameManager : GenericSingleton<GameManager>
    {
        [HideInInspector]public bool isGameStarted;
        
        public int currentLevel, totalPurpleScore;
    }
}
