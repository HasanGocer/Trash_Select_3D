using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyManager : MonoSingleton<DirtyManager>
{
    public List<int> openObjectTypeCount = new List<int>();
    public List<int> openObjectCount = new List<int>();

    public void NewDirtyListPlacement()
    {
        for (int i = 0; i < RocketManager.Instance.openObjectTypeCount.Count; i++)
        {
            openObjectTypeCount.Add(RocketManager.Instance.openObjectTypeCount[i]);
            openObjectCount.Add(RocketManager.Instance.openObjectCount[i]);
        }
    }

    public void ListPlacement(int openObjectTypeCount)
    {
        for (int i = 0; i < this.openObjectTypeCount.Count; i++)
        {
            if (openObjectTypeCount == this.openObjectTypeCount[i])
            {
                this.openObjectCount[i]--;
                if (this.openObjectCount[i] == 0)
                {
                    for (int i1 = 0; i1 < RocketManager.Instance.openObjectTypeCount.Count; i1++)
                    {
                        if (RocketManager.Instance.openObjectTypeCount[i1] == this.openObjectTypeCount[i])
                            RocketManager.Instance.openObjectTypeBool[i1] = true;
                    }
                }
            }
        }
    }
}
