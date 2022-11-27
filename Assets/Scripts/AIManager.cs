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
        public GameObject stackOutPlace;
    }
    public Stacker›nGame[] stackerInGame;

    public float AIDistanceConstant;
    public int stackDistance;
    public int maxStackerCount;
    public int maxStackerTypeCount;

    public void StartPlace()
    {
        for (int i1 = 0; i1 < ItemData.Instance.field.AICount.Length; i1++)
        {
            print(ItemData.Instance.field.AICount[i1]);
            for (int i2 = 0; i2 < ItemData.Instance.field.AICount[i1]; i2++)
            {
                stackerInGame[i1].boolStacker[i2] = true;
                StartCoroutine(stackerInGame[i1].gameObjectStacker[i2].GetComponent<AIStackAndDrop>().WalkAI(stackerInGame[i1].stackOutPlace));
            }
        }
    }
}
