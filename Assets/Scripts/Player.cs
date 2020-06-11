using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player Settings")]
    public float Speed;
    public float Acceleration;
    [Header("Shooting Settings")]
    public float shotDelay;
    public float shotgunDelay;
    public float shotSpeed;
    public float shotgunSpeed;
    public GameObject Gun;
    public GameObject Shotgun;
    public GameObject bulletPrefab;
    public GameObject shotgunBulletPrefab;
    public GameObject muzzleFlashPrefab;
    public GameObject Crosshair;
    Rigidbody2D player;
    Vector2 movement;
    Vector2 mousePos;
    float nextShotTime;
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    //faceMouse();
    Movement();
    Shoot();
    //MouseInput();
    }
    void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //float moveHorizontal = Input.GetAxis("Horizontal");
	//float moveVertical = Input.GetAxis("Vertical");
    //player.velocity = new Vector2(moveHorizontal, moveVertical);
    //if(Input.GetKey("left shift"))
        //{
        //player.velocity = new Vector2(moveHorizontal * Acceleration, moveVertical * Acceleration);
        //}
    }
    void faceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2
            (mousePosition.x - transform.position.x,
             mousePosition.y - transform.position.y);
        transform.up = direction;
        Crosshair.transform.localPosition = new Vector2(mousePos.x, mousePos.y);
    }

    void Shoot()
    {
        if (Time.time > nextShotTime && Input.GetButton("Fire1"))
        {
            GameObject gunBullet = Instantiate(bulletPrefab, Gun.transform.position, Gun.transform.rotation);
            gunBullet.GetComponent<Rigidbody2D>().AddForce(Gun.transform.up * shotSpeed, ForceMode2D.Impulse);
            nextShotTime = Time.time + shotDelay;
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, Gun.transform.position, Gun.transform.rotation);
            Destroy(muzzleFlash, 0.2f);
            Destroy(gunBullet, 0.1f);
        }
        if (Time.time > nextShotTime && Input.GetButton("Fire2"))
        {
            GameObject shotgunBullet = Instantiate(shotgunBulletPrefab, Shotgun.transform.position, Shotgun.transform.rotation);
            shotgunBullet.GetComponent<Rigidbody2D>().AddForce(Shotgun.transform.up * shotSpeed, ForceMode2D.Impulse);
            nextShotTime = Time.time + shotgunDelay;
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, Shotgun.transform.position, Shotgun.transform.rotation);
            Destroy(muzzleFlash, 0.2f);
            Destroy(shotgunBullet, 0.07f);

        }
    }
    void FixedUpdate()
    {
        player.MovePosition(player.position + movement * Speed * Time.deltaTime);
        Vector2 lookDir = mousePos - player.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        player.rotation = angle;
        Crosshair.transform.localPosition = new Vector2(mousePos.x, mousePos.y);
        if(Input.GetButton("Fire3"))
        {
            player.MovePosition(player.position + movement * Acceleration * Time.deltaTime);
        }
    }
}