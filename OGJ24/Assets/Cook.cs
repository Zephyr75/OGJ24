using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cook : MonoBehaviour
{
    
    [SerializeField] private GameObject cookText;
    
    private GameObject gm;
    
    // Start is called before the first frame update
    void Start()
    {
        
        gm = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && gm.GetComponent<GameManager>().GetFruitCount() >= 4)
        {
            cookText.SetActive(true);
            other.GetComponent<Player>().canCook = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            cookText.SetActive(false);
            other.GetComponent<Player>().canCook = false;
        }
    }
}
