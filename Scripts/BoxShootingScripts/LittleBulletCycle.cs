using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBulletCycle : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        GameObject bullet_explode = PoolManager.getInstance().GetObject(12);
        bullet_explode.transform.position = gameObject.transform.position;
        bullet_explode.SetActive(true);
        gameObject.SetActive(false);
    }
    
    [SerializeField]
    float speed = 50.0f;
    private void OnEnable()
    {
        forward_direction = gameObject.transform.forward;
        StartCoroutine("LifeCycle");
    }

    IEnumerator LifeCycle()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start () {
		
	}
    Vector3 forward_direction;
    // Update is called once per frame
    void Update () {
        gameObject.transform.Translate(forward_direction * Time.deltaTime * speed,Space.World);
	}
}
