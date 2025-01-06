using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public void SetMoney(float money)
    {
        string moneyStr = money.ToString();
        moneyText.text = moneyStr;
    }
}
