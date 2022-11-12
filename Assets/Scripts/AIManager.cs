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
    public Stacker›nGame[] stacker›nGame;

    public float AIDistanceConstant;
    public int stackDistance;

    public void StartPlace()
    {
        for (int i1 = 0; i1 < stacker›nGame.Length; i1++)
        {
            for (int i2 = 0; i2 < stacker›nGame[i1].gameObjectStacker.Count; i2++)
            {
                if (ContractSystem.Instance.FocusContract[i1].ContractBool)
                    stacker›nGame[i1].boolStacker[i2] = true;
                //sat˝n al˝ma gˆre bool ayarla
            }
        }
    }

    public void StartAIStacker()
    {
        for (int i1 = 0; i1 < stacker›nGame.Length; i1++)
        {
            for (int i2 = 0; i2 < stacker›nGame[i1].gameObjectStacker.Count; i2++)
            {
                if (stacker›nGame[i1].boolStacker[i2])
                {
                    StartCoroutine(stacker›nGame[i1].gameObjectStacker[i2].GetComponent<AIStackAndDrop>().Walk(stacker›nGame[i1].stackOutPlace));
                }
            }
        }
    }
}
