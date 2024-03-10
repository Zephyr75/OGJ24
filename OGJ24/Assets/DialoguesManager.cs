using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialoguesManager : MonoBehaviour
{
    [SerializeField] private bool end;

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
            yield return new WaitForSeconds(7);
            panel.SetActive(false);
        }
        
        if (end)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("terrain");
        }
    }
}
