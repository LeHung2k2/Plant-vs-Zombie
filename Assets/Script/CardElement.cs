using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
public class CardElement : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public PlantType plantId;
    public Image plantSprite;
    public TMP_Text costTxt;
    public int coolDown;
    public bool isCoolDownTime;
    public Image coolDownImg;
    private Color startColor;
    private Color endColor;
    public void OnPointerUp(PointerEventData eventData)
    {
        if(plantSprite = null)
            return; 
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isCoolDownTime) return;
        GameController.instance.ChangeIdSpawn(plantId);
        
    }
    private void Start()
    {
        startColor = coolDownImg.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        StartCoroutine(CoolDownDelay());
    }
   
    public IEnumerator CoolDownDelay()
    {
        /*isCoolDownTime = true;

             yield return new WaitForSeconds(coolDown);


         isCoolDownTime = false;
     }
 }*/
        isCoolDownTime = true;
        float elapsedTime = 0f;
        

        while (elapsedTime < coolDown)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / coolDown); 

            
            coolDownImg.color = Color.Lerp(startColor, endColor, t);

            yield return null;
        }

        
        coolDownImg.color = endColor;

        isCoolDownTime = false;
    }

   
}
