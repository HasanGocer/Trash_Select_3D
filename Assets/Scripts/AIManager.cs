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
        public GameObject stackOutPlace;
    }
    public StackerİnGame[] stackerİnGame;

    public float AIDistanceConstant;
    public int stackDistance;

    public void StartPlace()
    {
        for (int i1 = 0; i1 < stackerİnGame.Length; i1++)
        {
            ArrayList countint = new ArrayList(ContractSystem.Instance.FocusContract[i1].itemCount.Keys);
            for (int i2 = 0; i2 < ContractSystem.Instance.FocusContract[i1].itemCount.Count; i2++)
            {
                stackerİnGame[i2].stackOutPlace.GetComponent<WaitSystem>().placeCount[i2] = (int)countint[i2];
            }
        }
    }

    public void StartAIStacker()
    {
        for (int i1 = 0; i1 < stackerİnGame.Length; i1++)
        {
            for (int i2 = 0; i2 < stackerİnGame[i1].boolStacker.Count; i2++)
            {
                if (stackerİnGame[i1].boolStacker[i2])
                {
                    StartCoroutine(stackerİnGame[i1].gameObjectStacker[i2].GetComponent<AIStackAndDrop>().Walk(stackerİnGame[i1].stackOutPlace));
                }
            }
        }
    }
}
