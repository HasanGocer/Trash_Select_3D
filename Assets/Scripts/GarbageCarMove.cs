using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GarbageCarMove : MonoBehaviour
{
    public List<GameObject> clearTrash = new List<GameObject>();

    [SerializeField] private float _garbageCarMoveTime;
    [SerializeField] private GameObject _garbageCarFirstTarget;
    [SerializeField] private GameObject _garbageCarLastTarget;
    [SerializeField] private int _OPTrashCount;

    //bakýþ yönü gerekli dikkat 
    public IEnumerator GarbageCarMoveFunc()
    {
        transform.DOMove(_garbageCarLastTarget.transform.position, _garbageCarMoveTime).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(_garbageCarMoveTime);
        int limit = clearTrash.Count;
        for (int i = limit - 1; i >= 0; i--)
        {
            clearTrash[i].transform.GetChild(clearTrash[i].GetComponent<ObjectTouchPlane>().objectCount).gameObject.SetActive(false);
            ObjectPool.Instance.AddObject(_OPTrashCount, clearTrash[i]);
            clearTrash.RemoveAt(i);
        }
        transform.DOMove(_garbageCarFirstTarget.transform.position, _garbageCarMoveTime).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(_garbageCarMoveTime);
    }
    //Son contract bitiþi ve hareket
}
