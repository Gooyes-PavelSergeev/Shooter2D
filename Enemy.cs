using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float avoidingBulletsLevel;

    private bool isShoot;
    public Transform shotPoint;
    public GameObject bullet;

    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private Vector2 moveVelocity;

    private Transform playerTransform;

    private float timeBtwShots;
    public float startTimeBtwShots;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = rb.GetComponent<CircleCollider2D>();
        moveVelocity = new Vector2(0, -1) * speed;
        isShoot = false;
    }

    void Update()
    {
        try
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch (NullReferenceException)
        {
            playerTransform = transform;
        }
        Vector2 difference = transform.position - playerTransform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        Walk();
        TryShot();
        if (FeelDanger())
        {
            MoveOut();
        }
        if (isShoot)
        {
            Destroy(gameObject);
        }
    }

    private Vector2 ChooseRandomDir()
    {
        if (UnityEngine.Random.Range(0, 2) < 1)
            return transform.right;
        else
            return transform.right * -1 ;
    }

    private bool FeelDanger()
    {
        var bulletPrefab = GameObject.Find("Bullet(Clone)");
        if (bulletPrefab != null)
        {

            var runDistance = 1 / Vector2.Distance(transform.position, bulletPrefab.transform.position) * avoidingBulletsLevel;
            if (Mathf.Abs(bulletPrefab.transform.rotation.eulerAngles.z - transform.rotation.eulerAngles.z) < 180 + runDistance && Mathf.Abs(bulletPrefab.transform.rotation.eulerAngles.z - transform.rotation.eulerAngles.z) > 180 - runDistance)
            {
                return true;
            }
        }
        return false;
    }

    private void MoveOut()
    {
        Vector2 vector = ChooseRandomDir();
        moveVelocity = vector.normalized * speed;
    }

    private void Walk()
    {
        RaycastHit2D hitBack = Physics2D.Raycast(cc.transform.position, transform.up * -1, cc.radius * Mathf.Sqrt(2));
        RaycastHit2D hitForward = Physics2D.Raycast(cc.transform.position, transform.up, cc.radius * Mathf.Sqrt(2));
        if (hitBack.collider != null)
        {
            if (hitBack.collider.name == "Obstacles" || hitBack.collider.name == "SceneEdge")
            {
                moveVelocity = new Vector2(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1, 2)) * speed;
            }
        }
        else if (hitForward.collider != null)
        {
            if (hitForward.collider.name == "Obstacles" || hitForward.collider.name == "SceneEdge")
            {
                moveVelocity = new Vector2(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1, 2)) * speed;
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void TryShot()
    {
        var hitInfo = Physics2D.Raycast(transform.position + transform.up, transform.up);
        if (hitInfo)
        {
            if (hitInfo.collider.name == "Player")
            {
                if (timeBtwShots <= 0)
                {
                    Instantiate(bullet, shotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                    timeBtwShots -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage()
    {
        isShoot = true;
    }
}
