using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagPlayer : MonoBehaviour
{
    public float damageAmount = 1;
    public float damageTimer = 1;
    private float timmerCounter = 0;
    private GameObject Player = null;
    private vp_FPPlayerDamageHandler damageHandler = null;
    private bool playerEntered = false;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        damageHandler = Player.GetComponent<vp_FPPlayerDamageHandler>();
        timmerCounter = damageTimer;
    }
    void Update()
    {
        if (playerEntered)
        {
            timmerCounter += Time.deltaTime;
        }
        if (timmerCounter > damageTimer)
        {
            damageHandler.Damage(damageAmount);
            timmerCounter = 0;
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerEntered = true;
        }
           
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerEntered = false;
            timmerCounter = damageTimer;
        }
    }

}
