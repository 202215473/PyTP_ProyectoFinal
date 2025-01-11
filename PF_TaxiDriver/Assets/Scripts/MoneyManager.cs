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
    [SerializeField] private StateManager stateManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject player;

    private Taxi taxi;

    private float currentMoney = 0f;
    private float initialTip = 50f;
    private float expectedTip;
    public MoneyText moneyText;

    public override void Start()
    {
        expectedTip = initialTip;
        moneyText.SetMoney(currentMoney);
        taxi = player.GetComponent<Taxi>();
    }

    private void OnEnable()
    {
        gameManager.droppedClientAtDestination += clientDropeedOff;
        stateManager.CollisionWithObstacle += HandleCollisionWithObstacle;
    }
    private void OnDisable()
    {
        gameManager.droppedClientAtDestination -= clientDropeedOff;
        stateManager.CollisionWithObstacle -= HandleCollisionWithObstacle;
    }

    public void clientDropeedOff(Client client)
    {
        UpdateMoney(expectedTip);
        expectedTip = initialTip;
    }

    public void UpdateMoney(float money)
    {
        currentMoney += money;
        moneyText.SetMoney(currentMoney);
    }
    public void HandleCollisionWithObstacle(GameObject gameObject)
    {
        Obstacle obstacle = gameObject.GetComponent<Obstacle>();
        if (obstacle != null && taxi.GetIsCarryingClient())
        {
            float moneyToSubstract = obstacle.GetMoneyToSubstract();
            expectedTip += moneyToSubstract;
        }
    }

}
