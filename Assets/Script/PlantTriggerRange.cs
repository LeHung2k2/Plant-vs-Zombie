using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTriggerRange : MonoBehaviour
{
    PlantUnit unit;

    private void Awake()
    {
        unit = GetComponentInParent<PlantUnit>();
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Zombie target = collision.gameObject.GetComponent<Zombie>();
           
            unit.SetTarget( target);
            
              // StartCoroutine(unit.DoImpact());
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Zombie target = collision.gameObject.GetComponent<Zombie>();
            unit.DeleteTarget();
            //if (target == unit.currentTarget)
            //{
            //   unit. currentTarget = null;
            //}
        }
    }
}
