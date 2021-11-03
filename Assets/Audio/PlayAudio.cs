using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] AudioSource duckAudio;
    [SerializeField] AudioSource gooseAudio;

    [SerializeField] Advanced_EnemyHealth advanced_Enemy;
    [SerializeField] EnemyHealth enemy;
    private void Update()
    {
        if(advanced_Enemy.CurrentHitPoints <= 0 || enemy.CurrentHitPoints <= 0)
        {
            PlayDuckAudio();
        }

        //  If(bossGoose.SetActive(true))
        //  {
        //    gooseAudio.Play()
        //  }
    }

    void PlayDuckAudio()
    {
        duckAudio.Play();
    }

    void PlayerGooseAudio()
    {
        gooseAudio.Play();
    }
}
