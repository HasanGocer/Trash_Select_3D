using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoSingleton<AIManager>
{
    [System.Serializable]
    public class Stacker›nGame
    {
        public List<GameObject> gameObjectStacker = new List<GameObject>();
        public List<bool> boolStacker = new List<bool>();
    }
    public Stacker›nGame[] stacker›nGame;

    public float AIDistanceConstant;
    public int stackDistance;
}
