using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
    public Vector3 direction = Vector3.zero;

    public BulletMovement.bulletType myType = 0;

    [SerializeField]
    public List<Sprite> bulletSprites;

    [SerializeField]
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
