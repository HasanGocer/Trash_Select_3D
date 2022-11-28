using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackSystem : MonoSingleton<StackSystem>
{
    //Playerde bulunacak

    public float stackDistance;
    [SerializeField] private float _stackMoveTime, _dropMoveTime;
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
            if (Objects.Count < ItemData.Instance.field.playerStackCount)
                StartCoroutine(StackAdd(other.gameObject, false));
        }
        else if (other.CompareTag("DirtyInput"))
        {
            FirstSpawn firstSpawn = other.GetComponent<FirstSpawn>();
            for (int i = 0; i < firstSpawn.ObjectsBool.Count; i++)
            {
                if (Objects.Count < ItemData.Instance.field.playerStackCount && firstSpawn.ObjectsBool[i])
                {
                    StartCoroutine(StackAdd(firstSpawn.Objects[i], true));
                    firstSpawn.ObjectsBool[i] = false;
                }
            }
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
        int misObject = 0, limit = ObjectsCount.Count;
        for (int i1 = limit - 1; i1 >= 0; i1--)
        {
            GameObject obj = Objects[i1];
            if (!Objects[i1].GetComponent<ObjectTouchPlane>().isClear)
            {
                misObject++;
                Vector3 pos = dropPos;
                obj.transform.DOMove(pos, _dropMoveTime);
                yield return new WaitForSecondsRealtime(_dropMoveTime);
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
        int limit = ObjectsCount.Count;
        for (int i1 = limit - 1; (i1 >= 0 && ContractSystem.Instance.FocusContract.Contracts[contractCount].contractBool); i1--)
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
                    yield return new WaitForSecondsRealtime(_dropMoveTime);
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
