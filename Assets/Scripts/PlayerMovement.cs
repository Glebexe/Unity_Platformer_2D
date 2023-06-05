using UnityEngine;

public class PlayerMovement : HorizontalMovement
{
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public bool isRespawing = false;

    void Update()
    {
        if(!isRespawing && Time.timeScale != 0){ 
            MoveHorizontaly(Input.GetAxisRaw("Horizontal"));
        }
        else if(IsGrounded())
            isRespawing = false;
        else
            rb.velocity = new Vector2(0, rb.velocity.y);

        if (Input.GetKey("space") && IsGrounded())
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    }

    public bool IsGrounded() { return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); }
}
