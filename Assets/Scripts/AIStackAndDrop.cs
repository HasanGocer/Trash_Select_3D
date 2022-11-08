using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AIStackAndDrop : MonoBehaviour
{
    [SerializeField] private float _stackMoveTime;
    [SerializeField] private int _AIStackerCount;
    [SerializeField] private GameObject _stackParent;
    [SerializeField] private List<GameObject> _stackersStack;

    public IEnumerator walk()
    {
        while (true)
        {
            if (ObjectManager.Instance.object›nGame[_AIStackerCount].gameObject›nGame.Count > 0)
            {
                List<GameObject> gameObject›nGame = ObjectManager.Instance.object›nGame[_AIStackerCount].gameObject›nGame;
                int lastStackCount = gameObject›nGame.Count - 1;
                GameObject lastObjectGO = gameObject›nGame[lastStackCount];
                Vector3 lastObjectV3 = lastObjectGO.transform.position;

                transform.DOMove(lastObjectV3, Vector3.Distance(transform.position, lastObjectV3));
                yield return new WaitForSeconds(Vector3.Distance(transform.position, lastObjectV3));
                if (lastObjectGO == gameObject›nGame[lastStackCount])
                {
                    _stackersStack.Add(gameObject›nGame[lastStackCount]);
                    gameObject›nGame[lastStackCount].transform.SetParent(_stackParent.transform);
                    _stackersStack[lastStackCount].transform.DOLocalMove(new Vector3(_stackParent.transform.position.x, _stackParent.transform.position.y + AIManager.Instance.stackDistance * _stackersStack.Count, _stackParent.transform.position.z), _stackMoveTime);
                    gameObject›nGame.RemoveAt(lastStackCount);

                }
            }
            yield return null;
        }
    }
}
