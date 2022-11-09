using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTouchPlane : MonoBehaviour
{
    //objede bulunacak

    public int objectCount;
    [SerializeField] private GameObject _objectOfCircle;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private CapsuleCollider _capsuleCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            _rb.velocity = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _objectOfCircle.SetActive(true);
            _capsuleCollider.enabled = true;
        }
    }

    public void Stack›nPlayer()
    {
        _objectOfCircle.SetActive(false);
        _capsuleCollider.enabled = false;
    }

    public void AddedObjectPool(int count)
    {
        transform.GetChild(count).gameObject.SetActive(false);
        _capsuleCollider.enabled = true;
    }
}
