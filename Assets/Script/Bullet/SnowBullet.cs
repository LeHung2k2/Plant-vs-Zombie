using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBullet : MonoBehaviour
{
    public float speed = 1;
    public float damage = 25;
    public void Start()
    {
        Destroy(gameObject,1.5f);
    }
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Zombie zombie = collision.GetComponent<Zombie>();
            zombie.TakeDamage(damage);
            zombie.SlowDown();
            Destroy(this.gameObject);
        }
    }
}
