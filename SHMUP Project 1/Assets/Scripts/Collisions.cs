using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collisions : MonoBehaviour
{

    [SerializeField]
    EnemyControls enemyManager;

    [SerializeField]
    BulletMovement bulletManager;

    [SerializeField]
    GameObject player;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Pip_Movement pipControls;

    public static int score = 0;

    bool gameEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCollisions(enemyManager.GetAllEnemies(), bulletManager.GetAllBullets());

        playerCollisions(bulletManager.GetAllBullets(), enemyManager.GetAllEnemies(), player);

        scoreText.text = "Score: " + score;

        if (player.GetComponent<VehicleMovement>().gameOver &&
            !gameEnded)
        {
            gameEnded = true;
        }

        if (gameEnded && !player.GetComponent<VehicleMovement>().gameOver)
        {
            enemyManager.ResetEnemies();
            bulletManager.resetBullets();
            pipControls.ResetPips();
            gameEnded = false;
            score = 0;
        }
    }

    void enemyCollisions(List<GameObject> shipList, List<GameObject> bulletList)
    {
        for (int i = 0; i < shipList.Count; i++)
        {
            for (int j = 0; j < bulletList.Count; j++)
            {
                if (bulletList[j].GetComponent<BulletInfo>().myType == BulletMovement.bulletType.player) {


                    if (CircleCollision(shipList[i].GetComponent<SpriteRenderer>(), bulletList[j].GetComponent<SpriteRenderer>()))
                    {
                        int pipNum = 0;
                        if (shipList[i].GetComponent<TurretMovement>() != null)
                        {
                            pipNum = Random.Range(0, 2);
                        }
                        else if (shipList[i].GetComponent<EnemyMovement>() != null)
                        {
                            pipNum = Random.Range(0, 4);
                        }
                        pipControls.CreatePips(pipNum, shipList[i].transform);


                        score += 100;
                        

                        Destroy(shipList[i]);
                        shipList.RemoveAt(i);
                        i--;

                        Destroy(bulletList[j]);
                        bulletList.RemoveAt(j);
                        j--;

                        j = bulletList.Count;
                    }
                }
            }
        }

    }

    void playerCollisions(List<GameObject> bulletList, List<GameObject> shipList, GameObject player)
    {
        bool playerHit = false;

        for (int i = 0; i < bulletList.Count; i++)
        {
            if (bulletList[i].GetComponent<BulletInfo>().myType != BulletMovement.bulletType.player)
            {
                if (CircleCollision(bulletList[i].GetComponent<SpriteRenderer>(), player.GetComponent<SpriteRenderer>()))
                {
                    Destroy(bulletList[i]);
                    bulletList.RemoveAt(i);
                    i--;

                    playerHit = true;
                }
            }
        }

        for (int i = 0; i < shipList.Count; i++)
        {
            if (CircleCollision(shipList[i].GetComponent<SpriteRenderer>(), player.GetComponent<SpriteRenderer>()))
            {
                Destroy(shipList[i]);
                shipList.RemoveAt(i);
                i--;

                playerHit = true;
            }
        }

        if (playerHit)
        {
            player.GetComponent<VehicleMovement>().onHit();
        }
    }
    
    bool AABBCollision(SpriteRenderer player, SpriteRenderer enemy)
    {

        if (player.bounds.max.x > enemy.bounds.min.x &&
            player.bounds.min.x < enemy.bounds.max.x &&
            player.bounds.max.y > enemy.bounds.min.y &&
            player.bounds.min.y < enemy.bounds.max.y)
        {
            return true;
        }

        return false;
    }
    bool CircleCollision(SpriteRenderer ship, SpriteRenderer bullet)
    {
        Vector3 pRadius = ship.bounds.size / 4;
        Vector3 eRadius = bullet.bounds.size / 4;
        Vector3 distance = ship.bounds.center - bullet.bounds.center;

        if (pRadius.magnitude + eRadius.magnitude < distance.magnitude)
        {
            return false;
        }

        return true;
    }

    
    
    

}
