using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMotion : MonoBehaviour {
    [SerializeField]
    float MoveSpeed;
    GameObject Box;
    private void Awake()
    {
        Box = gameObject;
    }

    // Use this for initialization
    void Start () {
        controller = Box.GetComponent<CharacterController>();

    }
    CharacterController controller;
    Vector3 forward = Vector3.forward;
    Vector3 left = Vector3.left;
    bool is_moving = false;
    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetKey(KeyCode.W))
        {
            controller.Move(Box.transform.forward * Time.deltaTime * MoveSpeed);
            is_moving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            controller.Move(-Box.transform.forward * Time.deltaTime * MoveSpeed);
            is_moving = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            controller.Move(-Box.transform.right* Time.deltaTime * MoveSpeed);
            is_moving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            controller.Move(Box.transform.right * Time.deltaTime * MoveSpeed);
            is_moving = true;
        }
        else
        {
            is_moving = false;
        }
    }
    public bool CheckISMoving()
    {
        return is_moving;
    }
}
