using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    [SerializeField]
    List<GameObject> enemyObjects;

    [SerializeField]
    float spawnInterval, finalSpawnInterval;

    [SerializeField]
    BulletMovement bulletManager;

    [SerializeField]
    GameObject player;

    float timeSinceLastSpawn = 0;
    float timePlaying = 0;

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
        if(timePlaying > 10)
        {
            timePlaying = 0;

            if (spawnInterval > finalSpawnInterval)
            {
                spawnInterval -= 0.1f;
            }
        }

        if (timeSinceLastSpawn > spawnInterval)
        {
            int spawnChance = Random.Range(0, 100);
            if (spawnChance < 75)
            {
                SpawnEnemy();
            }
            else if (spawnChance < 100)
            {
                SpawnTurret();
            }
            timeSinceLastSpawn = 0;
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

            if (ship.GetComponent<TurretMovement>() != null)
            {

                if (ship.GetComponent<TurretMovement>().fireBullet())
                {
                    dir = player.transform.position - ship.transform.position;
                    dir.Normalize();
                    bulletManager.createBullet(ship.transform, dir, BulletMovement.bulletType.turret);
                }
            }
            else if (ship.GetComponent<EnemyMovement>() != null)
            {

                if (ship.GetComponent<EnemyMovement>().fireBullet())
                {
                    bulletManager.createBullet(ship.transform, Vector3.left, BulletMovement.bulletType.spin);
                }
            }
        }

        timeSinceLastSpawn += Time.deltaTime;
        timePlaying += Time.deltaTime;
    }

    void SpawnEnemy()
    {
        float randomY = Random.Range(-height + height*0.24f + 1.5f, height - 1.5f);
    
        GameObject spawned = Instantiate(enemyObjects[0], transform);
        spawned.transform.position = new Vector3(width + 2, randomY, 0);
        enemies.Add(spawned);
    }

    void SpawnTurret()
    {
        float randomY = Random.Range(-height + height*0.24f +0.5f, -1.5f);
        if (Random.Range(0, 2) == 1)
        {
            randomY = -randomY;
        }

        GameObject spawned = Instantiate(enemyObjects[1], transform);
        spawned.transform.position = new Vector3(width + 2, randomY, 0);
        enemies.Add(spawned);
    }

    public List<GameObject> GetAllEnemies()
    {
        return enemies;
    }

    public void ResetEnemies()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
        timeSinceLastSpawn = 0;
        timePlaying = 0;
        enemies.Clear();
        spawnInterval = 1.5f;
    }


}
