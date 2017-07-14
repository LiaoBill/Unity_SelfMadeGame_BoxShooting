using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.layer == 13)
        {
            //lightning damage
            health -= Damages[0];
            GameManager.getInstance().UpdateScore(3);
            if (health <= 0)
            {
                GameObject e = PoolManager.getInstance().GetObject(3);
                e.transform.position = gameObject.transform.position;
                e.SetActive(true);
                gameObject.SetActive(false);
                GameManager.getInstance().UpdateScore(10);
            }
        }
        else if (other.transform.gameObject.layer == 14)
        {
            //light bunch damage
            health -= Damages[1];
            GameManager.getInstance().UpdateScore(3);
            if (health <= 0)
            {
                GameObject e = PoolManager.getInstance().GetObject(3);
                e.transform.position = gameObject.transform.position;
                e.SetActive(true);
                gameObject.SetActive(false);
                GameManager.getInstance().UpdateScore(10);
            }
        }
        else if (other.transform.gameObject.layer == 15)
        {
            //light bunch damage
            health -= Damages[2];
            GameManager.getInstance().UpdateScore(3);
            if (health <= 0)
            {
                GameObject e = PoolManager.getInstance().GetObject(3);
                e.transform.position = gameObject.transform.position;
                e.SetActive(true);
                gameObject.SetActive(false);
                GameManager.getInstance().UpdateScore(10);
            }
        }
        else if (other.transform.gameObject.layer == 16)
        {
            //light bunch damage
            health -= Damages[3];
            GameManager.getInstance().UpdateScore(1);
            if (health <= 0)
            {
                GameObject e = PoolManager.getInstance().GetObject(3);
                e.transform.position = gameObject.transform.position;
                e.SetActive(true);
                gameObject.SetActive(false);
                GameManager.getInstance().UpdateScore(10);
            }
        }
    }
    int health;
    int[] Damages;
    // Use this for initialization
    void Start () {
        Damages = GameManager.getInstance().GetDamage();
    }

    private void OnEnable()
    {
        health = 50;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
