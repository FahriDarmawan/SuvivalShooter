﻿using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
    {
        //mencari game object dgn tag player
        player = GameObject.FindGameObjectWithTag ("Player");

        //mendapatkan komponen player health
        playerHealth = player.GetComponent <PlayerHealth> ();
        
        //mendapatkan enemy health
        enemyHealth = GetComponent<EnemyHealth>();

        //mendapatkan komponen animator
        anim = GetComponent <Animator> ();
    }

    //callback jika ada suatu object masuk kedalam trigger
    void OnTriggerEnter (Collider other)
    {
        //set player in range
        if(other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;
        }
    }

    //callback jika ada object keluar dari trigger
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        //mentrigger animasi playerdead jika darah player kurang dari sama dengan 0
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        //reset timer
        timer = 0f;

        //taking damage
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
