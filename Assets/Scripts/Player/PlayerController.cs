using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerControllerSettings playerSettings;

    private static PlayerController instance;
    
    public Text currentBalanceText;

    public Text currentHpText;

    private static int currentBalance;

    private static int currentHp;

    private static string dollarSign = "Баланс: $ ";

    private static string hpSign = "Здоровье: ";

    public static PlayerController getInstance()
    {
        return instance;
    }

    public static int CurrentBalance => currentBalance;

    public static int CurrentHp => currentHp;

    public void SpendMoney(int sum)
    {
        currentBalance -= sum;
        UpdateCurrentBalanceOnUI();
    }

    public void RecieveMoney(int sum)
    {
        currentBalance += sum;
        UpdateCurrentBalanceOnUI();
    }

    public void DecreaseHp(int damage)
    {
        if (currentHp > 0)
        {
            currentHp -= damage;
            UpdateHealthOnUI();
        }
    }
    
    void UpdateCurrentBalanceOnUI()
    {
        currentBalanceText.text = dollarSign + currentBalance;
    }

    void UpdateHealthOnUI()
    {
        if (currentHp < 0)
        {
            currentHp = 0;
        }
        currentHpText.text = hpSign + currentHp + "/"+ playerSettings.health;
    }
    
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
        
        currentBalance = playerSettings.startBalance;
        UpdateCurrentBalanceOnUI();
        currentHp = playerSettings.health;
        UpdateHealthOnUI();
    }
    
}
