using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public enum bulletType { player, spin, turret}

    [SerializeField]
    float speed = 1f;

    [SerializeField]
    GameObject bullet;

    List<GameObject> allBullets = new List<GameObject>();
    //List<Vector3> bulletMovements = new List<Vector3>();

    Vector3 direction = Vector3.right;
    Vector3 velocity = Vector3.zero;
    Vector3 bulletPos = Vector3.zero;

    static Camera cam;
    static float height = 0;
    static float width = 0;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < allBullets.Count; i++)
        {
            velocity = allBullets[i].GetComponent<BulletInfo>().direction * speed * Time.deltaTime;

            bulletPos = allBullets[i].transform.position;
            bulletPos += velocity;

            if (bulletPos.x > width || 
                bulletPos.x < -width ||
                bulletPos.y > height ||
                bulletPos.y < -height)
            {
                Destroy(allBullets[i]);
                allBullets.RemoveAt(i);
                i--;
                continue;
            }

            allBullets[i].transform.position = bulletPos;
        }
    }

    public void createBullet(Transform ship, Vector3 bulletDirection, bulletType type)
    {
       
       GameObject newBullet = Instantiate(bullet);
       newBullet.transform.position = ship.position;
        newBullet.GetComponent<SpriteRenderer>().sprite =
                                newBullet.GetComponent<BulletInfo>().bulletSprites[(int)type];
        newBullet.GetComponent<BulletInfo>().myType = type;
       allBullets.Add(newBullet);
        newBullet.GetComponent<BulletInfo>().direction = bulletDirection;

    }

    public List<GameObject> GetAllBullets()
    {
        return allBullets;
    }
}
