using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI fruitsText;
    private int fruitsCount;
    
    
    // Start is called before the first frame update
    void Start()
    {
       fruitsText.text = "Fruits: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddFruit()
    {
        fruitsCount++;
        fruitsText.text = "Fruits: " + fruitsCount;
    }
}
