using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour {
    void OnTriggerEnter(Collider c)
    {
        if (c.transform.gameObject.tag.Equals("Box"))
        {
            StartCoroutine("KillBoxCycle");
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.transform.gameObject.tag.Equals("Box"))
        {
            StopCoroutine("KillBoxCycle");
        }
    }
    IEnumerator KillBoxCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            bool cons = Box.GetComponent<BoxHealthManager>().Kill();
            if(cons == false)
            {
                break;
            }
        }
    }
    GameObject Box;
    private void Awake()
    {
        Box = GameObject.FindGameObjectWithTag("Box");
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
