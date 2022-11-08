using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoSingleton<AIManager>
{
    [System.Serializable]
    public class StackerİnGame
    {
        public List<GameObject> gameObjectStacker = new List<GameObject>();
        public List<bool> boolStacker = new List<bool>();
    }
    public StackerİnGame[] stackerİnGame;

    public float AIDistanceConstant;
    public int stackDistance;
}
