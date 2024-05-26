using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    public Transform backgroundRenderer;
    public List<Sprite> backgroundSprites;
    void Start()
    {
        ChangeBackgroundImg(GameData.LEVEL_CHOOSING);
    }

    public void ChangeBackgroundImg(int level)
    {
        if (level == 0)
        {
            backgroundRenderer.GetComponent<SpriteRenderer>().sprite = backgroundSprites[0];
        }
        else if (level == 1)
        {
            backgroundRenderer.GetComponent<SpriteRenderer>().sprite = backgroundSprites[1];
        }
        else
        {
            backgroundRenderer.GetComponent<SpriteRenderer>().sprite = backgroundSprites[2];
        }
    }
}
