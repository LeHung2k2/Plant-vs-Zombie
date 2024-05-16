using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Animator anim;
    public float speed = 1;
    public float damage = 25;
    private void Start()
    {
        Destroy(gameObject,1.4f);

    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Zombie zombie = collision.GetComponent<Zombie>();
            zombie.TakeDamage(damage);
            zombie.ChangeColorAtk();
            Destroy(gameObject,0.1f);
        }
    }
}
