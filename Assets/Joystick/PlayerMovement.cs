using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Animator animator;

    [SerializeField] private float movementSpeed;

    private void Update()
    {
        Movement(rigidbody, this.gameObject);
    }

    public void Movement(Rigidbody rigidbody, GameObject obj)
    {
        if (!GameManager.Instance.inTransfer)
        {
            rigidbody.velocity = new Vector3(joystick.Horizontal * movementSpeed, rigidbody.velocity.y, joystick.Vertical * movementSpeed);
            if (joystick.Horizontal != 0 || joystick.Vertical != 0 && !GameManager.Instance.inTransfer)
            {
                obj.transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
            }
        }

    }
}
