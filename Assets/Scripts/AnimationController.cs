using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement player;
    private Rigidbody2D rb;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        player = gameObject.GetComponent<PlayerMovement>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(!PauseMenu.IsGamePaused)
        {
            if((Input.GetKey("d") || Input.GetKey("a")) && player.IsGrounded()){
                animator.SetTrigger("walkAnim");
            }
            else if(rb.velocity.y > 0.1)
                animator.SetTrigger("jumpAnim");
            else if(rb.velocity.y < -0.1){
                animator.ResetTrigger("jumpAnim");
                animator.ResetTrigger("walkAnim");
                animator.ResetTrigger("idleAnim");
                animator.SetTrigger("fallAnim");
            }
            else{
                animator.ResetTrigger("jumpAnim");
                animator.ResetTrigger("fallAnim");
                animator.ResetTrigger("walkAnim");
                animator.SetTrigger("idleAnim");
            }
        }
    }
}
