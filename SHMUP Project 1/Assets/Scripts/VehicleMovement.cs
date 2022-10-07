using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    List<Sprite> damageLevelSprites;

    int damageLevel = 0;

    float timeSinceLastFire = 0;

    [SerializeField]
    float hitCooldownTimer = 1;
    bool wasHit = false;

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

        GetComponent<SpriteRenderer>().sprite = damageLevelSprites[damageLevel];
    }

    // Update is called once per frame
    void Update()
    {
        //direction = Quaternion.EulerAngles(0, 0, turnAmount*Time.deltaTime) * direction;

        velocity = direction * speed * Time.deltaTime;

        vehiclePosition += velocity;
        
        if (vehiclePosition.x < -width)
        {
            vehiclePosition.x = -width;
        }
        else if(vehiclePosition.x > width)
        {
            vehiclePosition.x = width;
        }
        else if(vehiclePosition.y < -height)
        {
            vehiclePosition.y = -height;
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
            if (Time.time - timeSinceLastFire > fireRate)
            {
                bulletManager.createBullet(transform, Vector3.right, BulletMovement.bulletType.player);
                timeSinceLastFire = Time.time;
            }
        }
    }

    public void onHit()
    {
        if (!wasHit)
        {
            damageLevel++;
            wasHit = true;

            if (damageLevel > 3)
            {
                damageLevel = 3;
            }
            SpriteRenderer currentSprite = GetComponent<SpriteRenderer>();
            currentSprite.sprite = damageLevelSprites[damageLevel];
            currentSprite.color = Color.red;
        }
    }
}
