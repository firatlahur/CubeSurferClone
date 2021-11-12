using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.CollectableCube
{
    [CreateAssetMenu(fileName = "ObstacleSkin", menuName = "Scriptable Objects/Collectable Cube Skin")]
    public class CollectableCubeSkin : ScriptableObject
    {
        public List<GameObject> collectableCubeSkin;

    }
}
