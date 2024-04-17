using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SunElement : MonoBehaviour,IPointerDownHandler
{
    public int sunValue;
    private float posEndY ;
    public bool isFalling;

    private void Start()
    {
        posEndY = Random.Range(-4f, 2.5f);
        Destroy(gameObject, 6);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject.FindObjectOfType<GameController>().AddSun(sunValue);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (!isFalling) return;
        if (this.transform.position.y <= posEndY) return;
        transform.Translate(Vector2.down * 3 * Time.deltaTime);

    }
    
   
}
