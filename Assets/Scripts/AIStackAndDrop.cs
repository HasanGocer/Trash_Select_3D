using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AIStackAndDrop : MonoBehaviour
{
    [SerializeField] private float _stackMoveTime;
    [SerializeField] private int _AIStackerContractCount;
    [SerializeField] private GameObject _stackParent;
    [SerializeField] private List<GameObject> _stackersStack;
    [SerializeField] private List<int> _stackerStackCount;
    [SerializeField] private int _stackMaxStackCount;
    [SerializeField] private bool backpack›sFull;

    public IEnumerator Walk(GameObject stackOutPlace)
    {
        while (true)
        {
            if (_stackersStack.Count > _stackMaxStackCount)
            {
                backpack›sFull = true;
                StartCoroutine(GoToTheStackOut(stackOutPlace));
            }
            for (int i = 0; i < ContractSystem.Instance.FocusContract[_AIStackerContractCount].itemCount.Count; i++)
            {
                if (ContractSystem.Instance.FocusContract[_AIStackerContractCount].itemCount.ContainsKey(i) && !backpack›sFull)
                {
                    List<GameObject> gameObject›nGame = ObjectManager.Instance.object›nGame[i].gameObject›nGame;
                    int lastStackCount = gameObject›nGame.Count - 1;
                    GameObject lastObjectGO = gameObject›nGame[lastStackCount];
                    Vector3 lastObjectV3 = lastObjectGO.transform.position;

                    transform.DOMove(lastObjectV3, Vector3.Distance(transform.position, lastObjectV3));
                    yield return new WaitForSeconds(Vector3.Distance(transform.position, lastObjectV3));
                    if (lastObjectGO == gameObject›nGame[lastStackCount])
                    {
                        _stackersStack.Add(gameObject›nGame[lastStackCount]);
                        _stackerStackCount.Add(lastObjectGO.GetComponent<ObjectTouchPlane>().objectCount);
                        gameObject›nGame[lastStackCount].transform.SetParent(_stackParent.transform);
                        _stackersStack[lastStackCount].transform.DOLocalMove(new Vector3(_stackParent.transform.position.x, _stackParent.transform.position.y + AIManager.Instance.stackDistance * _stackersStack.Count, _stackParent.transform.position.z), _stackMoveTime);
                        gameObject›nGame.RemoveAt(lastStackCount);
                    }
                }
            }
            yield return null;
        }
    }

    private IEnumerator GoToTheStackOut(GameObject stackOutPlace)
    {
        transform.DOMove(stackOutPlace.transform.position, AIManager.Instance.AIDistanceConstant * Vector3.Distance(stackOutPlace.transform.position, transform.position));
        yield return new WaitForSeconds(AIManager.Instance.AIDistanceConstant * Vector3.Distance(stackOutPlace.transform.position, transform.position));
        StartCoroutine(StackOut(stackOutPlace.transform.position));
    }

    private IEnumerator StackOut(Vector3 lastPos)
    {
        for (int i = 0; i < _stackersStack.Count; i++)
        {
            _stackersStack[i].transform.DOMove(lastPos, _stackMoveTime);
        }
        yield return new WaitForSeconds(_stackMoveTime);
        for (int i = 0; i < _stackersStack.Count; i++)
        {
            _stackersStack[i].GetComponent<ObjectTouchPlane>().AddedObjectPool(_stackerStackCount[i]);
            RocketManager.Instance.AddedObjectPool(_stackersStack[i]);
            _stackersStack.RemoveAt(i);
        }
        backpack›sFull = false;
    }
}
