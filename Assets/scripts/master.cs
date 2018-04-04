using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class master : MonoBehaviour {
    public static float HUBHP = 100;
    public static float ammoSpd = .2f;
    public float radius = 65;
    public GameObject hubLocation;
    public GameObject EneFolder;
    public static int moneys = 500;
    public static float towCost = 100;

    public static RectTransform hpT;
    public static RectTransform hpB;

    public static Text txtM;
    public bool nextWave;
    public int difficulty = 5;

    public Button NWB;

    // Use this for initialization
    void Start ()
    {
        EneFolder = new GameObject("EneFolder");
        hpB = GameObject.Find("HP_B").GetComponent<RectTransform>();
        hpT = GameObject.Find("HP_T").GetComponent<RectTransform>();
        txtM = GameObject.Find("txtMon").GetComponent<Text>();
        txtM.text = "MONEYS: $" + moneys;
        NWB = GameObject.Find("Button").GetComponent<Button>();
    }

    void NewWave(GameObject EneFolder, int count)
    {
        GameObject[] e = new GameObject[count];

        for (int i = 0; i < e.Length; i++)
        {
            float loc = Random.Range(0, Mathf.PI * 2);

            e[i] = CreateEntity("ENEMY");

            e[i].AddComponent<Rigidbody>().isKinematic = true;
            e[i].transform.SetParent(EneFolder.transform);
            e[i].transform.tag = "ENE";
            e[i].transform.position = new Vector3(Mathf.Sin(loc) * radius, 1, Mathf.Cos(loc) * radius);
            NavMeshAgent n = e[i].GetComponent<NavMeshAgent>();
            n.destination = hubLocation.transform.position;
            n.speed = Random.Range(4f, 9f);

        }
    }

    public static GameObject CreateEntity(string thing)
    {
        return Instantiate(Resources.Load<GameObject>("prefabs/"+thing));
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Break();
        }

        if (nextWave)
        {
            NewWave(EneFolder, difficulty);
            nextWave = false;
        }

        if (GameObject.Find("ENEMY(Clone)"))
        {
            NWB.interactable = false;
        }
        else
        {
            NWB.interactable = true;
        }

        //    if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    NewWave(EneFolder, Random.Range(1, 50));
        //}

        if (Input.GetMouseButtonDown(0))
        {
            if (moneys >= towCost)
            {
                PlaceTower();
            }
        }

        GameObject[] eneArr = GameObject.FindGameObjectsWithTag("ENE");
        GameObject[] towArr = GameObject.FindGameObjectsWithTag("TOW");

        foreach (GameObject gO in towArr)
        {
            float tracker = Mathf.Infinity;
            int updater = 0;

            for (int i = 0; i < eneArr.Length; i++)
            {
                float store = Vector3.Distance(gO.transform.position, eneArr[i].transform.position);

                // 
                if (store < tracker)
                {
                    tracker = store;
                    updater = i;
                }
            }

            if (eneArr.Length > 0)
            {
                gO.GetComponent<towerCTRL>().target = eneArr[updater];
            }

        }
    }

    private static void PlaceTower()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject t = CreateEntity("Tower");
            t.AddComponent<NavMeshSourceTag>();
            t.AddComponent<towerCTRL>();
            t.transform.position = new Vector3(hit.point.x, t.transform.position.y, hit.point.z);
            t.tag = "TOW";

            //
            moneys -= (int)towCost;
            UpdateMoneys();
        }
    }

    public static void UpdateMoneys()
    {
        txtM.text = "MONEYS: $" + moneys;
    }

    public static void TakeDmg()
    {
        HUBHP--;
        print((HUBHP / 100));
        hpT.sizeDelta = new Vector2((HUBHP / 100f) * hpB.sizeDelta.x, hpB.sizeDelta.y);
    }

    public void NW()
    {
        nextWave = true;
        difficulty += 5;
    }
}
