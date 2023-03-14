using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Suit :ushort
{
   HEART,
   SPADE,
   CLUB,
   DIAMOND
}

static class SuitMethods
{
    public static string toString(Suit s1)
    {
        switch (s1) {
            case Suit.HEART:
                return "Heart";
            case Suit.SPADE:
                return "Spade";
            case Suit.CLUB:
                return "Club";
            case Suit.DIAMOND:
                return "Diamond";
            default:
                return "Uh Oh";
        }
    }
}
