using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketManager : MonoSingleton<RocketManager>
{
    //managerde bulunacak
    //fazla atmasýn ayarla

    [SerializeField] private GameObject _rocketPushPos;
    [SerializeField] private int _minVeloCityPower, _maxVeloCityPower;
    [SerializeField] private int _OPTrashCount;
    [SerializeField] private float _pushTime;
    public List<int> openObjectTypeCount = new List<int>();
    public List<int> openObjectCount = new List<int>();
    public List<bool> openObjectTypeBool = new List<bool>();

    public IEnumerator RocketStart()
    {
        while (true)
        {
            if (GameManager.Instance.inStart)
                for (int i = 0; i < ContractSystem.Instance.FocusContract.Contracts.Count; i++)
                {
                    for (int i1 = 0; i1 < openObjectTypeCount.Count; i1++)
                    {
                        for (int i2 = 0; i2 < ContractSystem.Instance.FocusContract.Contracts[i].objectTypeCount.Count; i2++)
                        {
                            if (openObjectTypeCount[i1] == ContractSystem.Instance.FocusContract.Contracts[i].objectTypeCount[i2] && openObjectTypeBool[i1])
                            {
                                //buraya arkadan gelecek nesile göre atama yapýlacak
                                for (int i3 = 0; i3 < openObjectCount[i1]; i3++)
                                {
                                    JumpObject(_rocketPushPos.transform.position, ContractSystem.Instance.FocusContract.Contracts[i].objectTypeCount[i2], _minVeloCityPower, _maxVeloCityPower);
                                    yield return new WaitForSeconds(_pushTime);
                                }
                                openObjectTypeBool[i1] = false;
                            }
                        }
                    }
                }
            yield return null;
        }
    }

    public void JumpObject(Vector3 rocketPushPos, int openObjectCount, int minVeloCityPower, int maxVeloCityPower)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPTrashCount);
        obj.transform.position = rocketPushPos;
        obj.GetComponent<ObjectTouchPlane>().objectCount = openObjectCount;
        obj.GetComponent<ObjectTouchPlane>().isClear = true;
        obj.transform.GetChild(openObjectCount).gameObject.SetActive(true);
        int velocityPowerX = Random.Range(minVeloCityPower, maxVeloCityPower);
        int velocityPowerY = Random.Range(minVeloCityPower, maxVeloCityPower);
        int velocityPowerZ = Random.Range(minVeloCityPower, maxVeloCityPower);
        obj.GetComponent<Rigidbody>().velocity = new Vector3(velocityPowerX, velocityPowerY, velocityPowerZ);
    }

    public void AddedObjectPool(GameObject obj)
    {
        ObjectPool.Instance.AddObject(_OPTrashCount, obj);
    }

    public void DeleteListsAll()
    {
        for (int i = 0; i < openObjectTypeCount.Count; i++)
        {
            openObjectTypeCount.RemoveAt(i);
            openObjectCount.RemoveAt(i);
            openObjectTypeBool.RemoveAt(i);
        }
    }
}
