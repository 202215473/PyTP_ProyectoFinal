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
    public float currentMoney = 0f;
    public float expectedTip = 50f;
    public MoneyText moneyText;
    [SerializeField] private StateManager stateManager;
    
    public override void Start()
    {
        moneyText.SetMoney(currentMoney);
        //stateManager = Instantiate(StateManager);    // INSTANCIAR STATE MANAGER
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
    public void HandleCollisionWithObstacle(GameObject gameObject)
    {
        //Debug.Log("We have lost money after crashing with " + gameObject.name);
        //Obstacle obstacle = gameObject.GetComponent<Obstacle>();
        //if (obstacle != null)
        //{
        //    float moneyToSubstract = obstacle.GetMoneyToSubstract();
        //    expectedTip -= moneyToSubstract;
        //}
        Debug.Log("We have lost money after crashing with " + gameObject.name);
        Obstacle obstacle = gameObject.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            Debug.Log("Obstacle detected: " + obstacle.name);
            float moneyToSubstract = obstacle.GetMoneyToSubstract();
            expectedTip -= moneyToSubstract;
            Debug.Log("Money deducted: " + moneyToSubstract);
        }
        else
        {
            Debug.LogError("The collided object does not have an Obstacle component.");
        }
    }

}
