using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    Suit suit;
    Value value;

    public Card(Suit suit, Value value) {
        this.value = value;
        this.suit = suit;

        String toString()
        {
            return this.suit.toString() + "-" + this.value.toString();
        }

        Value getValue()
        {
            return this.value;
        }

        Suit getSuit() { return this.suit; }
    }
}
