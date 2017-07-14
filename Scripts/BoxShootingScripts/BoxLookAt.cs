using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLookAt : MonoBehaviour {
    GameObject Box;
    GameObject MainCamera;
    Camera main_camera;
    private void Awake()
    {
        Box = gameObject;
        MainCamera = GameObject.FindGameObjectWithTag("MyCamera");
        main_camera = MainCamera.GetComponent<Camera>();
    }
    // Use this for initialization
    void Start () {
        
    }
    Ray ray;
    RaycastHit hit;
	// Update is called once per frame
	void Update () {
        ray = main_camera.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 8;
        if(Physics.Raycast(ray,out hit, Mathf.Infinity, layerMask))
        {
            Vector3 point = hit.point;
            point.y = Box.transform.position.y;
            Box.transform.LookAt(point);
        }
    }
}
