using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemiesSettings settings;

    public GameObject deathEffect;
    
    private Transform target;
    
    private int waypointindex = 0;

    public Image healthBar;

    private float currentHp; 
    
    // Start is called before the first frame update
    void Start()
    {
        target = WaypointScript.waypoints[0];
        currentHp = settings.hp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (CheckIfWaypointWasReached())
        {
            if (WaypointScript.EndWasReached(waypointindex))
            {
                PlayerController.getInstance().DecreaseHp(settings.damageToPlayer);
                removeFromWaveAliveList();
                Destroy(gameObject);
            }
            else
            {
                waypointindex++;
                target = WaypointScript.GetTargetWaypoint(waypointindex, target);
            }
            
        }
    }
    
    // void Move();
    public void Move()
    {
        if (!GameController.IsGameEnded)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(  settings.speed * Time.deltaTime * dir.normalized, Space.World);
        }
        
    }

    public  bool CheckIfWaypointWasReached()
    {
        return Vector3.Distance(transform.position, target.position) <= 0.2f;
    }

    public void TakeDamage(int amount)
    {
        if (currentHp > 0)
        {
            currentHp -= amount; ;
            healthBar.fillAmount = currentHp / settings.hp;
        }

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        
        removeFromWaveAliveList();
        
        Destroy(gameObject);
        
        PlayerController.getInstance().RecieveMoney(settings.reward);
    }

    public void removeFromWaveAliveList()
    {
        WaveSpawnerController.getInstance().DecreaseAliveEnemiesCount();
        if (WaveSpawnerController.getInstance().GetAliveEnemies() == 0)
        {
            WaveSpawnerController.getInstance().GetWaveReward();
        }
    }
}
