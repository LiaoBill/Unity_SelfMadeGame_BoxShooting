using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBox : MonoBehaviour {
    GameObject MainCamera;
    [SerializeField]
    GameObject Box;
    Vector3 box_position;
    Vector3 camera_position;
    Vector3 distraction;
    private void Awake()
    {
        MainCamera = gameObject;
    }
	// Use this for initialization
	void Start () {
        box_position = Box.transform.position;
        camera_position = MainCamera.transform.position;
        distraction = camera_position - box_position;
    }
	
	// Update is called once per frame
	void Update () {
        MainCamera.transform.position = Box.transform.position + distraction;
    }
}
