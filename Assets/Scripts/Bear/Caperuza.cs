using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caperuza : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [Header("movement")]

    private float horizontalMovement = 0f;

    private float verticalMovement = 0f;

    [SerializeField] private float speedMovement;

    [SerializeField] private float smoothedMovement;


    private Vector3 speed = Vector3.zero;
    private bool lookRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * speedMovement;
        verticalMovement = Input.GetAxisRaw("Vertical") * speedMovement;
    }

    private void FixedUpdate() {
        Move(horizontalMovement * Time.fixedDeltaTime );
        MoveVertical(verticalMovement * Time.fixedDeltaTime );
    }

    private void Move(float move){
        Vector3 targetSpeed = new Vector2(move, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetSpeed, ref speed, smoothedMovement);

        if (move > 0 && !lookRight)
        {
            Rotate();
        }
        else if (move < 0 && lookRight)
        {
            Rotate();
        }
    }

    private void MoveVertical(float move)
    {
        Vector3 targetSpeed = new Vector2(rb2D.velocity.x, move);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetSpeed, ref speed, smoothedMovement);        
    }


    private void Rotate()
    {
        lookRight = !lookRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
