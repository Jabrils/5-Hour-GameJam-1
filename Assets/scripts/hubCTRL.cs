using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hubCTRL : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
       if(other.tag == "ENE")
        {
            Destroy(other.gameObject);
            master.TakeDmg();
        }
    }
}
