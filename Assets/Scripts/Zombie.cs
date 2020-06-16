using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float sightDistance;
    public float chassingSpeed;
    public float roamingSpeed;
    Transform target;
    public Transform moveSpot;
    private float waitTime;
    public float startWaitTime;
    public float minX, maxX, minY, maxY;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
    LineOfSight();
    Roaming();
    }
    
    void LineOfSight()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.forward, sightDistance);
        if(hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            if(hitInfo.collider.CompareTag("Player"))
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, chassingSpeed * Time.deltaTime);
                Vector2 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector2(direction.x, direction.y));
                transform.rotation = lookRotation;
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.forward * sightDistance, Color.green);
        }
    }

    void Roaming()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, roamingSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                Vector2 direction = (moveSpot.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector2(direction.x, direction.y));
                transform.rotation = lookRotation;
                waitTime = startWaitTime; 
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter2d()
    {

    }
}
