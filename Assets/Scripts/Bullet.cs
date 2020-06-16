using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject hitPrefab;
    public GameObject zombieHitPrefab;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Explosive")
        {
        GameObject bulletHit = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosionPrefab, 2f);
        Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Obstacle")
        {
        GameObject hit = Instantiate(hitPrefab, transform.position, transform.rotation);
        Destroy(hit, 1f);
        Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Zombie")
        {
        GameObject zombieHit = Instantiate(zombieHitPrefab, transform.position, transform.rotation);
        Destroy(zombieHit, 2f);
        Destroy(gameObject);
        }
    }
}