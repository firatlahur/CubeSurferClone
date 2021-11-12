using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Obstacle
{
    [CreateAssetMenu(fileName = "ObstacleSkin", menuName = "Scriptable Objects/Obstacle Skin")]
    public class ObstacleSkin : ScriptableObject
    {
        public List<GameObject> obstacleSkinList;
    }
}
