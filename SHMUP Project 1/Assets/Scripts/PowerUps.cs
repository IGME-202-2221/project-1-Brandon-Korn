using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUps : MonoBehaviour
{
    enum PowerUpType { Health, Sheild, Speed };

    [SerializeField]
    int cost;

    [SerializeField]
    Sprite sprite;

    [SerializeField]
    PowerUpType type;

    [SerializeField]
    GameObject player;

    SpriteRenderer spriteRender;

    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Pip_Movement.pipsGained < cost)
        {
            spriteRender.color = Color.gray;
        }
        else
        {
            spriteRender.color = Color.white;
        }
    }

    public void OnActivation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            
            if (Pip_Movement.pipsGained >= cost)
            {
                VehicleMovement playerScript = player.GetComponent<VehicleMovement>();
                if (type == PowerUpType.Health)
                {
                    
                    if (playerScript.regainHealth())
                    {
                        
                        Pip_Movement.pipsGained -= cost;
                    }
                }

                else if (type == PowerUpType.Speed)
                {
                    print(type);
                    if (playerScript.activateHyperdrive())
                    {
                        
                        Pip_Movement.pipsGained -= cost;
                    }
                }
                else if (type == PowerUpType.Sheild)
                {
                    print(type);
                    if (playerScript.summonSheild())
                    {
                        
                        Pip_Movement.pipsGained -= cost;
                    }
                }


            }
        }
    }
}
