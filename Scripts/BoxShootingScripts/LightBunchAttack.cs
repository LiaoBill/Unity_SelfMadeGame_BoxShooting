using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBunchAttack : MonoBehaviour {
    GameObject Box;
    GameObject MainCamera;
    Camera main_camera;
    private void Awake()
    {
        Box = gameObject;
        MainCamera = GameObject.FindGameObjectWithTag("MyCamera");
        main_camera = MainCamera.GetComponent<Camera>();
    }
    float cool_down_time;
    // Use this for initialization
    void Start()
    {
        Debug.Log("good");
        cool_down_time = GameManager.getInstance().GetComponent<BoxSkillDataManager>().GetCoolDown(2);
        cool_down_times = -1;
    }
    private void OnEnable()
    {
        is_attacking = false;
        cool_down_times = -1;
    }
    int cool_down_times;
    public int GetCoolDownTimes()
    {
        return cool_down_times;
    }
    IEnumerator AttackWithLightning()
    {
        is_attacking = true;
        //set attack
        Ray ray = main_camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << 8;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 point = hit.point;
            GameObject attack = PoolManager.getInstance().GetObject(7);
            attack.transform.position = Box.transform.position;
            point.y = Box.transform.position.y;
            attack.transform.LookAt(point);
            attack.SetActive(true);
        }
        cool_down_times = (int)(cool_down_time / 0.1f);
        while (cool_down_times != 0)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            cool_down_times--;
        }
        cool_down_times = -1;
        is_attacking = false;
    }
    bool is_attacking = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (AttackManager.getInstance().GetAttackStatus() == 3)
            {
                if (!is_attacking)
                    StartCoroutine("AttackWithLightning");
            }
        }
    }
}
