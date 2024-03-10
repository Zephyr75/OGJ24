using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemies : MonoBehaviour
{
    // Start is called before the first frame update
    protected NavMeshAgent ennemy;
    protected GameObject player;
    [SerializeField] private int hp;


    protected void Start()
    {
        ennemy = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    protected void Update()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < 2)
        {
            ennemy.destination = ennemy.transform.position;
        }
        else
        {
            ennemy.destination = player.transform.position;
        }
    }
    
    public void TakeDamage(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}