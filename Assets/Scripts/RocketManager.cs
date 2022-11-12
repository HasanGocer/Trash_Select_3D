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
    public List<int> openObjectTypeCount;

    public IEnumerator RocketStart(float pushTime)
    {
        while (true)
        {
            for (int i = 0; i < ContractSystem.Instance.FocusContract.Length; i++)
            {
                for (int i1 = 0; i1 < openObjectTypeCount.Count; i1++)
                {
                    for (int i2 = 0; i2 < ContractSystem.Instance.FocusContract[i].objectTypeCount.Count; i2++)
                    {
                        if (openObjectTypeCount[i1] == ContractSystem.Instance.FocusContract[i].objectTypeCount[i2])
                        {
                            for (int i3 = 0; i3 < ContractSystem.Instance.FocusContract[i].objectCount[i2] - ObjectManager.Instance.objectÝnGame[openObjectTypeCount[i1]].gameObjectÝnGame.Count; i3++)
                            {
                                if (GameManager.Instance.openContract)
                                {
                                    JumpObject(_rocketPushPos.transform.position, ContractSystem.Instance.FocusContract[i].objectTypeCount[i2], _minVeloCityPower, _maxVeloCityPower);
                                    yield return new WaitForSeconds(pushTime);
                                }
                            }
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
        ObjectManager.Instance.objectÝnGame[openObjectCount].gameObjectÝnGame.Add(obj);
        obj.GetComponent<ObjectTouchPlane>().objectCount = openObjectCount;
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
}
