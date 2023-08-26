using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bullet : MonoBehaviour
{

    Rigidbody2D myRigidbody;
    [SerializeField] float bulletSpeed = 5f;
    PlayerMovemont player;
    float xSpeed;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovemont>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        myRigidbody.velocity = new Vector2 (xSpeed, 0f) ;
        //new Vector2 (1f, 0f)
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);    
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);    
    }
}
