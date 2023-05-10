using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button dealButton;
    public Button hitButton;
    public Button standButton;
    public Button doubleButton;
    public Button betButton;
    public Text standButtonText;
    private int standClicks = 0;

    public PlayerScript playerScript;
    public PlayerScript dealerScript;
    void Start()
    {
        dealButton.onClick.AddListener(() => DealClicked());
        hitButton.onClick.AddListener(() => HitClicked());
        standButton.onClick.AddListener(() => StandClicked());
        doubleButton.onClick.AddListener(() => DoubleClicked());
    }

    private void DealClicked()
    {
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
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
}
