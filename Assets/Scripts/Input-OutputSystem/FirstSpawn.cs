using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirstSpawn : MonoBehaviour
{
    [SerializeField] private int _dirtyThrashItemID;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private int _objectTransferTime, _OPDirtyThrashCount;

    [SerializeField] private GameObject _spawnPosition;

    public List<GameObject> Objects = new List<GameObject>();

    public IEnumerator ItemSpawn()
    {
        while (true)
        {
            if (GameManager.Instance.openContract)
                for (int i1 = 0; i1 < RocketManager.Instance.openObjectTypeCount.Count; i1++)
                {
                    if (_dirtyThrashItemID == RocketManager.Instance.openObjectTypeCount[i1] && Objects.Count < RocketManager.Instance.openObjectCount[i1])
                    {
                        for (int i2 = 0; i2 < RocketManager.Instance.openObjectCount[i1]; i2++)
                        {
                            print(1);
                            //belki animasyon
                            GameObject obj = ObjectPool.Instance.GetPooledObject(_OPDirtyThrashCount);
                            obj.transform.position = transform.position;
                            Vector3 pos = new Vector3(_spawnPosition.transform.position.x,
                                                    _spawnPosition.transform.position.y + (Objects.Count * _spawnDistance),
                                                    _spawnPosition.transform.position.z);
                            obj.transform.GetChild(_dirtyThrashItemID).gameObject.SetActive(true);
                            obj.transform.DOMove(pos, _objectTransferTime);
                            Objects.Add(obj);
                            yield return new WaitForSeconds(_objectTransferTime);
                            obj.GetComponent<ObjectTouchPlane>().DirtyThrashFirstSpawn();
                        }
                    }
                }
            yield return null;
        }
    }
}
