using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask sawsLayer;
    private Vector2 direction;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if(Physics2D.OverlapCircle(transform.position, 0.2f, groundLayer) 
        || Physics2D.OverlapCircle(transform.position, 0.2f, sawsLayer)
        || transform.position.x > 80)
            Destroy(gameObject);
    }

    public void Setup(Vector2 direction) { this.direction = direction; }
}
