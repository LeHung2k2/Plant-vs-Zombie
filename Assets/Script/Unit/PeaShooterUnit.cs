using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PeaShooterUnit : PlantUnit
{
    public List<Transform> shootPositions = new List<Transform>();
    public Zombie currentTarget;
    public override void Attack()
    {
        if(currentTarget != null)
        {
            Shoot();
            plantAtkSound.Play();
        }
        
    }
    

    private void Shoot()
    {
        foreach(Transform t in shootPositions)
        {
            Instantiate(GameController.instance.bulletPrefab,t.position,Quaternion.identity);
        }
    }

    public override void SetTarget( Zombie target)
    {
        if(currentTarget != null) { return; }
        currentTarget = target;
    }

    public override void DeleteTarget()
    {
        currentTarget = null;
    }

}




