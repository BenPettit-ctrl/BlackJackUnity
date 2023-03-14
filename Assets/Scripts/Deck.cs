using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    List<Card> cards;

    public Deck()
    {
        this.cards = new List<Card>();
    }

    public void createFullDeck()
    {
        foreach (Suit cardSuit in Suit.GetValues(typeof(Suit)))
        {
            foreach (Value cardValue in Value.GetValues(typeof(Value)))
            {
                this.cards.Add(new Card(cardSuit, cardValue));
            }
        }
    }

    public void moveAllToDeck(Deck moveTo)
    {
        int thisDeckSize = this.cards.size();

        for (int i = 0; i <thisDeckSize; i++)
        {
            moveTo.addCard(this.getCard(i));
        }

        for (int i = 0; i < thisDeckSize; i++)
        {
            this.removeCard(0);
        }
    }

    public void shuffleDeck()
    {
        List<Card> tempDeck = new List<Card>();
        Random random = new Random();
        int randomCardIndex = 0;
        int originalSize = this.cards.size();
        for (int i = 0; i < originalSize; i++)
        {
            randomCardIndex = random.next(0,(this.cards.size() - 1) + 1);
            tempDeck.add(this.cards.get(randomCardIndex));
            this.cards.remove(randomCardIndex);
        }
        this.cards = tempDeck;
    }

    public void removeCard(int i)
    {
        this.cards.remove(i);
    }

    public Card getCard(int i)
    {
        return this.cards.get(i);
    }

    public void drawCard(Deck fromDeck)
    {
        this.cards.add(fromDeck.getCard(0));
        fromDeck.removeCard(0);
    }

    public int handValue()
    {
        int value = 0;
        int aces = 0;
        foreach (Card aCard in this.cards)
        {
            switch (aCard.getValue())
            {
                case TWO: value += 2; break;
                case THREE: value += 3; break;
                case FOUR: value += 4; break;
                case FIVE: value += 5; break;
                case SIX: value += 6; break;
                case SEVEN: value += 7; break;
                case EIGHT: value += 8; break;
                case NINE: value += 9; break;
                case TEN: value += 10; break;
                case JACK: value += 10; break;
                case QUEEN: value += 10; break;
                case KING: value += 10; break;
                case ACE: aces += 1; break;
            }
        }
        for (int i = aces; i > 0; i--)
        {
            if (value > 10)
            {
                value += 1;
            }
            else
            {
                value += 11;
            }

        }
        return value;
    }
}
