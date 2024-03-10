using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroupEnemies : MonoBehaviour
{
    public List<Ennemies> _ennemiesList;
    //TODO put Serializable chest

    private int count;
    // Start is called before the first frame update
    void Start()
    {
        count = _ennemiesList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        count = countEnemies();
        if (count == 0)
        {
            //TODO open the chest
        }
    }

    private int countEnemies()
    {
        int c = 0;
        foreach (var e in _ennemiesList)
        {
            if (!e.IsUnityNull())
            {
                c++;
            }
        }
        return c;
    }
}
