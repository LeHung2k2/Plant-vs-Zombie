using System.Collections;
using System.Collections.Generic;
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
    public GameController gameController;

    [SerializeField] private AudioSource zomAtkSound;
    private void Start()
    {
        gameController = GameController.instance;
        anim = GetComponentInChildren<Animator>();
        originspeed = time;
    }

    void Update()
    {
        if (gameController.isLose)
            return;
        if (attacking||isDead)
        {
            return;
        }
        Vector3 left = Vector3.left;
        float timeSinceLastFrame = Time.deltaTime;
        Vector3 translation = left * timeSinceLastFrame * time;
        transform.Translate(translation);
    }
    public void ChangeColorAtk()
    {
        if (!isFrozen)
        {
            StartCoroutine(ChangeColor());
        }
    }
    public IEnumerator ChangeColor()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
    public void SlowDown()
    {
        if (!isFrozen)
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
            target.TakeDaage(attackDamage);
            zomAtkSound.Play();
            yield return new WaitForSeconds(attackSpeed);
        }
        
        attacking = false;
        anim.SetBool("Atk", false);
        zomAtkSound.Stop();
    }
   
}