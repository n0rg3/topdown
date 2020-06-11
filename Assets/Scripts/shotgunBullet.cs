using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgunBullet : MonoBehaviour
{
    public GameObject bulletHitPrefab;
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject bulletHit = Instantiate(bulletHitPrefab, transform.position, Quaternion.identity);
        Destroy(bulletHit, 1f);
        Destroy(gameObject);
    }
}