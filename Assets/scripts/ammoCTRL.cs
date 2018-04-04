using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoCTRL : MonoBehaviour {
    public Vector3 target;

    float lerper;
    Vector3 start;

	// Use this for initialization
	void Start () {
        start = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        lerper += Time.deltaTime;
        transform.position = Vector3.Lerp(start, target, lerper/master.ammoSpd);

        if(lerper / 1 > 1)
        {
            master.moneys += 25;
            master.UpdateMoneys();
            Destroy(gameObject);
        }
	}
}
