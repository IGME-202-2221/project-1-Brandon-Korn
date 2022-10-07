using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    float spawnInterval;

    [SerializeField]
    BulletMovement bulletManager;

    float timeSinceLastSpawn = 0;

    List<GameObject> enemies = new List<GameObject>();

    static Camera cam;
    static float height = 0;
    static float width = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSpawn = Time.time;

        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;

        SpawnEnemy();

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeSinceLastSpawn > spawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = Time.time;
        }

        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].transform.position.x < -width)
            {
                Destroy(enemies[i]);
                enemies.RemoveAt(i);
                i--;
            }
        }

        foreach(GameObject ship in enemies)
        {
            Vector3 dir = Vector3.zero;
            if (ship.GetComponent<EnemyMovement>().fireBullet(out dir))
            {
                bulletManager.createBullet(ship.transform, dir, BulletMovement.bulletType.spin);
            }
        }
    }

    void SpawnEnemy()
    {
        float randomY = Random.Range(-height + 1, height - 1);

        GameObject spawned = Instantiate(enemy, transform);
        spawned.transform.position = new Vector3(width + 2, randomY, 0);
        enemies.Add(spawned);
    }

    public List<GameObject> GetAllEnemies()
    {
        return enemies;
    }


}
