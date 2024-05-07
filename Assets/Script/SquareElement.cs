using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SquareElement : MonoBehaviour,IPointerDownHandler
{
    public PlantType plantType;
    public int row = 0;
    public int col = 0;
    private PlantUnit currentPlant;
    public List<CardElement> cards = new List<CardElement>();
    [SerializeField] private AudioSource addPlantSound;
    public void SetId(int row,  int col)
    {
        this.row = row;
        this.col = col;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(GameController.instance.idSpawn == PlantType.None) 
        { 
            return;
        }

        if (currentPlant==null && GameController.instance.sunAmount >= GameController.instance.plantCards.plantCosts[GameController.instance.idSpawn])
        {

            plantType = GameController.instance.idSpawn;


            bool isCd = GameController.instance.plantCards.CheckCoolDown(plantType);
            if(isCd) { return; }

            PlantType id = GameController.instance.idSpawn;
        
            PlantUnit newUnit = GameController.instance.GetUnit(id);
            addPlantSound.Play();
            currentPlant = Instantiate(newUnit, this.transform.position, Quaternion.identity);
            GameController.instance.plantCards.FindPlant(plantType);
            GameController.instance.plantCards.Cost(plantType);
            GameController.instance.ChangeIdSpawn(PlantType.None);
           
                for (int i = 0; i < cards.Count; i++)
                {
                    if (cards[i].plantId == id)
                    {
                        StartCoroutine(cards[i].CoolDownDelay());
                        break;
                    }
                }
            
        }
        
    }
   
}