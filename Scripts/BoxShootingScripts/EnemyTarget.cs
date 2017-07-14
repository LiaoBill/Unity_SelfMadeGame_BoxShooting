using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTarget : MonoBehaviour {
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
        gameObject.GetComponent<NavMeshAgent>().destination = Box.transform.position;

    }
}
