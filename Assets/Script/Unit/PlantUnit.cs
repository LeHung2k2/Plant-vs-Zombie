using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantUnit : MonoBehaviour
{
    public PlantType plantType;
    public float hitPoint;
    public int attack;
    public float attackSpeed;
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(DoImpact());
    }


    public IEnumerator DoImpact()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(attackSpeed);
          
        }
    }


    public void TakeDaage(float damage)
    {
        hitPoint -= damage;
        if (hitPoint <= 0) { Destroy(gameObject); }
    }

    public virtual void Attack ()
    {

    }

    public virtual void SetTarget ( Zombie target)
    {

    }

    public virtual void DeleteTarget ()
    {

    }
}
