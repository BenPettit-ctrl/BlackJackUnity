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

    PlayerScript playerScript;
    PlayerScript dealerScript;
    void Start()
    {
        dealButton.onClick.AddListener(() => DealClicked());
        hitButton.onClick.AddListener(() => HitClicked());
        standButton.onClick.AddListener(() => StandClicked());
        doubleButton.onClick.AddListener(() => DoubleClicked());
    }

    private void DealClicked()
    {
        playerScript.StartHand();
    }

    private void HitClicked()
    {
        
    }

    private void StandClicked()
    {
        
    }

    private void DoubleClicked()
    {
        
    }
}
