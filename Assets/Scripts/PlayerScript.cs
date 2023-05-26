using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //import access to Card and Deck scripts
    public CardScript cardScript;
    public DeckScript deckScript;

    //int that will hold the hand value of the player or the dealer
    public int handValue = 0;

    //int that will hold the players current balance
    private int balance = 1000;

    //array of gameobjects to hold the cards in the players hand
    public GameObject[] hand;

    //int for holding the amount of cards in a hand and list to keep track of all aces
    public int cardIndex = 0;
    List<CardScript> aceList = new List<CardScript>();
    //starts the hand and deals two cards
    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    //function for dealing a single card and enabling its renderer. Also adds ace to ace list if necessary. Then returns hand value
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

    //checks each ace in acelist to make sure the user doesn't bust while also getting the max hand value
    public void AceCheck()
    {
        foreach(CardScript ace in aceList)
        {
            if (handValue + 10 < 22 && ace.GetValue() == 1)
            {
                ace.SetValue(11);
                handValue += 10;
            }else if (handValue > 21 && ace.GetValue() == 11)
            {
                ace.SetValue(1);
                handValue -= 10;
            }
        }
    }

    //takes an integer input and adjust the players balance accordingly
    public void adjustMoney(int amount)
    {
        balance += amount;
    }

    //returns the players current balance
    public int getBalance()
    {
        return balance;
    }

    //clears the players hand and other ints related to the current round
    public void ResetHand()
    {
        for(int i = 0;i < hand.Length;i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
        handValue = 0;
        aceList = new List<CardScript>();
    }
}
