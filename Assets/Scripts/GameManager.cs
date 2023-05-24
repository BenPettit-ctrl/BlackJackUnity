using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Button dealButton;
    public Button hitButton;
    public Button standButton;
    public Button doubleButton;
    public Button betButton;
    public Button shoveButton;
    private int intBet = 0;

    public PlayerScript playerScript;
    public PlayerScript dealerScript;

    public TextMeshProUGUI standButtonText;
    public TextMeshProUGUI bankText;
    public TextMeshProUGUI handText;
    public TextMeshProUGUI betText;
    public TextMeshProUGUI dealerHandText;
    public TextMeshProUGUI mainText;

    public GameObject hideCard;

    void Start()
    {
        dealButton.onClick.AddListener(() => DealClicked());
        hitButton.onClick.AddListener(() => HitClicked());
        standButton.onClick.AddListener(() => StandClicked());
        doubleButton.onClick.AddListener(() => DoubleClicked());
        betButton.onClick.AddListener(() => BetClicked());
        shoveButton.onClick.AddListener(() => ShoveClicked());
        standButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(false);
        doubleButton.gameObject.SetActive(false);
        dealButton.gameObject.SetActive(false);
    }

    private void DealClicked()
    {
        playerScript.adjustMoney(-intBet);
        playerScript.ResetHand();
        dealerScript.ResetHand();
        mainText.gameObject.SetActive(false);
        dealerHandText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
        handText.text = "Hand: " + playerScript.handValue.ToString();
        dealerHandText.text = "Hand: " + dealerScript.handValue.ToString();
        hideCard.GetComponent<Renderer>().enabled = true;
        dealButton.gameObject.SetActive(false);
        betButton.gameObject.SetActive(false);
        shoveButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
        doubleButton.gameObject.SetActive(true);
        standButtonText.text = "Stand";
        bankText.text = "$" + playerScript.getBalance().ToString();
        if (intBet <= playerScript.getBalance())
        {
            doubleButton.gameObject.SetActive(true);
        } else
        {
            doubleButton.gameObject.SetActive(false);
        }
        if (dealerScript.handValue == 21 || playerScript.handValue == 21)
        {
            RoundOver();
        }
    }

    private void HitClicked()
    {
        doubleButton.gameObject.SetActive(false);
        if (playerScript.cardIndex <=11)
        {
            playerScript.GetCard();
            handText.text = "Hand: " + playerScript.handValue.ToString();
            if (playerScript.handValue > 20)
            {
                RoundOver();
            }
        }
    }

    private void StandClicked()
    {
        doubleButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        for (int i = 0; i < 11; i++)
        {
            HitDealer();
        }
        RoundOver();
    }

    private void DoubleClicked()
    {
        doubleButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        playerScript.adjustMoney(-intBet);
        intBet += intBet;
        playerScript.GetCard();
        betText.text = "BET: $" + intBet;
        bankText.text = "$" + playerScript.getBalance();
        handText.text = "Hand: " + playerScript.handValue.ToString();
        StandClicked();
    }

    private void HitDealer()
    {
        if (dealerScript.handValue < 16 && dealerScript.cardIndex < 11)
        {
            dealerScript.GetCard();
            dealerHandText.text = "Hand: " + dealerScript.handValue.ToString();
        }
    }

    void RoundOver()
    {
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;
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
        if (playerScript.getBalance() == 0)
        {
            mainText.text = "Game Over";
            betButton.gameObject.SetActive(false);
            shoveButton.gameObject.SetActive(false);
        }
    }

    private void BetClicked()
    {
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
        bankText.text = "$" + playerScript.getBalance().ToString();
        betText.text = "Bet: $" + intBet.ToString();
        dealButton.gameObject.SetActive(true);
        if (intBet == playerScript.getBalance()) {
            betButton.gameObject.SetActive(false);
        }
    }

    private void ShoveClicked()
    {
        intBet = playerScript.getBalance();
        bankText.text = "$" + playerScript.getBalance().ToString();
        betText.text = "Bet: $" + intBet.ToString();
        DealClicked();
    }
}
