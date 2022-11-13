using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirstSpawn : MonoBehaviour
{
    [SerializeField] private float _spawnDistance;
    [SerializeField] private int _objectCount, _objectTransferTime, _OPDirtyThrashCount;

    [SerializeField] private GameObject _spawnPosition;

    public List<GameObject> Objects = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(ItemSpawn());
    }

    IEnumerator ItemSpawn()
    {
        if (GameManager.Instance.openContract)
        {
            while (true)
            {
                if (_objectCount > Objects.Count)
                {
                    //belki animasyon
                    GameObject obj = ObjectPool.Instance.GetPooledObject(_OPDirtyThrashCount);
                    Vector3 pos = new Vector3(_spawnPosition.transform.position.x,
                        _spawnPosition.transform.position.y + (Objects.Count * _spawnDistance),
                        _spawnPosition.transform.position.z);
                    obj.transform.DOMove(pos, _objectTransferTime);
                    Objects.Add(obj);
                    yield return new WaitForSeconds(_objectTransferTime);
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}
