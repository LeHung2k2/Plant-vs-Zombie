using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SunFlower : PlantUnit
{

    public override void Attack()
    {
        StartCoroutine(StartSpawnSun());
    }
    public IEnumerator StartSpawnSun()
    {
        yield return new WaitForSeconds(3);
        plantAtkSound.Play();
        SpawnSun();
    }
    private void SpawnSun()
    {
        SunElement sun = Instantiate(GameController.instance.sunPrefab,transform.position,Quaternion.identity);
    }
}
