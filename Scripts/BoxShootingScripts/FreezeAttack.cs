using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeAttack : MonoBehaviour {
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
        cool_down_time = GameManager.getInstance().GetComponent<BoxSkillDataManager>().GetCoolDown(1);
        cool_down_times = -1;
    }
    int cool_down_times;
    public int GetCoolDownTimes()
    {
        return cool_down_times;
    }
    private void OnEnable()
    {
        is_attacking = false;
        cool_down_times = -1;
    }
    IEnumerator AttackWithSnow()
    {
        is_attacking = true;
        //set bolt
        GameObject bolt = PoolManager.getInstance().GetObject(6);
        bolt.transform.position = Box.transform.position;
        //set attack
        Ray ray = main_camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << 8;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 point = hit.point;
            GameObject attackPoint = PoolManager.getInstance().GetObject(5);
            attackPoint.transform.position = point;
            attackPoint.SetActive(true);
            bolt.SetActive(true);
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
            if (AttackManager.getInstance().GetAttackStatus() == 2)
            {
                if (!is_attacking)
                    StartCoroutine("AttackWithSnow");
            }
        }
    }
}
