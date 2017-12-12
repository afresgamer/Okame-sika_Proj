using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_Destroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Okame-sika")
        {
            Destroy(other.gameObject);
        }
    }
}
