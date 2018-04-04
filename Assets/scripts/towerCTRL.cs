using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerCTRL : MonoBehaviour {
    public GameObject target;
    public bool ifEne;
    public bool hasAmmo = true;
    public float reloadTime = 2;
    public float range =20;

	// Update is called once per frame
	void Update ()
    {
        CheckForEne();

        if(ifEne && hasAmmo && InRange())
        {
            ShootEnemy();
            StartCoroutine(Reloader());
        }
    }

    bool InRange()
    {
        if(Vector3.Distance(gameObject.transform.position,target.transform.position) < range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ShootEnemy()
    {
        GameObject ammo = master.CreateEntity("ammo");
        ammo.transform.position = transform.position + (Vector3.up*5.5f);
        ammo.AddComponent<ammoCTRL>().target = target.transform.position;

        hasAmmo = false;
        Destroy(target);
    }

    IEnumerator Reloader()
    {
        yield return new WaitForSeconds(reloadTime);
        hasAmmo = true;
    }

    void CheckForEne()
    {
        if (GameObject.Find("ENEMY(Clone)"))
        {
            ifEne = true;
        }
        else
        {
            ifEne = false;
        }
    }
}
