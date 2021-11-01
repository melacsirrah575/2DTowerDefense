using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;

    int currentBalance;
    public int CurrentBalance {  get { return currentBalance; } }

    private void Awake()
    {
        currentBalance = startingBalance;
    }

    public void Deposit(int amount)
    {
        //Returns absolute value of what is passed in to protect from negative inputs
        currentBalance += Mathf.Abs(amount);
    }

    public void Withdraw(int amount)
    {
        if(currentBalance <= 0)
        {
            currentBalance = 0;
            return;
        }else
        {
            currentBalance -= Mathf.Abs(amount);
        }
    }
}
