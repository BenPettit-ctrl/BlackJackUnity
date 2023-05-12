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
    }

    private void DealClicked()
    {
        dealerHandText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
        handText.text = "Hand: " + playerScript.handValue.ToString();
        dealerHandText.text = "Hand: " + playerScript.handValue.ToString();
        dealButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
        standButtonText.text = "Stand";
        pot = 200;
        betText.text = pot.ToString();
        //playerScript.AdjustMoney(-100);
        //bankText.text = playerScript.getBalance().ToString();
    }

    private void HitClicked()
    {
        if (playerScript.GetCard() <=11)
        {
            playerScript.GetCard();
        }
    }

    private void StandClicked()
    {
        
    }

    private void DoubleClicked()
    {
        standClicks++;
        if (standClicks > 1) Debug.Log("End function");
        HitDealer();
        standButtonText.text = "Call";
    }

    private void HitDealer()
    {
        while (dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();

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
        }

    }
}
