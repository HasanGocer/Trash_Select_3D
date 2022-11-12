using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AIStackAndDrop : MonoBehaviour
{
    [SerializeField] private float _stackMoveTime;
    [SerializeField] private int _AIStackerContractCount;
    [SerializeField] private GameObject _stackParent;
    [SerializeField] private List<GameObject> _stackersStack = new List<GameObject>();
    [SerializeField] private List<int> _stackerStackCount = new List<int>();
    [SerializeField] private int _stackMaxStackCount;
    [SerializeField] private bool backpack›sFull;

    public IEnumerator Walk(GameObject stackOutPlace)
    {
        while (true)
        {
            if (_stackersStack.Count > _stackMaxStackCount)
            {
                Debug.Log("22");
                backpack›sFull = true;
                StartCoroutine(GoToTheStackOut(stackOutPlace));
                Debug.Log("23");
            }

            if (ContractSystem.Instance.FocusContract[_AIStackerContractCount].ContractBool)
            {
                List<int> contractSystemTypeCountList = ContractSystem.Instance.FocusContract[_AIStackerContractCount].objectTypeCount;
                for (int i1 = 0; i1 < contractSystemTypeCountList.Count; i1++)
                {
                    for (int i = 0; i < ObjectManager.Instance.object›nGame.Count; i++)
                    {
                        if (contractSystemTypeCountList[i1] == i && ObjectManager.Instance.object›nGame[i].gameObject›nGame.Count > 0)
                        {
                            for (int i2 = 0; i2 < ContractSystem.Instance.FocusContract[_AIStackerContractCount].objectCount[i1]; i2++)
                            {
                                if (!backpack›sFull)
                                {
                                    List<GameObject> gameObject›nGame = ObjectManager.Instance.object›nGame[i].gameObject›nGame;
                                    int lastStackCount = gameObject›nGame.Count - 1;
                                    GameObject lastObjectGO = gameObject›nGame[lastStackCount];
                                    Vector3 lastObjectV3 = lastObjectGO.transform.position;

                                    transform.DOMove(lastObjectV3, Vector3.Distance(transform.position, lastObjectV3) * AIManager.Instance.AIDistanceConstant);
                                    yield return new WaitForSeconds(Vector3.Distance(transform.position, lastObjectV3) * AIManager.Instance.AIDistanceConstant);
                                    if (lastObjectGO == gameObject›nGame[lastStackCount] && transform.position == gameObject›nGame[lastStackCount].transform.position)
                                    {
                                        _stackersStack.Add(gameObject›nGame[lastStackCount]);
                                        _stackerStackCount.Add(i);
                                        ContractSystem.Instance.ContractDown›tem(_AIStackerContractCount, i, 0, false);
                                        gameObject›nGame[lastStackCount].transform.SetParent(_stackParent.transform);
                                        _stackersStack[lastStackCount].transform.DOMove(new Vector3(_stackParent.transform.position.x, _stackParent.transform.position.y + AIManager.Instance.stackDistance * _stackersStack.Count, _stackParent.transform.position.z), _stackMoveTime);
                                        yield return new WaitForSeconds(_stackMoveTime);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (!backpack›sFull)
            {
                Debug.Log("141");
                backpack›sFull = true;
                StartCoroutine(GoToTheStackOut(stackOutPlace));
                Debug.Log("142");
            }
            yield return null;
        }
    }

    private IEnumerator GoToTheStackOut(GameObject stackOutPlace)
    {
        Debug.Log("14243");
        transform.DOMove(stackOutPlace.GetComponent<WaitSystem>().AIWaitPlace.transform.position, AIManager.Instance.AIDistanceConstant * Vector3.Distance(stackOutPlace.GetComponent<WaitSystem>().objectPos.transform.position, transform.position));
        yield return new WaitForSeconds(AIManager.Instance.AIDistanceConstant * Vector3.Distance(stackOutPlace.transform.position, transform.position));
        StartCoroutine(StackOut(stackOutPlace));
    }

    private IEnumerator StackOut(GameObject lastPos)
    {
        for (int i = 0; i < _stackersStack.Count; i++)
        {
            _stackersStack[i].transform.DOMove(lastPos.GetComponent<WaitSystem>().objectPos.transform.position, _stackMoveTime);
            _stackersStack.RemoveAt(i);
            _stackerStackCount.RemoveAt(i);
        }
        yield return new WaitForSeconds(_stackMoveTime);

        //contract tamamlan˝nca kendi yapar
        /*_stackersStack[i].GetComponent<ObjectTouchPlane>().AddedObjectPool(_stackerStackCount[i]);
        RocketManager.Instance.AddedObjectPool(_stackersStack[i]);*/

        backpack›sFull = false;
    }

    public bool ObjectControl(int objectCount, List<int> objectTypeCount)
    {
        bool inThere = true;
        for (int i = 0; i < objectTypeCount.Count; i++)
        {
            if (objectTypeCount[i] == objectCount)
                inThere = false;
        }
        return inThere;
    }
}
