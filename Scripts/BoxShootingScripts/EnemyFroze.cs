using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFroze : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
    Vector3 pre_position;
    bool is_first_time = true;
	// Update is called once per frame
	void Update () {
        if (is_first_time)
        {
            pre_position = gameObject.transform.position;
            is_first_time = false;
        }
        else
        {
            gameObject.transform.position = pre_position;
        }
	}

    private void OnDisable()
    {
        is_first_time = true;
    }
}
