using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    //int to hold value of card
    public int value = 0;
    
    //returns the int value of the card
    public int GetValue()
    {
        return value;
    }

    //sets the value of the card(used for ace logic)
    public void SetValue(int passed)
    {
        value = passed;
    }

    //returns the name of the sprite
    public string GetSpriteName()
    {
        return GetComponent<SpriteRenderer>().sprite.name;
    }

    //sets the sprite image
    public void SetSprite(Sprite passed)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = passed;
    }

    //resets a card to blank and changes value to zero
    public void ResetCard()
    {
        Sprite back = GameObject.Find("Deck").GetComponent<DeckScript>().GetCardBack();
        gameObject.GetComponent<SpriteRenderer>().sprite = back;
        value = 0;
    }
}
