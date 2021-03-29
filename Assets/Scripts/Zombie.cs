using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float sightDistance;
    public float chaseSpeed;
    public float roamingSpeed;
    Transform Player;
    public Transform moveSpot;
    private float waitTime;
    public float startWaitTime;
    public float minX, maxX, minY, maxY;
    public float offset;

    void Start()
    {
        Player = PlayerManager.instance.Player.transform;
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        //LineOfSight();
        Roaming();
        Chase();
    }

    void Chase()
    {
        if (gameObject.GetComponent<FieldOfView>().visibleTargets.Contains(Player))
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, chaseSpeed * Time.deltaTime);
            Vector2 direction = (Player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        }
    }

    //void LineOfSight()
    //{
    //    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, sightDistance);
    //    if(hitInfo.collider != null)
    //    {
    //        Debug.DrawLine(transform.position, hitInfo.point, Color.red);
    //        if(hitInfo.collider.CompareTag("Player"))
    //        {
    //            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, chaseSpeed * Time.deltaTime);
    //            Vector2 direction = (target.position - transform.position).normalized;
    //            //Quaternion lookRotation = Quaternion.LookRotation(new Vector2(direction.x, direction.y));
    //            //transform.rotation = lookRotation;
    //            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //            transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    //        }
    //    }
    //    else
    //    {
    //        Debug.DrawLine(transform.position, transform.position + transform.right * sightDistance, Color.green);
    //    }
    //}

    void Roaming()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, roamingSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                Vector2 direction = (moveSpot.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
                waitTime = startWaitTime; 
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.tag == "Bullet");
        Destroy(gameObject);
        if (collision.gameObject.tag == "ShotgunBullet");
        Destroy(gameObject);
    }
}
