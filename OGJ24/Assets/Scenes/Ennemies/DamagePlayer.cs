using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private int damage;
    
    private Ennemies enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        // find parent with <Ennemies> component
        GameObject parent = transform.parent.gameObject;
        while (enemy == null)
        {
            enemy = parent.GetComponent<Ennemies>();
            parent = parent.transform.parent.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.tag.Equals("Player") && enemy.isAttacking)
       {
           other.GetComponent<Player>().TakeDamage(damage);
       }
    }
}
