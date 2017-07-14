using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBoxAttack : MonoBehaviour {
	// Use this for initialization
	void Start () {
        cool_down_time = GameManager.getInstance().GetComponent<BoxSkillDataManager>().GetCoolDown(3);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!is_shooting)
            {
                StartCoroutine("StartShooting");
            }
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine("StartShooting");
            is_shooting = false;
        }
	}
    float cool_down_time;
    bool is_shooting = false;
    IEnumerator StartShooting()
    {
        is_shooting = true;
        while (true)
        {
            //bullet
            GameObject current_bullet = PoolManager.getInstance().GetObject(10);
            current_bullet.transform.position = gameObject.transform.position;
            Vector3 look_at_position = gameObject.transform.position;
            look_at_position += gameObject.transform.forward;
            current_bullet.transform.LookAt(look_at_position);
            current_bullet.SetActive(true);
            //flame
            GameObject current_flame = PoolManager.getInstance().GetObject(11);
            current_flame.transform.position = gameObject.transform.position;
            current_flame.transform.LookAt(look_at_position);
            current_flame.SetActive(true);
            //music
            //if (!gameObject.GetComponent<AudioSource>().isPlaying)
            //{
                gameObject.GetComponent<AudioSource>().Play();
            //}
            yield return new WaitForSecondsRealtime(cool_down_time);
        }
    }
}
