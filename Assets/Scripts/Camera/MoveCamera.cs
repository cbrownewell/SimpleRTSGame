using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    public InputAction moveActions;

    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    Vector2 movedirection;

    private void OnEnable()
    {
        moveActions.Enable();
    }
    private void OnDisable()
    {
        moveActions.Disable();
    }

    private void Update()
    {
        movedirection = moveActions.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3 (movedirection.x * speed, 0f, movedirection.y * speed);
    }

}
