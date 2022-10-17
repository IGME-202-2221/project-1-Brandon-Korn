using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    Vector3 vehiclePosition = Vector3.zero;

    [SerializeField]
    float fireRate;

    [SerializeField]
    BulletMovement bulletManager;

    [SerializeField]
    Text endgameScreen;

    [SerializeField]
    Animator animator;

    public int damageLevel = 0;

    float timeSinceLastFire = 0;

    [SerializeField]
    float hitCooldownTimer = 1;
    bool wasHit = false;
    bool alive = true;
    public bool gameOver = false;

     bool hyperdrive = false;
     float timeInHyperdrive = 100;

     bool shielded = false;
     float timeInSheild = 3;

    

    //[SerializeField]
    //float turnAmount = 0;

    Vector3 direction = Vector3.zero;

    Vector3 velocity = Vector3.zero;

    static Camera cam;
    static float height = 0;
    static float width = 0;

    // Start is called before the first frame update
    void Start()
    {
        vehiclePosition = transform.position;
        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;

        

        endgameScreen.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            //direction = Quaternion.EulerAngles(0, 0, turnAmount*Time.deltaTime) * direction;
            timeSinceLastFire += Time.deltaTime;
            

            velocity = direction * speed * Time.deltaTime;

            vehiclePosition += velocity;

            if (vehiclePosition.x < -width)
            {
                vehiclePosition.x = -width;
            }
            else if (vehiclePosition.x > width)
            {
                vehiclePosition.x = width;
            }
            else if (vehiclePosition.y < -height + height * 0.24f)
            {
                vehiclePosition.y = -height + height * 0.24f;
            }
            else if (vehiclePosition.y > height)
            {
                vehiclePosition.y = height;
            }

            transform.position = vehiclePosition;


            if (wasHit)
            {
                hitCooldownTimer -= Time.deltaTime;
                if (hitCooldownTimer < 0)
                {
                    wasHit = false;
                    GetComponent<SpriteRenderer>().color = Color.white;
                    hitCooldownTimer = 1;
                }
            }

            if (hyperdrive)
            {
                timeInHyperdrive -= Time.deltaTime;
                if (timeInHyperdrive < 0)
                {
                    hyperdrive = false;
                    speed = 4f;
                    fireRate = 0.65f;
                }
            }

            if (shielded)
            {
                timeInSheild -= Time.deltaTime;
                if (timeInSheild < 0)
                {
                    shielded = false;
                    GetComponent<SpriteRenderer>().color = Color.white;
                }
            }

        }

        else if (!alive)
        {
            
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8)
            {
                animator.StopPlayback();
                transform.position = new Vector3(2 * -width, 0, 0);
                endgameScreen.text = "GAME OVER\nScore: " + Collisions.score + "\nPress SPACE to play again";
                gameOver = true;
            }
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

        //if (direction != Vector3.zero)
        //transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!gameOver)
            {
                if (timeSinceLastFire > fireRate)
                {
                    bulletManager.createBullet(transform, Vector3.right, BulletMovement.bulletType.player);
                    timeSinceLastFire = 0;
                }
            }
            else
            {
                gameOver = false;
                endgameScreen.text = "";
                damageLevel = 0;
                animator.SetInteger("Damage", damageLevel);
                vehiclePosition = new Vector3(-5.5f, 0, 0);
                hitCooldownTimer = 0;
                timeInHyperdrive = 7;
                timeInSheild = 0;
                alive = true;
            }
        }
    }

    public void onHit()
    {
        if (!wasHit && !shielded)
        {
            damageLevel++;
            wasHit = true;

            if (damageLevel > 3)
            {
                alive = false;
            }
            SpriteRenderer currentSprite = GetComponent<SpriteRenderer>();
            animator.SetInteger("Damage", damageLevel);
            
            currentSprite.color = Color.red;
        }
    }

    public bool regainHealth()
    {
        if (!alive)
        {
            print("dead");
            return false;
        }

        if (damageLevel == 0)
        {
            print("healthy");
            return false;
        }

        damageLevel--;
        animator.SetInteger("Damage", damageLevel);

        return true;
    }

    public bool activateHyperdrive()
    {
        if (hyperdrive)
        {
            return false;
        }
        if (!alive)
        {
            return false;
        }

        hyperdrive = true;
        timeInHyperdrive = 10;
        speed = 5.5f;
        fireRate = 0.33f;

        return true;
    }

    public bool summonSheild()
    {
        if (shielded)
        {
            return false;
        }
        if (!alive)
        {
            return false;
        }

        timeInSheild = 6;
        GetComponent<SpriteRenderer>().color = Color.yellow;
        shielded = true;
        return true;
    }


}
