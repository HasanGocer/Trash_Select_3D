using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackSystem : MonoSingleton<StackSystem>
{
    //Playerde bulunacak

    public float stackDistance;
    [SerializeField] private float _stackMoveTime, _dropMoveTime;
    [SerializeField] private int _stackMaximumCount;
    [SerializeField] private GameObject _stackParent;
    [SerializeField] private GameObject _stackPos;

    public List<GameObject> Objects = new List<GameObject>();
    public List<int> ObjectsCount = new List<int>();
    public List<bool> ObjectsBool = new List<bool>();

    private void Start()
    {
        GameManager.Instance.dropTransfer = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Input"))
        {
            if (Objects.Count < _stackMaximumCount)
                StartCoroutine(StackAdd(other.gameObject, false));
        }
        else if (other.CompareTag("DirtyInput"))
        {
            if (Objects.Count < _stackMaximumCount)
                StartCoroutine(StackAdd(other.gameObject, true));
        }
    }

    IEnumerator StackAdd(GameObject other, bool isDirty)
    {
        Vector3 pos = new Vector3(_stackPos.transform.position.x, _stackPos.transform.position.y + stackDistance * Objects.Count, _stackPos.transform.position.z);
        other.transform.transform.DOLocalMove(pos, _stackMoveTime);
        other.GetComponent<ObjectTouchPlane>().StackÝnPlayer(!isDirty);
        Objects.Add(other.gameObject);
        ObjectsCount.Add(other.GetComponent<ObjectTouchPlane>().objectCount);
        ObjectsBool.Add(isDirty);
        yield return new WaitForSeconds(_stackMoveTime);
        pos = new Vector3(_stackPos.transform.position.x, _stackPos.transform.position.y + stackDistance * (Objects.Count - 1), _stackPos.transform.position.z);
        other.transform.position = pos;
        other.transform.SetParent(_stackParent.transform);
    }

    public IEnumerator DirtyThrashDropObject(GameObject dropParent, Vector3 dropPos)
    {
        int misObject = 0;
        for (int i1 = 0; (i1 < ObjectsCount.Count); i1++)
        {
            GameObject obj = Objects[i1];
            if (!Objects[i1].GetComponent<ObjectTouchPlane>().isClear)
            {
                misObject++;
                Vector3 pos = dropPos;
                obj.transform.DOMove(pos, _dropMoveTime);
                yield return new WaitForSeconds(_dropMoveTime);
                obj.transform.SetParent(dropParent.transform);
                DirtyManager.Instance.ListPlacement(ObjectsCount[i1]);
                Objects.RemoveAt(i1);
                ObjectsCount.RemoveAt(i1);
                ObjectsBool.RemoveAt(i1);
            }
            else
                StartCoroutine(ObjectDistancePlacement(obj, misObject, stackDistance));
        }
        yield return null;
    }

    public IEnumerator StackDrop(WaitSystem waitSystem, GameObject dropParent, Vector3 dropPos, int contractCount)
    {
        for (int i1 = 0; (i1 < ObjectsCount.Count && ContractSystem.Instance.FocusContract[contractCount].ContractBool); i1++)
        {
            for (int i = 0; (i < waitSystem.placeCount.Length && Objects[i].GetComponent<ObjectTouchPlane>().isClear); i++)
            {
                int misObject = 0;
                GameObject obj = Objects[i1];
                if (waitSystem.placeCount[i] == ObjectsCount[i1])
                {
                    misObject++;
                    ContractSystem.Instance.ContractDownÝtem(contractCount, ObjectsCount[i1], i1, true);
                    Vector3 pos = new Vector3(dropPos.x, dropPos.y, dropPos.z);
                    obj.transform.DOMove(pos, _dropMoveTime);
                    yield return new WaitForSeconds(_dropMoveTime);
                    obj.transform.SetParent(dropParent.transform);

                    //bunu contract bitince yapýcaz kamyon gidince
                    /*obj.GetComponent<ObjectTouchPlane>().AddedObjectPool(waitSystem.placeCount[i]);
                    RocketManager.Instance.AddedObjectPool(obj);*/
                }
                else
                    StartCoroutine(ObjectDistancePlacement(obj, misObject, stackDistance));
            }
            yield return null;
        }
    }

    public IEnumerator ObjectDistancePlacement(GameObject obj, int distanceCount, float distance, int objectPlacementTime = 1)
    {
        Vector3 pos = new Vector3(obj.transform.position.x, obj.transform.position.y - distance * distanceCount, obj.transform.position.z);
        obj.transform.DOLocalMove(pos, objectPlacementTime);
        yield return new WaitForSeconds(objectPlacementTime);
    }
}
