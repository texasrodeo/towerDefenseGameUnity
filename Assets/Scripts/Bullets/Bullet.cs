using System.Collections;
using System.Collections.Generic;
using Bullets;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public BulletSettings settings;

    public GameObject explodeEffect;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Fly();
        }
    }

    void Fly()
    {
        if (!GameController.IsGameEnded)
        {
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = settings.speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }
        
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.LookAt(target);
        }
      
    }

    void HitTarget()
    {
        if (explodeEffect != null)
        {
            GameObject effectIns = (GameObject)Instantiate(explodeEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
        }
        if (settings.explosionRange > 0)
        {
            DamageInAOE();
        }
        else
        {
            DamageSingleTarget(target);
        }
        Destroy(gameObject);
    }

    void DamageSingleTarget(Transform enemy)
    {
        enemy.gameObject.GetComponent<Enemy>().TakeDamage(settings.damage);
    }

    void DamageInAOE()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, settings.explosionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "EnemyTag")
            {
                DamageSingleTarget(collider.transform);
            }
        }
    }
    
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, settings.explosionRange);
    }
}
