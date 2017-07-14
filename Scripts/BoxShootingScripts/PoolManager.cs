using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
    [SerializeField] MyPoolObject[] PoolObjects;
    [System.Serializable]
    public class MyPoolObject
    {
        [SerializeField]
        public GameObject Prefab;
        [SerializeField]
        public int Count;
    }
    // Use this for initialization
    List<List<GameObject>> pool_lists;
    public static PoolManager thisInstance;
    private void Awake()
    {
        if (thisInstance == null)
        {
            thisInstance = this;
        }
        else
        {
            Destroy(thisInstance);
        }
    }
    public static PoolManager getInstance()
    {
        return thisInstance;
    }
    void Start () {
        pool_lists = new List<List<GameObject>>();

        for (int i = 0; i != PoolObjects.Length; i++)
        {
            MyPoolObject c = PoolObjects[i];
            GameObject prefab = c.Prefab;
            int count = c.Count;
            List<GameObject> pool_list = new List<GameObject>();
            for (int j = 0; j != count; j++)
            {
                GameObject current = Instantiate(prefab,gameObject.transform);
                current.SetActive(false);
                pool_list.Add(current);
            }
            pool_lists.Add(pool_list);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public GameObject GetObject(int index)
    {
        List<GameObject> t = pool_lists[index];
        for(int i = 0; i != t.Count; i++)
        {
            if(t[i].activeSelf == false)
            {
                return t[i];
            }
        }
        return null;
    }
}
