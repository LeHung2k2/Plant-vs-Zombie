using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class PlantCardManager : MonoBehaviour
{
    public List<CardElement> cards = new List<CardElement>();
    public Dictionary<PlantType, int> plantCosts = new Dictionary<PlantType, int>();
    
    private void Start()
    {
        plantCosts.Add(PlantType.SunFlower, 50);
        plantCosts.Add(PlantType.PeaShooter, 100);
        plantCosts.Add(PlantType.SnowPeaShooter, 175);
        plantCosts.Add(PlantType.WallNut, 50);
        plantCosts.Add(PlantType.PlantTrap, 125);
        plantCosts.Add(PlantType.PeaShooter2, 225);
        plantCosts.Add(PlantType.PlantSilverTrap, 250);
        plantCosts.Add(PlantType.PeaShooterCone, 500);
        plantCosts.Add(PlantType.TallNut, 75);
        plantCosts.Add(PlantType.ThreePea, 325);
        plantCosts.Add(PlantType.PotatoMine, 25);
    }


    public void FindPlant(PlantType id)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].plantId == id)
            {
                StartCoroutine(cards[i].CoolDownDelay());
                break;
            }
        }
    }
    public bool CheckCoolDown(PlantType id)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].plantId == id)
            {
                
                return cards[i].isCoolDownTime;
            }
        }
        return false;
    }

    public void Cost(PlantType id)
    {
        int cost = plantCosts[id];
        GameController.instance.DeductSun(cost);
    }


}
