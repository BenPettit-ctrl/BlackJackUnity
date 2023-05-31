using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    //array of sprites used to hold all the sprites
    public Sprite[] cardSprites;
    //array to hold all the card values
    int[] cardValues = new int[53];
    int currentIndex = 0;
    
    //initializes card values on start of game
    void Start()
    {
        GetCardValues();
    }

    //initializes all cards with their corresponding values
    void GetCardValues()
    {
        int num = 0;
        for (int i = 0; i < cardSprites.Length; i++)
        {
            num = i;
            num %= 13;
            if (num > 10 || num == 0)
            {
                num = 10;
            }
            cardValues[i] = num++;
        }
    }

    //shuffles the cards using a random number to replace cards
    public void Shuffle()
    {
        for (int i = cardSprites.Length - 1; i > 0; --i)
        {
            int ran = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * (cardSprites.Length - 1)) + 1;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[ran];
            cardSprites[ran] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[ran];
            cardValues[ran] = value;
        }
        currentIndex = 1;
    }

    //deals a card by replacing the current blank card on the screen with the card at the top of the deck
    public int DealCard(CardScript cardScript)
    {
        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.SetValue(cardValues[currentIndex]);
        currentIndex++;
        return cardScript.GetValue();
    }

    //function to get the sprite for the blank card
    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }
}
