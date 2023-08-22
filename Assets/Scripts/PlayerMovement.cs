using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const float DefaultUpSpeed = -1f;

    [SerializeField] private CharacterController _controller;
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;

    private Vector3 _velocity;
    private float _upSpeed;
    void Update()
    {
        UpdateTravelling();
        UpdateFalling();
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void UpdateTravelling()
    {
        Vector2 inputVector2 = _moveAction.action.ReadValue<Vector2>();
        Vector3 direction = inputVector2.y * transform.forward + inputVector2.x * transform.right;
        _velocity = direction.normalized * _movementSpeed;
        //_controller.Move(_velocity * Time.deltaTime);
    }

    private void UpdateFalling()
    {
        if(_controller.isGrounded)
        {
            _upSpeed = Mathf.Max(_upSpeed, DefaultUpSpeed);
        }
        else
        {
            _upSpeed += Physics.gravity.y * Time.deltaTime;
        }

        _velocity += _upSpeed * Vector3.up;
    }

    public void OnJump()
    {
        Debug.Log("Jummp");
        if(_controller.isGrounded)
        {
            _upSpeed = _jumpForce;
        }
    }
}
