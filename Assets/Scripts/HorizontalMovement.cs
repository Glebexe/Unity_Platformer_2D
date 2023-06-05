using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    private bool flagFlip;
    [SerializeField] protected float speed = 8f;
    public Rigidbody2D rb;
    public Vector2 externalSpeed;   
    public bool isWalking;

    void Start(){
        flagFlip = true;
        externalSpeed = new Vector2(0,0);
        isWalking = false;
    }

    protected void MoveHorizontaly(float input)
    {
        if((input < 0 && flagFlip) || (input > 0 && !flagFlip))
        {
            flagFlip = !flagFlip;
            Flip();
        }
        rb.velocity = new Vector2(input * speed + externalSpeed.x, rb.velocity.y + externalSpeed.y);
        isWalking = input != 0;
    }

    protected void Flip()
    {
        if(!PauseMenu.IsGamePaused)
            gameObject.transform.localScale = new Vector3(
                -gameObject.transform.localScale.x,gameObject.transform.localScale.y,gameObject.transform.localScale.z);
    }
}
