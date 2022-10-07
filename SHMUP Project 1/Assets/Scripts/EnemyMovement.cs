using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    Vector2 fireRateRange;

    float fireRate;

    float timeSinceLastFire = 0;

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

        enemyPos += velocity;

        transform.position = enemyPos;
    }

    public bool fireBullet(out Vector3 direct)
    {
        if (Time.time - timeSinceLastFire > fireRate)
        {
            direct = direction;
            timeSinceLastFire = Time.time;
            fireRate = Random.Range(fireRateRange.x, fireRateRange.y);
            return true;
        }
        direct = Vector3.zero;
        return false;
    }
}
