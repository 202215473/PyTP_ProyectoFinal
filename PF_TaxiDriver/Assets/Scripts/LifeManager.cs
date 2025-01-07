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
    // private ;  Clase a la que me tengo que suscribir

    public override void Start()
    {
        currentLife = maxLife;
        lifeBar.SetMaxLife(maxLife);
    }

    private void OnEnable()
    {
        // <objeto_al_q_me_subscribo>.<evento> += UpdateLife;
    }

    private void OnDisable()
    {
        // <objeto_al_q_me_subscribo>.<evento> -= UpdateLife;
    }

    public void UpdateLife(int damage)
    {
        if (damage >= currentLife)
        {
            currentLife -= damage;
            lifeBar.SetLife(currentLife);
        }
        else
        {
            gameOver.Invoke();
        }
    }
}
