using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackSystem : MonoSingleton<StackSystem>
{
    //Playerde bulunacak

    public float stackDistance, stackMoveTime, dropMoveTime, stackShakeTime, stackShakeStrength;
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
            {
                StackAddPrivateFunc(other.gameObject, false);
                StartCoroutine(TemplateStackAdd(other.gameObject, _stackPos.transform.position, _stackParent, stackMoveTime, other.GetComponent<ObjectTouchPlane>().objectCount, false, Objects, ObjectsCount, ObjectsBool));
            }
        }
        else if (other.CompareTag("DirtyInput"))
        {
            FirstSpawn firstSpawn = other.GetComponent<FirstSpawn>();
            StartCoroutine(InputStackAdd(firstSpawn, _stackPos.transform.position, _stackParent, stackMoveTime, firstSpawn.dirtyThrashItemID, true, Objects, ObjectsCount, ObjectsBool));
        }
    }

    IEnumerator InputStackAdd(FirstSpawn firstSpawn, Vector3 stackPos, GameObject stackParent, float stackMoveTime, int objectCount, bool isDirty, List<GameObject> Objects, List<int> ObjectsCount, List<bool> ObjectsBool)
    {
        for (int i = 0; i < firstSpawn.ObjectsBool.Count; i++)
            if (Objects.Count < ItemData.Instance.field.playerStackCount && firstSpawn.ObjectsBool[i] && firstSpawn.isStay)
            {
                GameManager.Instance.inTransfer = true;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                StackAddPrivateFunc(firstSpawn.Objects[i], true);
                StartCoroutine(TemplateStackAdd(firstSpawn.Objects[i], stackPos, stackParent, stackMoveTime, objectCount, isDirty, Objects, ObjectsCount, ObjectsBool));
                firstSpawn.ObjectsBool[i] = false;
                yield return new WaitForSeconds(stackMoveTime);
                GameManager.Instance.inTransfer = false;
            }
        yield return null;
    }

    IEnumerator TemplateStackAdd(GameObject obj, Vector3 stackPos, GameObject stackParent, float stackMoveTime, int objectCount, bool isDirty, List<GameObject> Objects, List<int> ObjectsCount, List<bool> ObjectsBool)
    {
        GameManager.Instance.inTransfer = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Objects.Add(obj);
        ObjectsCount.Add(objectCount);
        ObjectsBool.Add(isDirty);
        Vector3 pos = new Vector3(stackPos.x, stackPos.y + stackDistance * Objects.Count, stackPos.z);
        obj.GetComponent<BoxMove>().isTransfer = true;
        obj.transform.transform.DOLocalMove(pos, stackMoveTime);
        yield return new WaitForSeconds(stackMoveTime);
        obj.transform.SetParent(stackParent.transform);
        obj.GetComponent<BoxMove>().isTransfer = false;
        StartCoroutine(DoShaker(stackShakeTime, Objects));
        GameManager.Instance.inTransfer = false;
    }

    private void StackAddPrivateFunc(GameObject obj, bool isDirty)
    {
        obj.GetComponent<ObjectTouchPlane>().StackÝnPlayer(!isDirty);
    }
    private IEnumerator DoShaker(float stackShakeTime, List<GameObject> objects)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].transform.DOShakePosition(stackShakeTime, stackShakeStrength);
            objects[i].transform.DOShakeRotation(stackShakeTime, stackShakeStrength);
            objects[i].transform.DOShakeScale(stackShakeTime, stackShakeStrength);
        }
        yield return null;
    }

    public IEnumerator TemplateDirtyTrashDrop(WaitSystem waitSystem, GameObject dropParent, float stackDistance, Vector3 dropPos, float dropMoveTime, List<GameObject> Objects, List<int> ObjectsCount, List<bool> ObjectsBool)
    {
        int misObject = 0, limit = ObjectsCount.Count;
        for (int i1 = limit - 1; i1 >= 0; i1--)
        {
            if (!waitSystem.inPlace)
                break;
            GameObject obj = Objects[i1];
            if (ObjectControlFunc(obj))
            {
                misObject++;
                obj.transform.SetParent(dropParent.transform);
                obj.transform.DOLocalMove(dropPos, dropMoveTime);
                yield return new WaitForSecondsRealtime(dropMoveTime);
                ObjectRemoveControlFunc(ObjectsCount[i1], i1, Objects, ObjectsCount, ObjectsBool);
            }
            else
                StartCoroutine(ObjectDistancePlacement(obj, misObject, stackDistance));
        }
        yield return null;
    }

    private bool ObjectControlFunc(GameObject obj)
    {
        return !obj.GetComponent<ObjectTouchPlane>().isClear;
    }
    private void ObjectRemoveControlFunc(int objCount, int forCount, List<GameObject> Objects, List<int> ObjectsCount, List<bool> ObjectsBool)
    {
        DirtyManager.Instance.ListPlacement(objCount);
        Objects[forCount].GetComponent<ObjectTouchPlane>().DirtyTrashAddedOPFunc();
        Objects.RemoveAt(forCount);
        ObjectsCount.RemoveAt(forCount);
        ObjectsBool.RemoveAt(forCount);

    }

    public IEnumerator TemplateStackDrop(WaitSystem waitSystem, GameObject dropParent, Vector3 dropPos, int contractCount, float dropMoveTime, float stackDistance, List<int> ObjectsCount, List<GameObject> Objects)
    {
        int limit = ObjectsCount.Count, misObject = 0;
        for (int i1 = limit - 1; (i1 >= 0 && StackDropControl(contractCount)); i1--)
            for (int i = 0; (i < waitSystem.placeCount.Length && ObjectControlFunc(Objects[i])); i++)
            {
                if (!waitSystem.inPlace)
                    break;
                GameObject obj = Objects[i1];
                if (waitSystem.placeCount[i] == ObjectsCount[i1])
                {
                    misObject++;
                    ContractSystem.Instance.ContractDownÝtem(contractCount, ObjectsCount[i1], i1, true);
                    Vector3 pos = new Vector3(dropPos.x, dropPos.y, dropPos.z);
                    obj.transform.SetParent(dropParent.transform);
                    obj.transform.DOLocalMove(pos, dropMoveTime);
                    yield return new WaitForSecondsRealtime(dropMoveTime);
                    AddedClearTrash(obj, contractCount);
                }
                else
                    StartCoroutine(ObjectDistancePlacement(obj, misObject, stackDistance));
            }
        yield return null;
    }

    private bool StackDropControl(int contractCount)
    {
        return ContractSystem.Instance.FocusContract.Contracts[contractCount].contractBool;
    }
    private void AddedClearTrash(GameObject obj, int contractCount)
    {
        UpgradeManager.Instance._upgradeItem[GarbageSystem.Instance.garbageCarUSCount]._items[contractCount].GetComponent<GarbageCarMove>().clearTrash.Add(obj);
    }

    public IEnumerator ObjectDistancePlacement(GameObject obj, int distanceCount, float distance, int objectPlacementTime = 1)
    {
        Vector3 pos = new Vector3(obj.transform.position.x, obj.transform.position.y + distance * distanceCount, obj.transform.position.z);
        obj.transform.DOLocalMove(pos, objectPlacementTime);
        yield return new WaitForSeconds(objectPlacementTime);
    }

    //Kullanýlmýyor
    /*
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
    }*/

    /* public IEnumerator DirtyThrashDropObject(GameObject dropParent, Vector3 dropPos)
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
     }*/

    /*public IEnumerator StackDrop(WaitSystem waitSystem, GameObject dropParent, Vector3 dropPos, int contractCount)
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
                    obj.transform.DOMove(pos, dropMoveTime);
                    yield return new WaitForSecondsRealtime(dropMoveTime);
                    obj.transform.SetParent(dropParent.transform);
                    UpgradeManager.Instance._upgradeItem[GarbageSystem.Instance.garbageCarUSCount]._items[contractCount].GetComponent<GarbageCarMove>().clearTrash.Add(obj);
                }
                else
                    StartCoroutine(ObjectDistancePlacement(obj, misObject, stackDistance));
            }
            yield return null;
        }
    }*/

}
