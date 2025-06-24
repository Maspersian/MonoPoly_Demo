using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceNumber : MonoBehaviour
{
    public int randomNumber; // The random number assigned to the dice face
    public GameObject[] diceFaces; // Array of dice face GameObjects    
    public Animator diceAnimator; // Animator for the dice
    public static DiceNumber Instance; // Singleton instance
    public Transform[] wayPoint; // Transform for the waypoint
    public int currentWaypointIndexRed = 0; // Index of the current waypoint
    public int currentWaypointIndexBlue = 0;
    public GameObject player1; // Reference to the player GameObject
    public GameObject player2;
    public bool redTrue;
    public int chekingJailRed=0;
    public int chekingJailBlue=0;

    public Button diceRollButton; // Assign this in the Inspector

    private bool redStartedLap = false;
    private bool blueStartedLap = false;

    public GameObject winPanel; // assign a UI panel in Inspector
    public TMPro.TextMeshProUGUI winText; // assign a TMP Text object in the panel
    //public bool blueTrue;

    void Awake()
    {
        Instance = this;
        player1.transform.position = wayPoint[0].transform.position;
        player2.transform.position = wayPoint[0].transform.position;

        WayPointValue wpValue = wayPoint[0].GetComponent<WayPointValue>();
        if (wpValue != null)
        {
            player1.GetComponent<Player1Account>().AddMoney(2000);
            player2.GetComponent<Player2Account>().AddMoney(2000);
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetRandomNumber()
    {
        diceRollButton.interactable = false; // Disable the button

        randomNumber = Random.Range(1, 7); // Generate a random number between 1 and 6
        if (randomNumber == 1)
        {
            diceAnimator.SetBool("DiceAnimation1", true); // Trigger the animation for the first face
        }
        else if (randomNumber == 2)
        {
            diceAnimator.SetBool("DiceAnimation2", true); // Trigger the animation for the second face
        }
        else if (randomNumber == 3)
        {
            diceAnimator.SetBool("DiceAnimation3", true); // Trigger the animation for the third face
        }
        else if (randomNumber == 4)
        {
            diceAnimator.SetBool("DiceAnimation4", true); // Trigger the animation for the fourth face
        }
        else if (randomNumber == 5)
        {
            diceAnimator.SetBool("DiceAnimation5", true); // Trigger the animation for the fifth face
        }
        else
        {
            diceAnimator.SetBool("DiceAnimation6", true); // Trigger the animation for the sixth face
        }
        if (randomNumber <= diceFaces.Length)
        {
            for (int i = 0; i < diceFaces.Length; i++)
            {
                diceFaces[i].SetActive(false); // Deactivate all dice faces
            }
            diceFaces[randomNumber-1 ].SetActive(true); // Activate the corresponding dice face
        }
        StartCoroutine(WaitForAnimationAndMove()); // Wait for animation, then move
        //PlayerMovement(); // Call the PlayerMovement method to handle player movement based on the dice roll

    }
    IEnumerator WaitForAnimationAndMove()
    {
        yield return new WaitForSeconds(1f); // adjust this time to match your animation length
        PlayerMovement(); // Now move player after animation finishes
        diceRollButton.interactable = true; // Re-enable button
    }
    public void PlayerMovement()
    {
        if (randomNumber != 6)
        {
            redTrue = !redTrue; // Flip turn
        }
        else
        {
            Debug.Log("Player rolled a 6! Gets another turn.");
        }


        if (redTrue == true)
        {
            Debug.Log("Its Red Turn");
            if (chekingJailRed > 0)
            {
                chekingJailRed += 1;
                if (chekingJailRed == 4)
                {
                    chekingJailRed = 0;
                }
            }
            else
            {
                int previousIndexRed = currentWaypointIndexRed;
                currentWaypointIndexRed += randomNumber;

                if (currentWaypointIndexRed >= wayPoint.Length)
                {
                    currentWaypointIndexRed -= wayPoint.Length;

                    // Completed a round, passed starting point
                    if (redStartedLap)
                    {
                        player1.GetComponent<Player1Account>().AddMoney(200); // Bonus
                        Debug.Log("Red completed a round: +200 points");
                    }

                    redStartedLap = true;
                }
                player1.transform.position = wayPoint[currentWaypointIndexRed].transform.position;// Move the player towards the current waypoint

                //Add WayPoint Value to Player Account

                WayPointValue wpValue = wayPoint[currentWaypointIndexRed].GetComponent<WayPointValue>();
                if (wpValue != null)
                {
                    //player1.GetComponent<Player1Account>().AddMoney(wpValue.value);
                    if (wpValue.owner == 0)
                    {
                        wpValue.ShowPanel(1); // Show to Player1
                    }
                    else if (wpValue.owner == 2)
                    {
                        var p1 = player1.GetComponent<Player1Account>();
                        /*if (p1.balance >= wpValue.rentAmount)
                        {*/
                            p1.AddMoney(-wpValue.rentAmount);
                            player2.GetComponent<Player2Account>().AddMoney(wpValue.rentAmount);
                        Debug.Log("Blue Rent Amount Adding");
                        //}
                    }

                    if (wpValue.value == 500)
                    {
                        chekingJailRed += 1;
                    }
                }
            }
            
        }
        else
        {
            if (chekingJailBlue > 0)
            {
                chekingJailBlue += 1;
                if (chekingJailBlue == 4)
                {
                    chekingJailBlue = 0;
                }
            }
            else
            {
                int previousIndexBlue = currentWaypointIndexBlue;
                currentWaypointIndexBlue += randomNumber;

                if (currentWaypointIndexBlue >= wayPoint.Length)
                {
                    currentWaypointIndexBlue -= wayPoint.Length;

                    if (blueStartedLap)
                    {
                        player2.GetComponent<Player2Account>().AddMoney(200);
                        Debug.Log("Blue completed a round: +200 points");
                    }

                    blueStartedLap = true;
                }

                player2.transform.position = wayPoint[currentWaypointIndexBlue].transform.position;

                //Add WayPoint Value to Player Account

                WayPointValue wpValue = wayPoint[currentWaypointIndexBlue].GetComponent<WayPointValue>();
                if (wpValue != null)
                {
                    //player2.GetComponent<Player2Account>().AddMoney(wpValue.value);
                    if (wpValue.owner == 0)
                    {
                        wpValue.ShowPanel(2); // Show to Player1
                    }
                    else if (wpValue.owner == 1)
                    {
                        var p2 = player2.GetComponent<Player2Account>();
                        /*if (p2.balance >= wpValue.rentAmount)
                        {*/
                            p2.AddMoney(-wpValue.rentAmount);
                            player1.GetComponent<Player1Account>().AddMoney(wpValue.rentAmount);
                        Debug.Log("Red Rent Amount Adding");

                        // }
                    }

                    if (wpValue.value == 500)
                    {
                        chekingJailBlue += 1;
                    }
                }
            }
           
        }
    }
   

    public void CheckGameOver()
    {
        int p1Balance = player1.GetComponent<Player1Account>().balance;
        int p2Balance = player2.GetComponent<Player2Account>().balance;

        if (p1Balance <= 0)
        {
            ShowWin("Blue Player Wins!");
            Debug.Log("GameOver");
        }
        else if (p2Balance <= 0)
        {
            ShowWin("Red Player Wins!");
            Debug.Log("GameOver");
        }
    }

    void ShowWin(string winnerMessage)
    {
        winPanel.SetActive(true);
        winText.text = winnerMessage;
        diceRollButton.interactable = false; // Stop the game
    }
}
