using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(menuName = "Config/ChunkListConfig")]
    public class ChunkListConfig : ScriptableObject
    {
        public List<GameObject> chunkList = new List<GameObject>();
    }