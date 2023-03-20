using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    private int value = 0;
    
    public int GetValue()
    {
        return value;
    }

    public void SetValue(int passed)
    {
        value = passed;
    }

    public string GetSpriteName()
    {
        return GetComponent<SpriteRenderer>().sprite.name;
    }

    public void SetSprite(Sprite passed)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = passed;
    }

    public void ResetCard()
    {
        Sprite back = GameObject.Find("DeckController").GetComponent<DeckScript>().GetCardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = back;
        value = 0;
    }
}
