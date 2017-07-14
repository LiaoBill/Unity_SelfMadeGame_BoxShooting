using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
    [SerializeField]
    GameObject[] SpawnPoints;
    int[] wave_count = new int[10] {
        3,5,10,10,22,34,50,50,100,100
    };
	// Use this for initialization
	void Start () {
        
    }
    bool no_enemy = true;
    int current_wave = -1;
    int getNextWave()
    {
        if(current_wave == 9)
        {
            current_wave = 0;
            return current_wave;
        }
        else
        {
            current_wave++;
            return current_wave;
        }
    }
	// Update is called once per frame
	void Update () {
        if (no_enemy)
        {
            StartCoroutine("SpawnEnemies");
        }
	}
    IEnumerator SpawnEnemies()
    {
        no_enemy = false;
        int enemy_count = wave_count[getNextWave()];
        for(int i = 0; i != enemy_count; i++)
        {
            getEnemy();
        }
        yield return new WaitForSeconds(10);
        no_enemy = true;
    }

    GameObject getEnemy()
    {
        GameObject enemy = PoolManager.getInstance().GetObject(0);
        if(enemy == null)
        {
            return null;
        }
        enemy.transform.position = SpawnPoints[Random.Range(0, 4)].transform.position;
        enemy.SetActive(true);
        return enemy;
    }
}
