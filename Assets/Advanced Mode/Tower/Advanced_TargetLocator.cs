using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advanced_TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon; //Object that Looks at Enemy
    [SerializeField] float range = 3;
    [SerializeField] ParticleSystem[] guns;

    Transform target;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        Advanced_Enemy[] enemies = FindObjectsOfType<Advanced_Enemy>();
        Transform closestTarget = null;
        //Furthest distance an enemy will be detected. Initialized to infinity to always give a result
        float maxDistance = Mathf.Infinity;

        foreach (Advanced_Enemy enemy in enemies)
        {
            //Finds distance between tower and current enemy looked at
            float targetDistance = Vector2.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }

    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        if(targetDistance < range)
        {
            //LookAt function for 2D
            Vector3 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Attack(true);
        } else
        {
            Attack(false);
        }
    }

    private void Attack(bool isActive)
    {
        foreach (ParticleSystem particleSystem in guns)
        {
            var emissionModule = particleSystem.emission;
            emissionModule.enabled = isActive;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
