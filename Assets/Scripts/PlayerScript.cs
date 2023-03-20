using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CardScript cardScript;
    public DeckScript deckScript;

    public int handValue = 0;

    private int balance = 1000;

    public GameObject[] hand;

    public int cardIndex = 0;
    public int aceCount = 0;
    List<CardScript> aceList = new List<CardScript>();
    void Start()
    {
        GetCard();
        GetCard();
    }

    public int GetCard()
    {
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        handValue += cardValue;
        if (cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());   
        }
        AceCheck();
        cardIndex++;
        return handValue;
    }
}
