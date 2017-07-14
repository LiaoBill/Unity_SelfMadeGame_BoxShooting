using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighSpeedMovement : MonoBehaviour {
    GameObject Box;
    GameObject MainCamera;
    Camera main_camera;
    [SerializeField]
    GameObject MovePositionPrefab;
    [SerializeField]
    GameObject ForcingPrefab;
    [SerializeField]
    GameObject BoxMoveBackPrefab;
    private void Awake()
    {
        Box = gameObject;
        MainCamera = GameObject.FindGameObjectWithTag("MyCamera");
        main_camera = MainCamera.GetComponent<Camera>();
    }
    // Use this for initialization
    GameObject MovePosition;
    GameObject Forcing;
    GameObject BoxMoveBack;
    void Start () {
        MovePosition = Instantiate(MovePositionPrefab);
        MovePosition.SetActive(false);
        Forcing = Instantiate(ForcingPrefab);
        Forcing.SetActive(false);
        BoxMoveBack = Instantiate(BoxMoveBackPrefab);
        BoxMoveBack.SetActive(false);
    }
    Ray ray;
    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Box.GetComponent<BoxMotion>().enabled = false;
            StopCoroutine("FastMove");
            ray = main_camera.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << 8;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (!MovePosition.activeSelf)
                {
                    MovePosition.SetActive(true);
                    Forcing.SetActive(true);
                    //BoxMoveBack.SetActive(true);
                }
                Vector3 show_position = hit.point;
                show_position.y += 0.5f;
                MovePosition.transform.position = show_position;
                Forcing.transform.position = Box.transform.position;
                //BoxMoveBack.transform.position = Box.transform.position;
            }
            Time.timeScale = 0.2f;
        }
        else if(Input.GetMouseButtonUp(1))
        {
            Box.GetComponent<BoxMotion>().enabled = true;
            Time.timeScale = 1.0f;
            StartCoroutine("FastMove");
            if (MovePosition.activeSelf)
            {
                MovePosition.SetActive(false);
                Forcing.SetActive(false);
                //BoxMoveBack.SetActive(false);
            }
        }
        else if(Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.D))
        {
            Box.GetComponent<BoxMotion>().enabled = true;
            StopCoroutine("FastMove");
        }
    }
    IEnumerator FastMove()
    {
        float using_time = 0.1f;
        Vector3 box_new_position = MovePosition.transform.position;
        box_new_position.y = Box.transform.position.y;
        Vector3 move_direction = box_new_position - Box.transform.position;
        Vector3 box_current_position = Box.transform.position;
        Vector3 reverse_box_new_position = box_current_position - box_new_position + box_current_position;
        StartCoroutine("TimeRecorder");
        StopCoroutine("StopBackParticle");
        BoxMoveBack.SetActive(false);
        while (true)
        {
            if (is_time_reaching)
            {
                is_time_reaching = false;
                break;
            }
            Box.GetComponent<BoxHealthManager>().SetUnDamaging();
            Box.GetComponent<CharacterController>().Move(Vector3.Normalize(move_direction)*Time.deltaTime*Vector3.Distance(box_current_position,box_new_position)/using_time);
            BoxMoveBack.SetActive(true);
            BoxMoveBack.transform.position = Box.transform.position;
            BoxMoveBack.transform.LookAt(reverse_box_new_position);
            /*
            if (Vector3.Distance(Box.transform.position, box_new_position) < 2.0f)
            {
                break;
            }
            */
            yield return new WaitForFixedUpdate();
        }
    }
    bool is_time_reaching = false;
    IEnumerator TimeRecorder()
    {
        is_time_reaching = false;
        yield return new WaitForSecondsRealtime(0.1f);
        Box.GetComponent<BoxHealthManager>().SetGetDamage();
        StartCoroutine("StopBackParticle");
        is_time_reaching = true;
    }
    IEnumerator StopBackParticle()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        BoxMoveBack.SetActive(false);
    }
}
