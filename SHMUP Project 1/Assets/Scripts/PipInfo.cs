using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipInfo : MonoBehaviour
{
    [SerializeField]
    public float speed;

    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMove()
    {
        transform.position += direction * speed * Time.deltaTime;
        speed = speed * 0.95f;
    }
}
