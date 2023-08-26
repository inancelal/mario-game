using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goober : MonoBehaviour
{
    [SerializeField] float gooberSpeed = 1f;
    Rigidbody2D myRigidbody;
    BoxCollider2D myBoxCollider;


    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2 (gooberSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        gooberSpeed = -gooberSpeed;
        FlipEnemyFacing();    
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }

}
