using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectile;
    private float timeLeft;
    [SerializeField] private float timeBetweenShots;
    void Start()
    {
        timeLeft = timeBetweenShots;
    }

    void Update()
    {
        if(timeLeft <= 0)
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation)
            .GetComponent<Projectile>().Setup(new Vector2(transform.localScale.x,0));

            timeLeft = timeBetweenShots;
        }
        timeLeft -= Time.deltaTime;
    }
}
