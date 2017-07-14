using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSkillEffectController : MonoBehaviour {
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
        if (Box.GetComponent<BoxMotion>().CheckISMoving())
        {
            gameObject.GetComponent<Animator>().SetBool("is_moving", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("is_moving", false);
        }
    }
}
