using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoves : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = player.transform.position + new Vector3(0,0, 0);
    }
}
