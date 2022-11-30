using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirstSpawn : MonoBehaviour
{
    public int dirtyThrashItemID;
    [SerializeField] private float _spawnDistance;
    [SerializeField] private int _objectTransferTime, _OPDirtyThrashCount;

    [SerializeField] private GameObject _spawnPosition;

    public List<GameObject> Objects = new List<GameObject>();
    public List<bool> ObjectsBool = new List<bool>();

    public IEnumerator ItemSpawn()
    {
        while (true)
        {
            if (GameManager.Instance.inStart)
                for (int i1 = 0; i1 < RocketManager.Instance.openObjectTypeCount.Count; i1++)
                {
                    if (dirtyThrashItemID == RocketManager.Instance.openObjectTypeCount[i1] && Objects.Count < RocketManager.Instance.openObjectCount[i1])
                    {
                        for (int i2 = 0; i2 < RocketManager.Instance.openObjectCount[i1]; i2++)
                        {
                            //belki animasyon
                            GameObject obj = ObjectPool.Instance.GetPooledObject(_OPDirtyThrashCount);
                            obj.transform.position = transform.position;
                            Vector3 pos = new Vector3(_spawnPosition.transform.position.x,
                                                    _spawnPosition.transform.position.y + (Objects.Count * _spawnDistance),
                                                    _spawnPosition.transform.position.z);
                            obj.transform.GetChild(dirtyThrashItemID).gameObject.SetActive(true);
                            obj.transform.DOMove(pos, _objectTransferTime);
                            Objects.Add(obj);
                            ObjectsBool.Add(true);
                            obj.GetComponent<ObjectTouchPlane>().objectCount = dirtyThrashItemID;
                            yield return new WaitForSeconds(_objectTransferTime);
                            obj.GetComponent<ObjectTouchPlane>().DirtyThrashFirstSpawn();
                        }
                    }
                }
            yield return null;
        }
    }
}
