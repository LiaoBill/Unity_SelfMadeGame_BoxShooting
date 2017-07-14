using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCycle : MonoBehaviour {
    ParticleSystem thisSystem;
    // Use this for initialization
    void Start () {
        thisSystem = gameObject.GetComponent<ParticleSystem>();

    }
	
	// Update is called once per frame
	void Update () {
        if (!thisSystem.isPlaying)
        {
            gameObject.SetActive(false);
        }
	}
}
