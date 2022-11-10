using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketManager : MonoSingleton<RocketManager>
{
    //managerde bulunacak
    //fazla atmasýn ayarla

    [SerializeField] private GameObject _rocketPushPos;
    [SerializeField] private int _minVeloCityPower, _maxVeloCityPower;
    [SerializeField] private int _minAngleLimit, _maxAngleLimit, angleZCordinate = 30;
    [SerializeField] private int _OPTrashCount;
    public List<int> openObjectCount;

    public IEnumerator RocketStart(float pushTime)
    {
        while (true)
        {
            if (GameManager.Instance.openContract)
            {
                JumpObject(_rocketPushPos.transform.position, openObjectCount, _minVeloCityPower, _maxVeloCityPower, _minAngleLimit, _maxAngleLimit, angleZCordinate);
                yield return new WaitForSeconds(pushTime);
            }
            yield return null;
        }
    }

    public void JumpObject(Vector3 rocketPushPos, List<int> openObjectCount, int minVeloCityPower, int maxVeloCityPower, int minAngleLimit, int maxAngleLimit, int angleZCordinate = 30)
    {
        GameObject obj = ObjectPool.Instance.GetPooledObject(_OPTrashCount);
        obj.transform.position = rocketPushPos;
        int angleLimitWithRandom = Random.Range(minAngleLimit, maxAngleLimit);
        int objectCount = Random.Range(0, openObjectCount.Count);
        ObjectManager.Instance.objectÝnGame[openObjectCount[objectCount]].gameObjectÝnGame.Add(obj);
        obj.GetComponent<ObjectTouchPlane>().objectCount = openObjectCount[objectCount];
        obj.transform.GetChild(openObjectCount[objectCount]).gameObject.SetActive(true);
        obj.transform.rotation = Quaternion.Euler(0, angleLimitWithRandom, angleZCordinate);
        int velocityPower = Random.Range(minVeloCityPower, maxVeloCityPower);
        obj.GetComponent<Rigidbody>().velocity = new Vector3(0, velocityPower, 0);
    }

    public void AddedObjectPool(GameObject obj)
    {
        ObjectPool.Instance.AddObject(_OPTrashCount, obj);
    }
}
