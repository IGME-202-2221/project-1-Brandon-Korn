using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    Vector2 fireRateRange;

    float fireRate;

    float timeSinceLastFire = 0;

    bool drifting = false;

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

        fireRate = Random.Range(fireRateRange.x, fireRateRange.y);
    }

    // Update is called once per frame
    void Update()
    {

        velocity = direction * speed * Time.deltaTime;

        timeSinceLastFire += Time.deltaTime;

        enemyPos += velocity;

        if (enemyPos.x <= width / 2 && 
            direction == Vector3.left)
        {
            direction = Vector3.right;
            speed = 0.5f;
            drifting = true;
        }
        else if (enemyPos.x >=  6 * width / 10 &&
            direction == Vector3.right)
        {
            direction = Vector3.left;
            drifting = true;
        }

        transform.position = enemyPos;
    }

    public bool fireBullet()
    {
        if (drifting)
        {
            if (timeSinceLastFire > fireRate)
            {
                timeSinceLastFire = 0;
                fireRate = Random.Range(fireRateRange.x, fireRateRange.y);
                return true;
            }
        }
        return false;
    }

    
}
