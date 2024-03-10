using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemies : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent ennemy;
    [SerializeField] private GameObject player;

    private void Start()
    {
        ennemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ennemy.destination = player.transform.position;
    }
}
