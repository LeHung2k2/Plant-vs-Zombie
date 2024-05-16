using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lawn : MonoBehaviour
{
    public float speed = 5;
    public float damage = 500;

    [SerializeField] private AudioSource lawnSound;

    public bool run = false;

    void Update()
    {
        if (run == false) return;
        Vector3 right = Vector3.right;
        float translate = Time.deltaTime;
        Vector3 translation = right * translate * speed;
        transform.Translate(translation);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!run)
            {
                lawnSound.Play();  
                run = true;
                Destroy(gameObject,3.1f);
            }

            Zombie zombie = collision.GetComponent<Zombie>();
            zombie.TakeDamage(damage);
        }
    }

}
