using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTouchPlane : MonoBehaviour
{
    //objede bulunacak

    public int objectCount;
    [SerializeField] private GameObject _objectOfCircle;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private BoxCollider _boxCollider;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _objectOfCircle.SetActive(true);
            _boxCollider.enabled = true;
        }
    }

    public void Stack›nPlayer()
    {
        _objectOfCircle.SetActive(false);
        _boxCollider.enabled = false;
    }

    public void AddedObjectPool(int count)
    {
        transform.GetChild(count).gameObject.SetActive(false);
        _boxCollider.enabled = true;
    }
}
