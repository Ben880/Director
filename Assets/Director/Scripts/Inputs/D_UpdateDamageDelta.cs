using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_UpdateDamageDelta : D_InputObject
{
    //================<time, amount>======================================================
    private List<Tuple<float, float>> damageTaken = new List<Tuple<float, float>>();
    private List<Tuple<float, float>> damageHealed = new List<Tuple<float, float>>();
    private float lastHealth;
    private float currentHealth;
    private float currentTime;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        lastHealth =  director.getData().getFloat("Health").value;
        damageTaken.Add(new Tuple<float, float>(0, 0));
        damageHealed.Add(new Tuple<float, float>(0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth =  director.getData().getFloat("Health").value;
        currentTime = director.getData().getFloat("Total Time").value;
        if (lastHealth < currentHealth)
        {
            damageHealed.Add(new Tuple<float, float>(currentTime, currentHealth-lastHealth));
            director.getData().getFloat("Total Healed").value += currentHealth-lastHealth;
        }
        if (lastHealth > currentHealth)
        {
            damageTaken.Add(new Tuple<float, float>(currentTime, currentHealth-lastHealth));
            director.getData().getFloat("Total Damage").value += currentHealth-lastHealth;
        }
        lastHealth = currentHealth;
        director.getData().getFloat("Last Damage").value = currentTime - damageTaken[damageTaken.Count - 1].Item1;
        director.getData().getFloat("Last Healed").value = currentTime - damageHealed[damageHealed.Count - 1].Item1;
        
    }

     
    
}
