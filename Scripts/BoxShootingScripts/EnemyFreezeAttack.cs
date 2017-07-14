using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFreezeAttack : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == 14)
        {
            if(is_freezing == false)
            {
                StartCoroutine("FreezeEnemy");
                GameManager.getInstance().UpdateScore(3);
            }
        }
    }
    GameObject freeze_status;
    IEnumerator FreezeEnemy()
    {
        is_freezing = true;
        freeze_status = PoolManager.getInstance().GetObject(4);
        freeze_status.transform.position = gameObject.transform.position;
        freeze_status.SetActive(true);
        gameObject.GetComponent<EnemyFroze>().enabled = true;
        yield return new WaitForSeconds(3);
        freeze_status.SetActive(false);
        gameObject.GetComponent<EnemyFroze>().enabled = false;
        is_freezing = false;
    }
    bool is_freezing = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
        if(freeze_status!=null)
            freeze_status.SetActive(false);
    }
}
