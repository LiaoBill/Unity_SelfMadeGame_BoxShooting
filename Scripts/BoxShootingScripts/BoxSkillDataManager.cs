using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSkillDataManager : MonoBehaviour {
    [SerializeField]
    MyAttack[] Attacks;

    [System.Serializable]
    public class MyAttack
    {
        [SerializeField]
        public string AttackName;
        [SerializeField]
        public float CoolDown;
        [SerializeField]
        public int Damage;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float GetCoolDown(int index)
    {
        return Attacks[index].CoolDown;
    }
    public int GetDamage(int index)
    {
        return Attacks[index].Damage;
    }
}
