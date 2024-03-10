using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI fruitsText;
    private int fruitsCount;
    private float loopCountdown = 8 * 60;
    
    
    // Start is called before the first frame update
    void Start()
    {
       fruitsText.text = "Ingrédients: " + fruitsCount + "/4";
    }

    // Update is called once per frame
    void Update()
    {
        loopCountdown -= Time.deltaTime;
        if (loopCountdown <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("terrain");
        }
        
    }
    
    public void AddFruit()
    {
        fruitsCount++;
        fruitsText.text = "Ingrédients: " + fruitsCount + "/4";
        if (fruitsCount == 4)
        {
            fruitsText.text = "Paré à cuisinier, vous trouverez le nécessaire sur votre route.";
        }
    }
    
    public int GetFruitCount()
    {
        return fruitsCount;
    }
}
