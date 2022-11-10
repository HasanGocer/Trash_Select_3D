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
    public List<int> openObjectCount;

    public IEnumerator RocketStart(float pushTime)
    {
        while (true)
        {
            if (GameManager.Instance.openContract)
            {
                JumpObject(_rocketPushPos.transform.position, openObjectCount, _minVeloCityPower, _maxVeloCityPower);
                yield return new WaitForSeconds(pushTime);
            }
            yield return null;
        }
    }

    public void JumpObject(Vector3 rocketPushPos, List<int> openObjectCount, int minVeloCityPower, int maxVeloCityPower)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPTrashCount);
        obj.transform.position = rocketPushPos;
        int objectCount = Random.Range(0, openObjectCount.Count);
        Debug.Log(openObjectCount[objectCount]);
        Debug.Log(objectCount);
        ObjectManager.Instance.objectÝnGame[openObjectCount[objectCount]].gameObjectÝnGame.Add(obj);
        obj.GetComponent<ObjectTouchPlane>().objectCount = openObjectCount[objectCount];
        obj.transform.GetChild(openObjectCount[objectCount]).gameObject.SetActive(true);
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
