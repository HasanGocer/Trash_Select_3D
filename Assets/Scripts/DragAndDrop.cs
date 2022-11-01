using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 distence;

    private void OnMouseDown()
    {
        distence = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - distence);
    }
}
