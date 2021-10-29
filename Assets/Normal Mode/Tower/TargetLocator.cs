using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] GameObject weapon; //Object that Looks at Enemy
    [SerializeField] private float shootDistance;
    public bool targeting = true;

    ParticleSystem[] guns;

    GameObject target;

    void Start()
    {
        guns = weapon.GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && Vector3.Distance(target.transform.position, transform.position) > shootDistance)
        {
            target = null;
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) <= shootDistance)
            {
                if (target == null || enemy.GetComponent<EnemyMover>().GetCurrentWaypoint() > target.GetComponent<EnemyMover>().GetCurrentWaypoint())
                {
                    target = enemy;
                } 
            }
        }
        Shoot();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, shootDistance);
    }

    private void Shoot()
    {
        if (target != null && Vector3.Distance(target.transform.position, transform.position) <= shootDistance)
        {
            if (targeting)
            {
                AimWeapon();
            }
            else
            {
                EnableShooting();
            }
        }
        else
        {
            DisableShooting();
        }
    }

    private void AimWeapon()
    {
        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        EnableShooting();

        //weapon.transform.right = new Vector3(weapon.transform.position.x - target.transform.position.x * 1000000, weapon.transform.position.y - target.transform.position.y * 1000000, weapon.transform.position.z);
    }

    private void EnableShooting()
    {
        foreach (ParticleSystem particleSystem in guns)
        {
            particleSystem.enableEmission = true;
        }
    }

    private void DisableShooting()
    {
        foreach (ParticleSystem particleSystem in guns)
        {
            particleSystem.enableEmission = false;
        }
    }
}
