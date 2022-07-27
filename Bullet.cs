using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isSolid;

    private int ricochetNum = 0;
    public int maxRicochetNum;

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, isSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("SceneEdge"))
            {
                Destroy(gameObject);
            }

            else if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage();
            }

            else if (hitInfo.collider.CompareTag("Player"))
            {
                hitInfo.collider.GetComponent<Player>().TakeDamage();
            }

            if (ricochetNum != maxRicochetNum)
            {
                Ray ray = new Ray(transform.position, transform.up);
                Vector2 reflectDir = Vector2.Reflect(ray.direction, hitInfo.normal);
                float rotZ = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                ricochetNum++;
            }
            else if (gameObject != null)
                Destroy(gameObject);
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}