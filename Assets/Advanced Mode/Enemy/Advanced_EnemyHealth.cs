using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Advanced_Enemy))]
public class Advanced_EnemyHealth : MonoBehaviour
{
    [Tooltip("Adds amount to maxHitPoints when enemy dies")]
    [SerializeField] int difficultyRamp = 1;

    [SerializeField] int maxHitPoints = 5;
    int currentHitPoints = 1;
    public int CurrentHitPoints { get { return currentHitPoints; } }

    Advanced_Enemy enemy;
    public AudioClip audioClip;

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
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
    }
}
