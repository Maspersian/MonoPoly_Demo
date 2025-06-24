using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WayPointValue : MonoBehaviour
{
    public int value; // Base value of the property
    public GameObject infoPanel; // UI panel for this waypoint
    public int buyCost = 200;
    public int rentAmount = 50;
    public int owner = 0; // 0 = unowned, 1 = player1, 2 = player2
    public TextMeshProUGUI[] textForDisplay;
    public GameObject buyButton;
    public GameObject redOwner;
    public GameObject blueOwner;

    private int currentPlayerTryingToBuy = 0;
    public void ShowPanel(int playerId)
    {
        infoPanel.SetActive(true);
        if (buyButton != null)
            buyButton.SetActive(true); // ensure button is turned on too
        DisplayText();
        currentPlayerTryingToBuy = playerId;

        // Show buy button only if unowned and the player can afford
        Player1Account p1 = DiceNumber.Instance.player1.GetComponent<Player1Account>();
        Player2Account p2 = DiceNumber.Instance.player2.GetComponent<Player2Account>();
        bool canAfford = (playerId == 1 && p1.balance >= buyCost) || (playerId == 2 && p2.balance >= buyCost);

        buyButton.SetActive(owner == 0 && canAfford);

        StartCoroutine(HidePanelAfterDelay(4f)); // Hide after 3 seconds
    }

    public void HidePanel()
    {
        infoPanel.SetActive(false);
        Debug.Log("Show Panel Hide");
    }

    /*public void BuyProperty(int playerId)
    {
        owner = playerId;
        HidePanel();
    }*/
    public void DisplayText()
    {
        //textForDisplay[0].text = value.ToString();
        textForDisplay[1].text = buyCost.ToString();
        textForDisplay[2].text = rentAmount.ToString();
        textForDisplay[3].text = owner == 0 ? "Unowned" : (owner == 1 ? "Player 1" : "Player 2");

    }
    public void OnBuyButtonClick()
    {
        if (owner == 0)
        {
            if (currentPlayerTryingToBuy == 1)
            {
                var p1 = DiceNumber.Instance.player1.GetComponent<Player1Account>();
                if (p1.balance >= buyCost)
                {
                    p1.AddMoney(-buyCost);
                    owner = 1;
                    redOwner.SetActive(true);
                }
            }
            else if (currentPlayerTryingToBuy == 2)
            {
                var p2 = DiceNumber.Instance.player2.GetComponent<Player2Account>();
                if (p2.balance >= buyCost)
                {
                    p2.AddMoney(-buyCost);
                    owner = 2;
                    blueOwner.SetActive(true);
                }
            }
        }

        HidePanel();
    }
    IEnumerator HidePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Only hide if still visible and not bought yet
        if (infoPanel.activeSelf)
        {
            HidePanel();
        }
    }


}
