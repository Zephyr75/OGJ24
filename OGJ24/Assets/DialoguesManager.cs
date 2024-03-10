using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguesManager : MonoBehaviour
{

    public List<GameObject> panels;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Dialogue());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator Dialogue()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(true);
            yield return new WaitForSeconds(3);
            panel.SetActive(false);
        }
    }
}
