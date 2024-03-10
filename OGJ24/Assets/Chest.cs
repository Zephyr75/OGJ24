using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Transform couvercle;
    [SerializeField] private Transform fruit;
    public bool open;
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
           if (other.tag.Equals("Player") && open)
           {
               other.GetComponent<Player>().OpenChest();
               other.transform.position = transform.position + new Vector3(0, 0, 1);
               other.transform.rotation = transform.rotation;
               StartCoroutine(OpenCouvercle());
               open = false;
               gm.GetComponent<GameManager>().AddFruit();
           }
        }

    private IEnumerator OpenCouvercle()
    {
        yield return new WaitForSeconds(1.0f);
        float time = 0;
        while (time < 1)
        {
              time += Time.deltaTime;
              couvercle.transform.Rotate(0, 0, -90 * Time.deltaTime);
              yield return null;
        }
        time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            fruit.transform.position += new Vector3(0, 3f * Time.deltaTime, 0);
            // grow fruit
            fruit.transform.localScale += new Vector3(0.4f * Time.deltaTime, 0.4f * Time.deltaTime, 0.4f * Time.deltaTime);
            yield return null;
        }
        
        yield return new WaitForSeconds(1.0f);
        Destroy(fruit.gameObject);
    }
}
