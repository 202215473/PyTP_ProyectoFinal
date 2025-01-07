using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : DataManager
{
    public float currentMoney = 0;
    public MoneyText moneyText;

    public override void Start()
    {
        moneyText.SetMoney(currentMoney);
    }

    private void OnEnable()
    {
        // <objeto_al_q_me_subscribo>.<evento> += UpdateMoney;
    }

    private void OnDisable()
    {
        // <objeto_al_q_me_subscribo>.<evento> -= UpdateMoney;
    }

    public void UpdateMoney(float money)
    {
        currentMoney = money;
        moneyText.SetMoney(currentMoney);
    }
}
