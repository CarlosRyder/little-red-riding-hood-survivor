using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private float speed = 1f;
    private new Rigidbody2D rigidbody2D;
    private Vector2 moveInput;
    private Animator animator;
    #endregion

    #region Start
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();   
    }
    #endregion

    #region Update
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;
        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);
    }
    #endregion

    #region FixedUpdate
    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(rigidbody2D.position +  moveInput * speed * Time.fixedDeltaTime);
    }
    #endregion
}
