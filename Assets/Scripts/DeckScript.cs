using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardValues = new int[53];
    int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void GetCardValues()
    {
        int num = 0;
        for (int i = 0; i <cardSprites.Length; i++)
        {
            num = i;
            num %= 13;
            if (num > 10 || num == 0)
            {
                num = 10;
            }
            cardValues[i] = num++;
        }
        currentIndex = 1;
    }

    public void Shuffle()
    {
        for (int i = cardSprites.Length -1; i > 0;--i)
        {
            int ran = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardSprites - 1) + 1;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[ran];
            cardSprites[ran] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[ran];
            cardValues[ran] = value;

        }
    }

    public int DealCard(CardScript cardScript)
    {
        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.SetValue(cardValues[currentIndex]);
        currentIndex++;
        return cardScript.getCardValue();

    }

    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }
}