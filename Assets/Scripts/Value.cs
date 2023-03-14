using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Value : ushort
{
    TWO,
    THREE,
    FOUR,
    FIVE,
    SIX,
    SEVEN,
    EIGHT,
    NINE,
    TEN,
    JACK,
    QUEEN,
    KING,
    ACE
}

class ValueMethods
{
    public string toString(Value v1)
    {
        switch (v1)
        {
            case Value.TWO:
                return "Two";
            case Value.THREE:
                return "Three";
            case Value.FOUR:
                return "Four";
            case Value.FIVE:
                return "Five";
            case Value.SIX:
                return "Six";
            case Value.SEVEN:
                return "Seven";
            case Value.EIGHT:
                return "Eight";
            case Value.NINE:
                return "Nine";
            case Value.TEN:
                return "Ten";
            case Value.JACK:
                return "Jack";
            case Value.QUEEN:
                return "Queen";
            case Value.KING:
                return "King";
            case Value.ACE:
                return "Ace";
            default:
                return "Uh Oh";
        }
    }
}
