using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{

    [SerializeField]
    List<SpriteInfo> enemies;

    [SerializeField]
    SpriteInfo player;

    [SerializeField]
    TextMesh instructions;

    string currentType = "AABB Collisions";
    bool aabbCollisions = true;
    bool playerColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        instructions = Instantiate(instructions, new Vector3(0, -0.5f, 0), new Quaternion());
        
    }

    // Update is called once per frame
    void Update()
    {
        instructions.text = "Current collision type: " + currentType;
        foreach (SpriteInfo enemy in enemies)
        {
            bool collisionCheck;
            if (aabbCollisions)
            {
                collisionCheck = AABBCollision(player.getSprite(), enemy.getSprite());
            }
            else
            {
                collisionCheck = CircleCollision(player.getSprite(), enemy.getSprite());
            }
            enemy.updateCollisions(collisionCheck);
            if (!playerColliding && collisionCheck)
            {
                playerColliding = true;
            }
        }
        player.updateCollisions(playerColliding);
        playerColliding = false;
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
    bool CircleCollision(SpriteRenderer player, SpriteRenderer enemy)
    {
        Vector3 pRadius = player.bounds.size / 2;
        Vector3 eRadius = enemy.bounds.size / 2;
        Vector3 distance = player.bounds.center - enemy.bounds.center;

        if (pRadius.magnitude + eRadius.magnitude < distance.magnitude)
        {
            return false;
        }

        return true;
    }

    public void OnFire()
    {
        if (aabbCollisions)
        {
            aabbCollisions = false;
            currentType = "Circle Collisions";
            
        }
        else
        {
            aabbCollisions = true;
            currentType = "AABB Collisions";
        }
    }

    

}
