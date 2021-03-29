using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_deprecated : MonoBehaviour
{
    [SerializeField]
    float roamingSpeed;
    [SerializeField]
    float range;
    [SerializeField]
    float maxDistance;
    public float chaseSpeed;
    Transform target;
    Vector2 wayPiont;
    Rigidbody2D enemy;

    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        // SetNewDestination();
        target = PlayerManager.instance.Player.transform;
    }

    //void Update()
    //{
    //    transform.position = Vector2.MoveTowards(transform.position, wayPiont, roamingSpeed * Time.deltaTime);
    //    Vector2 lookDir = wayPiont - enemy.position;
    //    float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
    //    enemy.rotation = angle;
    //    if (Vector2.Distance(transform.position, wayPiont) < range)
    //    {
    //        SetNewDestination();
    //    }
    //}

    void SetNewDestination()
    {
        wayPiont = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.tag == "Bullet");
        {
        Destroy(gameObject);
        }
        if (collision.gameObject.tag == "ShotgunBullet");
        {
        Destroy(gameObject);
        }
    }
    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, roamingSpeed * Time.deltaTime);
        //Vector2 lookDir = target.transform - enemy.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //enemy.rotation = angle;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player") ;
        {
            Chase();
        }
    }


}