using UnityEngine;

public class PlatformMovementController : MonoBehaviour
{
    [SerializeField] private Vector2 basePosition;
    [SerializeField] private Vector2 direction = new Vector2(0,0);
    [SerializeField] private float movementRange = 1f;
    [SerializeField] private float speed = 1f;
    private GameObject player;
    private Vector3 newPos;

    void Update()
    {
        if((transform.position.x > basePosition.x + movementRange && direction.x == 1) 
        || (transform.position.x < basePosition.x - movementRange && direction.x == -1))
        {
            Flip(-1,1);
        }
        else if((transform.position.y > basePosition.y + movementRange && direction.y == 1)
        || (transform.position.y < basePosition.y - movementRange && direction.y == -1))
        {
            Flip(1,-1);
        }
        else 
        {
            transform.Translate(speed * direction.x * Time.deltaTime, speed * direction.y * Time.deltaTime, 0);
            if(player != null)
                player.GetComponent<PlayerMovement>().externalSpeed = new Vector2(speed * direction.x, 0);
        }
    }

    private void Flip(int x, int y)
    {
        direction = new Vector2(direction.x * x, direction.y * y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            player = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerMovement>().externalSpeed = new Vector2(0,0);
            player = null;
        }
    }
}
