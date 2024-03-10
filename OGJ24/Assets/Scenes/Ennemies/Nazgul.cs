using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Nazgul : Ennemies
{
    
     [SerializeField] private Animator anim;
     [SerializeField] private GameObject spin;
     [SerializeField] private GameObject massue;
     private float cooldownTime = 4f;
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

        anim.SetFloat("Speed", ennemy.velocity.magnitude);
        
        if (Vector3.Distance(player.transform.position, transform.position) < 2 && Time.time > lastUsedTime + cooldownTime)
        {
            StartCoroutine(Spin());
            lastUsedTime = Time.time;
        }

    }

    IEnumerator Spin()
    {
        spin.SetActive(true);
        massue.SetActive(false);
        float time = 0;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            spin.transform.Rotate(0, 720 * Time.deltaTime, 0);
            yield return null;
        }
        spin.SetActive(false);
        massue.SetActive(true);
    }


}
