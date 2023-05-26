using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //initializes all the buttons and the bet integer to keep track of current bet
    public Button dealButton;
    public Button hitButton;
    public Button standButton;
    public Button doubleButton;
    public Button betButton;
    public Button shoveButton;
    public Button countButton;
    private int intBet = 0;

    //initializes player and dealer objects
    public PlayerScript playerScript;
    public PlayerScript dealerScript;

    //initializes all text objects so they can be edited
    public TextMeshProUGUI standButtonText;
    public TextMeshProUGUI bankText;
    public TextMeshProUGUI handText;
    public TextMeshProUGUI betText;
    public TextMeshProUGUI dealerHandText;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI countText;

    //initializes the hideCard object used for hiding the dealers card.
    public GameObject hideCard;

    //adds listeners for all the button and sets the buttons to their starting position
    void Start()
    {
        dealButton.onClick.AddListener(() => DealClicked());
        hitButton.onClick.AddListener(() => HitClicked());
        standButton.onClick.AddListener(() => StandClicked());
        doubleButton.onClick.AddListener(() => DoubleClicked());
        betButton.onClick.AddListener(() => BetClicked());
        shoveButton.onClick.AddListener(() => ShoveClicked());
        countButton.onClick.AddListener(() => CountClicked());
        standButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(false);
        doubleButton.gameObject.SetActive(false);
        dealButton.gameObject.SetActive(false);
    }

    //the function for when the deal button is clicked
    private void DealClicked()
    {
        //removes the bet amount from the players balance
        playerScript.adjustMoney(-intBet);
        //resets each players hand
        playerScript.ResetHand();
        dealerScript.ResetHand();
        //hides text that shouldn't be shown till hand is over
        mainText.gameObject.SetActive(false);
        dealerHandText.gameObject.SetActive(false);
        //shuffles deck and deals cards to each player
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
        //adds text for hand values of player's and dealer's hand
        handText.text = "Hand: " + playerScript.handValue.ToString();
        dealerHandText.text = "Hand: " + dealerScript.handValue.ToString();
        //hides or shows buttons/objects
        hideCard.GetComponent<Renderer>().enabled = true;
        dealButton.gameObject.SetActive(false);
        betButton.gameObject.SetActive(false);
        shoveButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
        doubleButton.gameObject.SetActive(true);
        //edits text for the player's balance
        bankText.text = "$" + playerScript.getBalance().ToString();
        //decides whether the player should see the double button based on their current bet and balance
        if (intBet <= playerScript.getBalance())
        {
            doubleButton.gameObject.SetActive(true);
        } else
        {
            doubleButton.gameObject.SetActive(false);
        }
        //ends the round immidiately if either player or dealer hits 21
        if (dealerScript.handValue == 21 || playerScript.handValue == 21)
        {
            RoundOver();
        }
    }

    //the function for when the hit button is clicked
    private void HitClicked()
    {
        //hides the double button
        doubleButton.gameObject.SetActive(false);
        //if the players has less than 11 cards (the max amount possible in a hand of blackjack) it deals another card and checks if they bust
        if (playerScript.cardIndex <=10)
        {
            playerScript.GetCard();
            handText.text = "Hand: " + playerScript.handValue.ToString();
            if (playerScript.handValue > 20)
            {
                RoundOver();
            }
        } 
        //checks if the player just drew their eleventh card. If so, runs roundover and hides the hit button
        if (playerScript.cardIndex ==11)
        {
            hitButton.gameObject.SetActive(false);
            RoundOver();
        }
    }

    //the function for when the stand button is clicked
    private void StandClicked()
    {
        //hides all gameplay buttons
        doubleButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        //hits the dealer as long as they have less than 11 cards and then ends the round
        for (int i = 0; i < 11; i++)
        {
            HitDealer();
        }
        RoundOver();
    }

    //the function for when the double button is clicked
    private void DoubleClicked()
    {
        //hides gameplay buttons and and removes the the new doubled bet from the players balance
        doubleButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        playerScript.adjustMoney(-intBet);
        //doubles bet
        intBet += intBet;
        //deals the player 1 additional card
        playerScript.GetCard();
        //edits text for bet, balance, and hand
        betText.text = "BET: $" + intBet;
        bankText.text = "$" + playerScript.getBalance();
        handText.text = "Hand: " + playerScript.handValue.ToString();
        //runs the stand clicked function to handle dealers hand
        StandClicked();
    }

    //the function for when the dealer hits a card
    private void HitDealer()
    {
        //dealer hits on 16 and stands on 17
        if (dealerScript.handValue < 17 && dealerScript.cardIndex < 11)
        {
            dealerScript.GetCard();
            dealerHandText.text = "Hand: " + dealerScript.handValue.ToString();
        }
    }

    //the function run at the end of every hand to determine payout
    void RoundOver()
    {
        //booleans for bust and 21
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;
        //final game decision. First checks for push, then player 21, then player win, finally dealer wins
        if (playerScript.handValue == dealerScript.handValue)
        {
            playerScript.adjustMoney(intBet);
            mainText.text = "Push, bets returned";
        } else if (player21)
        {
            playerScript.adjustMoney(intBet +((intBet * 3) / 2));
            mainText.text = "BlackJack";
        } else if (!playerBust && (playerScript.handValue > dealerScript.handValue || dealerBust))
        {
            playerScript.adjustMoney(intBet * 2);
            mainText.text = "Player Wins";
        } else
        {
            mainText.text = "Dealer Wins";
        }
        //resets buttons, objects, and text for next round of blackjack
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        doubleButton.gameObject.SetActive(false);
        betButton.gameObject.SetActive(true);
        shoveButton.gameObject.SetActive(true);
        mainText.gameObject.SetActive(true);
        dealerHandText.gameObject.SetActive(true);
        hideCard.GetComponent<Renderer>().enabled = false;
        bankText.text = "$" + playerScript.getBalance().ToString();
        betText.text = "BET: $" + 0;
        intBet = 0;
        //checks if the player has any balance left to play with
        if (playerScript.getBalance() == 0)
        {
            mainText.text = "Game Over";
            betButton.gameObject.SetActive(false);
            shoveButton.gameObject.SetActive(false);
        }
    }
    //the function for when the bet button is clicked
    private void BetClicked()
    {
        //if statement to decide how much the bet button adds to the current bet
        if (playerScript.getBalance() < 2000)
        {
            intBet += 50;
        } else if (playerScript.getBalance() < 5000)
        {
            intBet += 100;
        } else if (playerScript.getBalance() < 10000)
        {
            intBet += 500;
        } else
        {
            intBet += 1000;
        }
        //updates balance and bet text
        bankText.text = "$" + playerScript.getBalance().ToString();
        betText.text = "Bet: $" + intBet.ToString();
        //reveals the deal button after a singular bet click
        dealButton.gameObject.SetActive(true);
        //hides the bet button once the player has put in all their money
        if (intBet == playerScript.getBalance()) {
            betButton.gameObject.SetActive(false);
        }
    }

    //the function for when the shove button is clicked
    private void ShoveClicked()
    {
        //updates bet and balance to show that all money has been bet. Then deals the hand
        intBet = playerScript.getBalance();
        bankText.text = "$" + playerScript.getBalance().ToString();
        betText.text = "Bet: $" + intBet.ToString();
        DealClicked();
    }

    private void CountClicked()
    {
        countText.text = DeckScript.cardCount.ToString();
        countText.gameObject.SetActive(true);
    }
}
