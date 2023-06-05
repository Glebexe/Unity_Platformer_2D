using UnityEngine;

public class EnemyMovement : HorizontalMovement
{
    private int direction;
    [SerializeField] private float leftTurn;
    [SerializeField] private float rightTurn;
    void Start()
    {
        direction = -1;
    }

    void Update()
    {
        if(rb.position.x < leftTurn)
            direction = 1;
        else if(rb.position.x > rightTurn)
            direction = -1;

        MoveHorizontaly(direction);
    }
}
