using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private float speed = 1f;
    [SerializeField] private float raycastDistance = 1f; // Distance for the raycast
    private new Rigidbody2D rigidbody2D;
    private Vector2 moveInput;
    private Animator animator;

    // Variables to store the last movement direction before stopping
    private float lastHorizontal;
    private float lastVertical;
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

        // Update the speed parameter in the animator
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        // If there is movement, update last direction variables
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastHorizontal = moveX;
            lastVertical = moveY;

            // Set animator parameters for current movement
            animator.SetFloat("Horizontal", moveX);
            animator.SetFloat("Vertical", moveY);
        }
        else
        {
            // When speed is zero, store the last direction in the animator
            animator.SetFloat("LastHorizontal", lastHorizontal);
            animator.SetFloat("LastVertical", lastVertical);
        }

        // Perform a Raycast in the direction of the last movement
        Vector2 raycastDirection = new Vector2(lastHorizontal, lastVertical).normalized;
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position, raycastDirection, raycastDistance);

        // Draw the ray in the scene view for debugging purposes
        Debug.DrawRay(rigidbody2D.position, raycastDirection * raycastDistance, Color.red);

        // If the ray hits something
        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
        }
    }
    #endregion

    #region FixedUpdate
    void FixedUpdate()
    {
        // Move the player
        rigidbody2D.MovePosition(rigidbody2D.position + moveInput * speed * Time.fixedDeltaTime);
    }
    #endregion
}
