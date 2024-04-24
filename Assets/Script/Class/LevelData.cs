using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int level;
    public List<ZombieQuantity> zombies = new List<ZombieQuantity>();
    public int GetTotalZombie ()
    {
        int total = 0;

        for (int i = 0; i < zombies.Count; i++)
        {
            total += zombies[i].quantity;
        }

        return total;
    }
}
