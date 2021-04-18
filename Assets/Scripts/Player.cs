using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player Settings")]
    public float Speed;
    public float Acceleration;
    public float smoothTime;
    [Header("Shooting Settings")]
    public float shotDelay;
    public float shotgunDelay;
    public float shotSpeed;
    public float shotgunSpeed;
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject Gun;
    public GameObject Shotgun;
    public GameObject bulletPrefab;
    public GameObject shotgunBulletPrefab;
    public GameObject muzzleFlashPrefab;
    //public GameObject Crosshair;
    Rigidbody2D player;
    Vector2 movement;
    Vector2 mousePos;
    float nextShotTime;
    void Start()
    {
        currentHealth = maxHealth;
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    Movement();
    Shoot();
    }
    void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            shotgunBullet.GetComponent<Rigidbody2D>().AddForce(Shotgun.transform.up * shotgunSpeed, ForceMode2D.Impulse);
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
        //Crosshair.transform.localPosition = new Vector2(mousePos.x, mousePos.y);
        if(Input.GetButton("Fire3"))
        {
            player.MovePosition(player.position + movement * Acceleration * Time.deltaTime);
        }
    }
}