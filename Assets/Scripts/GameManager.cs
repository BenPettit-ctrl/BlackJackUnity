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
    }

    private void DealClicked()
    {
        playerScript.ResetHand();
        dealerScript.ResetHand();
        mainText.gameObject.SetActive(false);
        dealerHandText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
        handText.text = "Hand: " + playerScript.handValue.ToString();
        dealerHandText.text = "Hand: " + playerScript.handValue.ToString();
        hideCard.GetComponent<Renderer>().enabled = true;
        dealButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
        standButtonText.text = "Stand";
        pot = 200;
        betText.text = "$" + pot.ToString();
        playerScript.adjustMoney(-100);
        bankText.text = "$" + playerScript.getBalance().ToString();
    }

    private void HitClicked()
    {
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
        standClicks++;
        if (standClicks > 1) RoundOver();
        HitDealer();
        standButtonText.text = "Call";
    }

    private void DoubleClicked()
    {
        
    }

    private void HitDealer()
    {
        while (dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
            dealerHandText.text = "Hand: " + dealerScript.handValue.ToString();
            if (dealerScript.handValue > 20) RoundOver();
        }
    }

    void RoundOver()
    {
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = playerScript.handValue == 21;
        if (standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21)
        {
            return;
        }
        bool roundOver = true;
        if (playerBust && dealerBust)
        {
            mainText.text = "ALL BUST: BETS RETURNED";
            playerScript.adjustMoney(pot / 2);
        } else if (playerBust || (!dealerBust && dealerScript.handValue > playerScript.handValue)) {
            mainText.text = "DEALER WINS!";
        } else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            mainText.text = "YOU WIN!";
            playerScript.adjustMoney(pot);
        } else if (playerScript.handValue == dealerScript.handValue)
        {
            mainText.text = "PUSH: BETS RETURNED";
            playerScript.adjustMoney(pot / 2);
        } else
        {
            roundOver = false;
        }
        if (roundOver)
        {
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            dealButton.gameObject.SetActive(true);
            doubleButton.gameObject.SetActive(false);
            mainText.gameObject.SetActive(true);
            dealerHandText.gameObject.SetActive(true);
            hideCard.GetComponent<Renderer>().enabled = false;
            bankText.text = "$" + playerScript.getBalance().ToString();
            standClicks = 0;
        }
    }

    private void BetClicked()
    {
        Text newBet = betButton.GetComponentInChildren(typeof(Text)) as Text;
        int intBet = int.Parse(newBet.text.ToString().Remove(0, 1));
        playerScript.adjustMoney(-intBet);
        bankText.text = "$" + playerScript.getBalance().ToString();
        pot += (intBet * 2);
        betText.text = "Bet: $" + pot.ToString();
    }
}
