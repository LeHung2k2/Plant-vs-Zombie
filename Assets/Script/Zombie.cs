using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieType zombieId;
    public float hitpoint = 100;
    public float attackSpeed = 1;
    public float attackDamage = 3f;
    private float time = 0.3f;
    [SerializeField]   private bool attacking;
    [SerializeField] public bool isFrozen = false;
    private float originspeed;
    private Animator anim;
    public bool isDead=false;

    [SerializeField] private AudioSource zomAtkSound;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        originspeed = time;
    }

    void Update()
    {
        if (attacking||isDead)
        {
            return;
        }
        Vector3 left = Vector3.left;
        float timeSinceLastFrame = Time.deltaTime;
        Vector3 translation = left * timeSinceLastFrame * time;
        transform.Translate(translation);
    }
    public void SlowDown()
    {
        if(!isFrozen)
        {
            StartCoroutine(DurationTime(4));
        }
    }
    public IEnumerator DurationTime(float duration)
    {
        isFrozen = true;
        time /= 3;
        attackSpeed /= 2;
        GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        yield return new WaitForSeconds(duration);
        time = originspeed;
        attackSpeed *= 2;
        isFrozen = false;
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
    public void TakeDamage(float damage)
    {
        hitpoint -= damage;
        if (hitpoint <= 0)
        {
            isDead = true;
            anim.SetTrigger("Die");
            Die();
        }
       
    }
    void Die()
    {
        Destroy(gameObject,1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plant"))
        {
            //Debug.Log(" ATACK PLANT");
           
            attacking = true;
            anim.SetBool("Atk",true);
            PlantUnit target = collision.gameObject.GetComponent<PlantUnit>();
            StartCoroutine(AttackPlant(target));
        }
      


    }

    public IEnumerator AttackPlant(PlantUnit target)
    {
        while (target.hitPoint > 0)
        {
            zomAtkSound.Play();
            target.TakeDaage(attackDamage);
            yield return new WaitForSeconds(attackSpeed);
        }
        
        attacking = false;
        anim.SetBool("Atk", false);
        zomAtkSound.Stop();
    }
   
}