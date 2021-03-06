using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using ScriptableObjects.CollectableCube;
using ScriptableObjects.Obstacle;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class GameManager : GenericSingleton<GameManager>
    {
        [HideInInspector] public bool isGameStarted;
        [HideInInspector] public bool isPurchasedRedObstacleSkin;
        [HideInInspector] public bool isPurchasedGreenObstacleSkin;
        [HideInInspector] public bool isPurchasedBlueCollectableCubeSkin;
        [HideInInspector] public bool isPurchasedOrangeCollectableCubeSkin;
        
        public int currentLevel, totalGold;
        public ObstacleSkin obstacleSkin;
        public CollectableCubeSkin collectableCubeSkin;
        
        [HideInInspector] public string json;
        
        private void Start()
        {
            if (File.Exists(Application.dataPath + "/GameDetails.json"))
            {
                json = File.ReadAllText(Application.dataPath + "/GameDetails.json");
                JsonUtility.FromJsonOverwrite(Decompress(json), this);
            }
        }

        public void SaveProgress()
        {
            isGameStarted = false;
            json = JsonUtility.ToJson(this);
            string compress = Compress(json);
            File.WriteAllText(Application.dataPath + "/GameDetails.json", compress);
        }

        private void OnApplicationQuit()
        {
            SaveProgress();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if(pauseStatus)
                SaveProgress();
        }
        
        public static string Compress(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string Decompress(string s)
        {
            var bytes = Convert.FromBase64String(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }
    }
}
