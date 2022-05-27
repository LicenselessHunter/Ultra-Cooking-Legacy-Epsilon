using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Area")]
    public Transform attackPos;
	public float attackRange;
	public LayerMask WhatIsEnemies;
    public Transform downAttackPos;
    public float downAttackRange;

    [Header("Particle prefabs")]
    public GameObject GenericParticles;

    [Header("Sound related")]
    public AudioClip[] cuttingSounds;
    AudioSource source;
    
    PlayerController pc;

    void Awake()
    {
        pc = GetComponent<PlayerController>();
        source = GetComponent<AudioSource>();
    }

    public void killEnemies()
    {
		Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, WhatIsEnemies);
	    for	(int i = 0; i < enemiesToDamage.Length; i++)
		{   
            //Get enemy death particles
            GameObject particles;
            try
            {
                particles = enemiesToDamage[i].gameObject.GetComponent<EnemyController>().enemyDeathParticles;
            }
            catch
            {
                particles = null;
                Debug.Log("No particles");
            }
            try
            {
                particles = enemiesToDamage[i].transform.parent.gameObject.GetComponent<EnemyController>().enemyDeathParticles;
            }
            catch
            {
                particles = null;
                Debug.Log("No particles");
            }
            if(particles != null)
            {
                Instantiate(particles, enemiesToDamage[i].transform.position, Quaternion.identity);
            }
            else
            { 
                Instantiate(GenericParticles, enemiesToDamage[i].transform.position, Quaternion.identity);
            }

            //Get & spawn enemy drop
            GameObject drop;
            try
            {
                drop = enemiesToDamage[i].gameObject.GetComponent<EnemyController>().enemyDrop;
            }
            catch
            {
                drop = null;
                Debug.Log("No drop");
            }
            try
            {
                drop = enemiesToDamage[i].transform.parent.gameObject.GetComponent<EnemyController>().enemyDrop;
                Debug.Log(enemiesToDamage[i].transform.parent.gameObject.name);
            }
            catch
            {
                drop = null;
                Debug.Log("No drop");
            }
            if(drop != null)
            {
                Instantiate(drop, enemiesToDamage[i].transform.position, Quaternion.identity);
            }

            Destroy(enemiesToDamage[i].gameObject);
		}
    }
    
    void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackPos.position, attackRange);
        
        Gizmos.DrawWireSphere(downAttackPos.position, downAttackRange);
	}
}