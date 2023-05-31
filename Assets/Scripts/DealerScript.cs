using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerScript : MonoBehaviour
{
    //import access to Card and Deck scripts
    public CardScript cardScript;
    public DeckScript deckScript;

    //int that will hold the hand value of the player or the dealer
    public int dealerHandValue = 0;

    //array of gameobjects to hold the cards in the players hand
    public GameObject[] hand;

    //int for card count
    public int cardCount = 0;
    //int for holding the amount of cards in a hand and list to keep track of all aces
    public int dealerCardIndex = 0;
    List<CardScript> aceList = new List<CardScript>();


    //starts the hand and deals two cards
    public void StartDealerHand()
    {
        GetDealerCard();
        GetDealerCard();
    }

    //function for dealing a single card and enabling its renderer. Also adds ace to ace list if necessary. Then returns hand value
    public int GetDealerCard()
    {
        int cardValue = deckScript.DealCard(hand[dealerCardIndex].GetComponent<CardScript>());
        hand[dealerCardIndex].GetComponent<Renderer>().enabled = true;

        //logic for counting cards. skips the dealers first card
        if (dealerHandValue == 0)
        {
            if (cardValue == 1 || cardValue > 9)
            {
                cardCount--;
            }
            else if (cardValue < 7)
            {
                cardCount++;
            }
        }

        dealerHandValue += cardValue;
        if (cardValue == 1)
        {
            aceList.Add(hand[dealerCardIndex].GetComponent<CardScript>());
        }
        AceDealerCheck();
        dealerCardIndex++;
        return dealerHandValue;
    }

    //checks each ace in acelist to make sure the user doesn't bust while also getting the max hand value
    public void AceDealerCheck()
    {
        foreach (CardScript ace in aceList)
        {
            if (dealerHandValue + 10 < 22 && ace.GetValue() == 1)
            {
                ace.SetValue(11);
                dealerHandValue += 10;
            }
            else if (dealerHandValue > 21 && ace.GetValue() == 11)
            {
                ace.SetValue(1);
                dealerHandValue -= 10;
            }
        }
    }

    //clears the players hand and other ints related to the current round
    public void ResetDealerHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        dealerCardIndex = 0;
        dealerHandValue = 0;
        aceList = new List<CardScript>();
        cardCount = 0;
    }

    public int getDealerCardCount()
    {
        return cardCount;
    }
}
