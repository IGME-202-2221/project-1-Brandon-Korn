using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpriteInfo : MonoBehaviour
{

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCollisions(bool isColliding)
    {
        if (isColliding)
        {
            sprite.color = Color.red;
        }
        else
        {
            sprite.color = Color.white;
        }
    }

    public SpriteRenderer getSprite()
    {
        return sprite;
    }
    
}
