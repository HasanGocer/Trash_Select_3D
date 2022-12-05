using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour
{
    public bool isTransfer;
    [SerializeField] Rigidbody rb;

    void Update()
    {
        if (isTransfer)
        {
            PlayerMovement.Instance.Movement(rb, this.gameObject);
        }
    }
}
