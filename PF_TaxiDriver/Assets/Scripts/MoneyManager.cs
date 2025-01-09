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

    private StateManager stateManager;

    public override void Start()
    {
        moneyText.SetMoney(currentMoney);
        //stateManager = StateManager();    // INSTANCIAR STATE MANAGER
    }

    private void OnEnable()
    {
        // <objeto_al_q_me_subscribo>.<evento> += UpdateMoney;
        stateManager.CollisionWithObstacle += HandleCollisionWithObstacle;
    }
    private void OnDisable()
    {
        // <objeto_al_q_me_subscribo>.<evento> -= UpdateMoney;
        stateManager.CollisionWithObstacle -= HandleCollisionWithObstacle;
    }

    public void UpdateMoney(float money)
    {
        currentMoney = money;
        moneyText.SetMoney(currentMoney);
    }
    public void HandleCollisionWithObstacle(GameObject @object)
    {
        Obstacle obstacle = @object.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            float moneyToSubstract = obstacle.GetMoneyToSubstract();
            UpdateMoney(currentMoney + moneyToSubstract);
        }
    }
}
