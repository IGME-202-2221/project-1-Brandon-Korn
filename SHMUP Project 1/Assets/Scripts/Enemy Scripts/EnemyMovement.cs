using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    Vector2 fireRateRange;

    [SerializeField]
    float movementChangeTime = 2f;

    float fireRate;

    float timeSinceLastFire = 0;
    float timeDirectionChange = 0;

    public Vector3 direction = Vector3.left;

    Vector3 velocity = Vector3.zero;

    Vector3 enemyPos = Vector3.zero;

    static Camera cam;
    static float height = 0;
    static float width = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyPos = transform.position;
        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;

        timeSinceLastFire = Time.time;

        fireRate = Random.Range(fireRateRange.x, fireRateRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed * Time.deltaTime;

        timeDirectionChange += Time.deltaTime;
        timeSinceLastFire += Time.deltaTime;

        enemyPos += velocity;

        transform.position = enemyPos;

        if (timeDirectionChange > movementChangeTime)
        {
            AlterDirection();
        }
    }

    public bool fireBullet()
    {
        if (timeSinceLastFire > fireRate)
        {
            timeSinceLastFire = 0;
            fireRate = Random.Range(fireRateRange.x, fireRateRange.y);
            return true;
        }
        return false;
    }

    void AlterDirection()
    {
        
        if (direction.y == 0)
        {
            float initialRan = Random.Range(0, 1f);
            if (initialRan > 0.5)
            {
                direction.y = 0.35f;
            }
            else
            {
                direction.y = -0.35f;
            }
        }
        else 
        {
            direction.y = -direction.y;
        }
        direction.Normalize();
        timeDirectionChange = Random.Range(-timeDirectionChange, 0);
    }
}
