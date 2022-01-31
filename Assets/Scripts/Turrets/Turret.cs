using System;
using System.Collections;
using System.Collections.Generic;
using Towers;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject target;

    public TurretSettings settings;

    private string enemyTag = "EnemyTag";

    public Transform partToRotate;

    private float fireCountdown = 0;

    public GameObject bulletPrefab;

    public Transform[] firePoint;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.IsGameEnded)
        {
            if (target != null)
            {
                Rotate();

                if (fireCountdown <= 0)
                {
                    Shoot();
                    fireCountdown = 1f / settings.attackSpeed;
                }

                fireCountdown -= Time.deltaTime;
            }
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject selectedEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToenemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToenemy < shortestDistance)
            {
                shortestDistance = distanceToenemy;
                selectedEnemy = enemy;
            }
        }

        if (selectedEnemy != null && shortestDistance <= settings.range)
        {
            target = selectedEnemy;
        }
        else
        {
            target = null;
        }
    }

    void Rotate()
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * settings.turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        for (int i = 0; i < firePoint.Length; i++)
        {
            GameObject bulletObject = (GameObject) Instantiate(bulletPrefab, firePoint[i].position, firePoint[i].rotation);
            Debug.Log(bulletObject);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetTarget(target.transform);
            }
        }
       

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, settings.range);
    }

    public int GetSellReward()
    {
        return settings.cost / 2;
    }
}
