using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitSystem : MonoBehaviour
{
    //place de bulunacak

    public bool isClear;
    public int contractCount;
    public int[] placeCount;
    public bool inPlace;
    [SerializeField] private float _timerSpeed;
    [SerializeField] private Image _barImage;
    public GameObject objectPos, AIWaitPlace;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPlace = true;
            StartCoroutine(TempBar(isClear, _barImage, _timerSpeed));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inPlace = false;
    }

    IEnumerator TempBar(bool isClear, Image barImage, float timerSpeed)
    {
        float timer = 0;
        while (inPlace)
        {
            timer += Time.deltaTime * timerSpeed;
            barImage.fillAmount = Mathf.Lerp(1, 0, timer);
            yield return new WaitForEndOfFrame();

            if (barImage.fillAmount == 0)
            {
                if (isClear)
                    StartCoroutine(StackSystem.Instance.TemplateStackDrop(this, objectPos, objectPos.transform.position, contractCount, StackSystem.Instance.dropMoveTime, StackSystem.Instance.stackDistance, StackSystem.Instance.ObjectsCount, StackSystem.Instance.Objects));
                else
                    StartCoroutine(StackSystem.Instance.TemplateDirtyTrashDrop(this, objectPos, StackSystem.Instance.stackDistance, objectPos.transform.position, StackSystem.Instance.dropMoveTime, StackSystem.Instance.Objects, StackSystem.Instance.ObjectsCount, StackSystem.Instance.ObjectsBool));

                barImage.fillAmount = 1;
                break;
            }
        }
        barImage.fillAmount = 1;
        yield return null;
    }

    //kullanýlmýyor
    /*public IEnumerator bar(bool isClear)
    {
        float timer = 0;

        while (inPlace)
        {
            timer += Time.deltaTime * _timerSpeed;
            _barImage.fillAmount = Mathf.Lerp(1, 0, timer);
            yield return new WaitForEndOfFrame();
            if (_barImage.fillAmount == 0)
            {
                inPlace = false;
                if (isClear)
                    StartCoroutine(StackSystem.Instance.TemplateStackDrop(this, objectPos, objectPos.transform.position, contractCount, StackSystem.Instance.dropMoveTime, StackSystem.Instance.stackDistance, StackSystem.Instance.ObjectsCount, StackSystem.Instance.Objects));
                else
                    StartCoroutine(StackSystem.Instance.TemplateDirtyTrashDrop(this,objectPos, StackSystem.Instance.stackDistance, objectPos.transform.position, StackSystem.Instance.dropMoveTime, StackSystem.Instance.Objects, StackSystem.Instance.ObjectsCount, StackSystem.Instance.ObjectsBool));

                _barImage.fillAmount = 1;
                break;
            }
        }
        _barImage.fillAmount = 1;
    }*/
}
