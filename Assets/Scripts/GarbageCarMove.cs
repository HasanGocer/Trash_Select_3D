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
        print(1);
        transform.DOMove(_garbageCarLastTarget.transform.position, _garbageCarMoveTime).SetEase(Ease.InOutSine);
        print(2);
        yield return new WaitForSeconds(_garbageCarMoveTime);
        print(3);
        int limit = clearTrash.Count;
        for (int i = limit - 1; i >= 0; i--)
        {
            print(4);
            clearTrash[i].transform.GetChild(clearTrash[i].GetComponent<ObjectTouchPlane>().objectCount).gameObject.SetActive(false);
            print(5);
            ObjectPool.Instance.AddObject(_OPTrashCount, clearTrash[i]);
            print(6);
            clearTrash.RemoveAt(i);
        }
        print(7);
        transform.DOMove(_garbageCarFirstTarget.transform.position, _garbageCarMoveTime).SetEase(Ease.InOutSine);
        print(8);
        yield return new WaitForSeconds(_garbageCarMoveTime);
        print(9);
    }
    //Son contract bitiþi ve hareket
}
