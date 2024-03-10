using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField] private int damage;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.tag.Equals("Enemy") && player.GetComponent<Player>().isAttacking)
       {
           other.GetComponent<Ennemies>().TakeDamage(damage);
       }
    }
}
