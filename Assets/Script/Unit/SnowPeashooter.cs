using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPeashooter : PlantUnit
{
    public Transform shootPosition;
    public Zombie currentTarget;

    public override void Attack()
    {
        if (currentTarget != null)
        {
            Shoot();
            plantAtkSound.Play();
        }

    }


    private void Shoot()
    {
        Instantiate(GameController.instance.snowBulletPrefab, shootPosition.position, Quaternion.identity);
    }

    public override void SetTarget(Zombie target)
    {
        if (currentTarget != null) { return; }
        currentTarget = target;
    }

    public override void DeleteTarget()
    {
        currentTarget = null;
    }
}
