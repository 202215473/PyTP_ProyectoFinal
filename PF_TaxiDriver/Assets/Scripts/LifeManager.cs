using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LifeManager : DataManager
{
    public int maxLife = 100;
    public int currentLife;
    public LifeBar lifeBar;
    public event Action gameOver;
    [SerializeField] private StateManager stateManager;
    // private ;  Clase a la que me tengo que suscribir

    public override void Start()
    {
        currentLife = maxLife;
        lifeBar.SetMaxLife(maxLife);
        //stateManager = StateManager();    // INSTANCIAR STATE MANAGER
    }

    private void OnEnable()
    {
        // <objeto_al_q_me_subscribo>.<evento> += UpdateLife;
        stateManager.CollisionWithObstacle += HandleCollisionWithObstacle;
        stateManager.CollisionWithWorldLimits += HandleCollisionWithWorldLimits;
    }

    private void OnDisable()
    {
        // <objeto_al_q_me_subscribo>.<evento> -= UpdateLife;
        stateManager.CollisionWithObstacle -= HandleCollisionWithObstacle;
        stateManager.CollisionWithWorldLimits -= HandleCollisionWithWorldLimits;
    }

    public void UpdateLife(int damage)
    {
        if (damage <= currentLife)
        {
            currentLife -= damage;
            lifeBar.SetLife(currentLife);
        }
        else
        {
            currentLife = 0;
            lifeBar.SetLife(currentLife);
            gameOver.Invoke();
        }
    }
    public void HandleCollisionWithObstacle(GameObject gameObject)
    {
        Obstacle obstacle = gameObject.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            int pointsToSubstract = obstacle.GetPointsToSubstract();
            UpdateLife(Mathf.Abs(pointsToSubstract));
        }
    }
    public void HandleCollisionWithWorldLimits() 
    {
        UpdateLife(20);
    }
}
