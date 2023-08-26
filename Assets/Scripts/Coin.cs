using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickup;
    [SerializeField] int pointsForCoinPickup = 100;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position);
            Destroy(gameObject);
             
        }
    }
}
