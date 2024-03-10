using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Orc : Ennemies
{
     [SerializeField] private Animator anim;
     private float cooldownTime = 2f;
     private float lastUsedTime; 
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        Debug.Log(ennemy.velocity.magnitude);

        anim.SetFloat("Speed", ennemy.velocity.magnitude);
        
        if (Vector3.Distance(player.transform.position, transform.position) < 2 && Time.time > lastUsedTime + cooldownTime)
        {
            anim.SetTrigger("Attack");
            lastUsedTime = Time.time;
        }

    }

}
