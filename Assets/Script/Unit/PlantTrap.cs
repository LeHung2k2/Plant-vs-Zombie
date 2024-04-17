using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTrap : PlantUnit
{
    public List<Zombie> targets = new List<Zombie>();
    public override void Attack()
    {
        if(targets.Count > 0)
        {
            for(int i = 0; i < targets.Count; i++)
            {
                targets[i].TakeDamage(attack);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Zombie zombie = collision.GetComponent<Zombie>();
            targets.Add(zombie);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Zombie zombie = collision.GetComponent<Zombie>();
        targets.Remove(zombie);
    }

}
