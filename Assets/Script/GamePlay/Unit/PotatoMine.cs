using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMine : PlantUnit
{
    Animator anim;
    public List<Zombie> targets = new List<Zombie>();
    public bool atk=false;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(StartPotatoMine());

    }
 
    public override void Attack()
    {
        if(atk && targets.Count > 0)
        {
            for(int i = 0; i < targets.Count; i++)
            {
                targets[i].TakeDamage(attack);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
             atk = true;
             Zombie zombie= collision.GetComponent<Zombie>();
             targets.Add(zombie);
             anim.SetTrigger("Atk");
             zombie.TakeDamage(attack);
             plantAtkSound.Play();
             Destroy(gameObject, 1);
        }
    }
    public IEnumerator StartPotatoMine()
    {
        yield return new WaitForSeconds(3);
        anim.SetBool("atk", true);
    }
}
