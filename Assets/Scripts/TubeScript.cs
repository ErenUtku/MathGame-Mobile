using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeScript : MonoBehaviour
{
    public int tubeId;  

    void OnTriggerEnter2D(Collider2D col)
    {
     
        if (col.CompareTag("Player"))
        {
            GameManager.instance.OnPlayerEnterTube(tubeId);
        }
    }
}
