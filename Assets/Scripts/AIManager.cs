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
            ArrayList countint = new ArrayList(ContractSystem.Instance.FocusContract[i1].itemCount.Keys);
            for (int i2 = 0; i2 < ContractSystem.Instance.FocusContract[i1].itemCount.Count; i2++)
            {
                stacker›nGame[i2].stackOutPlace.GetComponent<WaitSystem>().placeCount[i2] = (int)countint[i2];
            }
        }
    }

    public void StartAIStacker()
    {
        for (int i1 = 0; i1 < stacker›nGame.Length; i1++)
        {
            for (int i2 = 0; i2 < stacker›nGame[i1].boolStacker.Count; i2++)
            {
                if (stacker›nGame[i1].boolStacker[i2])
                {
                    StartCoroutine(stacker›nGame[i1].gameObjectStacker[i2].GetComponent<AIStackAndDrop>().Walk(stacker›nGame[i1].stackOutPlace));
                }
            }
        }
    }
}
