using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_Destroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Okame-sika")
        {
            if(other.gameObject.name == "ue" || other.gameObject.name == "pCylinder360" || other.gameObject.name == "AttachPoint")
            { Destroy(other.gameObject); }
            Destroy(other.gameObject);
            GameController.instance.RemoveMemory(other.gameObject);
        }
    }
}
