using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player2Account : MonoBehaviour
{
    //This Script for add money in Player1
    public int balance = 0;
    public TextMeshProUGUI balanceDesplay;

    public void AddMoney(int amount)
    {
        balance += amount;
        Debug.Log("Money added! Current Balance: " + balance);

        balanceDesplay.text = balance.ToString();
    }
}
