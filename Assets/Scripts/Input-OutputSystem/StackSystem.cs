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

    private void Start()
    {
        GameManager.Instance.dropTransfer = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Input"))
        {
            if (Objects.Count < _stackMaximumCount)
            {
                StartCoroutine(StackAdd(other.gameObject));
            }
        }
        else if (other.CompareTag("Output"))
        {
            if (!GameManager.Instance.dropTransfer == false)
            {
                StartCoroutine(other.GetComponent<WaitSystem>().bar(this.gameObject));
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {

    }


    IEnumerator StackAdd(GameObject other)
    {
        Vector3 pos = new Vector3(_stackPos.transform.position.x, _stackPos.transform.position.y + stackDistance * Objects.Count, _stackPos.transform.position.z);
        other.transform.transform.DOLocalMove(pos, _stackMoveTime);
        other.GetComponent<ObjectTouchPlane>().Stack›nPlayer();
        Objects.Add(other.gameObject);
        ObjectsCount.Add(other.GetComponent<ObjectTouchPlane>().objectCount);
        yield return new WaitForSeconds(_stackMoveTime);
        other.transform.position = pos;
        other.transform.SetParent(_stackParent.transform);
    }

    public IEnumerator StackDrop(GameObject place, GameObject dropParent, Vector3 dropPos)
    {
        WaitSystem waitSystem = place.GetComponent<WaitSystem>();
        int placeCount = 0, distanceCount = 0;
        while (waitSystem.inPlace && placeCount < Objects.Count && !GameManager.Instance.dropTransfer)
        {
            GameManager.Instance.dropTransfer = true;
            for (int i = 0; i < waitSystem.placeCount.Length; i++)
            {
                GameObject obj = Objects[placeCount];
                if (waitSystem.placeCount[i] == ObjectsCount[placeCount])
                {
                    distanceCount++;
                    ObjectsCount.RemoveAt(placeCount);
                    Objects.RemoveAt(placeCount);
                    obj.transform.SetParent(dropParent.transform);
                    Vector3 pos = new Vector3(dropPos.x, dropPos.y, dropPos.z);
                    obj.transform.DOLocalMove(pos, _dropMoveTime);
                    yield return new WaitForSeconds(_dropMoveTime);
                    obj.GetComponent<ObjectTouchPlane>().AddedObjectPool(waitSystem.placeCount[i]);
                    RocketManager.Instance.AddedObjectPool(obj);
                }
                else
                    StartCoroutine(ObjectDistancePlacement(obj, distanceCount, stackDistance));
            }
            yield return null;

            placeCount++;
            GameManager.Instance.dropTransfer = false;
        }
    }

    public IEnumerator ObjectDistancePlacement(GameObject obj, int distanceCount, float distance, int objectPlacementTime = 1)
    {
        Vector3 pos = new Vector3(obj.transform.position.x, obj.transform.position.y - distance * distanceCount, obj.transform.position.z);
        yield return new WaitForSeconds(objectPlacementTime);
    }
}
