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
    private int standClicks = 0;
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

    int pot = 0;

    void Start()
    {
        dealButton.onClick.AddListener(() => DealClicked());
        hitButton.onClick.AddListener(() => HitClicked());
        standButton.onClick.AddListener(() => StandClicked());
        doubleButton.onClick.AddListener(() => DoubleClicked());
        betButton.onClick.AddListener(() => BetClicked());
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
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
        doubleButton.gameObject.SetActive(true);
        standButtonText.text = "Stand";
        //betText.text = "$" + intBet.ToString();
        bankText.text = "$" + playerScript.getBalance().ToString();
        if (intBet*2 <= playerScript.getBalance())
        {
            doubleButton.gameObject.SetActive(true);
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
            playerScript.adjustMoney((intBet * 3) / 2);
            mainText.text = "BlackJack";
        } else if (!playerBust && (playerScript.handValue > dealerScript.handValue || dealerBust))
        {
            playerScript.adjustMoney(intBet * 2);
            mainText.text = "Player Wins";
        } else
        {
            mainText.text = "Dealer Wins";
        }
        /*if (!playerBust && !dealerBust && !player21 && !dealer21)
        {
            return;
        }
        bool roundOver = true;
        if (playerBust && dealerBust)
        {
            mainText.text = "ALL BUST: BETS RETURNED";
            playerScript.adjustMoney(intBet);
        } else if (player21)
        {
            playerScript.adjustMoney((intBet * 3)/2);
            mainText.text = "BlackJack";
        } else if (playerBust || (!dealerBust && dealerScript.handValue > playerScript.handValue)) 
        {
            mainText.text = "DEALER WINS!";
            playerScript.adjustMoney(-intBet);
        } else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            mainText.text = "YOU WIN!";
            playerScript.adjustMoney(intBet*2);
        } else if (playerScript.handValue == dealerScript.handValue)
        {
            mainText.text = "PUSH: BETS RETURNED";
            playerScript.adjustMoney(intBet);
        }*/
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        doubleButton.gameObject.SetActive(false);
        betButton.gameObject.SetActive(true);
        mainText.gameObject.SetActive(true);
        dealerHandText.gameObject.SetActive(true);
        hideCard.GetComponent<Renderer>().enabled = false;
        bankText.text = "$" + playerScript.getBalance().ToString();
        betText.text = "BET: $" + 0;
        intBet = 0;
    }

    private void BetClicked()
    {
        Text newBet = betButton.GetComponentInChildren(typeof(Text)) as Text;
        //int intBet = int.Parse(newBet.text.ToString().Remove(0, 1));
        intBet += 50;
        bankText.text = "$" + playerScript.getBalance().ToString();
        betText.text = "Bet: $" + intBet.ToString();
        dealButton.gameObject.SetActive(true);
    }
}
