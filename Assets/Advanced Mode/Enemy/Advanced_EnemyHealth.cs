using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Advanced_Enemy))]
public class Advanced_EnemyHealth : MonoBehaviour
{
    [Tooltip("Adds amount to maxHitPoints when enemy dies")]
    [SerializeField] int difficultyRamp = 1;

    [SerializeField] int maxHitPoints = 5;
    int currentHitPoints = 0;

    Advanced_Enemy enemy;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void Start()
    {
        enemy = GetComponent<Advanced_Enemy>();
    }

    //On Collision
    //ProcessHit();
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }
    void ProcessHit()
    {
        currentHitPoints--;

        if(currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
    }
}
