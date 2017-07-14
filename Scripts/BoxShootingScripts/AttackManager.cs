using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour {
    public static AttackManager thisInstance;
    private void Awake()
    {
        if(thisInstance == null)
        {
            thisInstance = this;

        }
        else
        {
            Destroy(thisInstance);
        }
    }

    public static AttackManager getInstance()
    {
        return thisInstance;
    }
    // Use this for initialization
    void Start () {
		
	}
    int status = 1;
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //lightning attack
            status = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //eternal boom
            status = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //eternal boom
            status = 3;
        }
    }

    public int GetAttackStatus()
    {
        return status;
    }
}
