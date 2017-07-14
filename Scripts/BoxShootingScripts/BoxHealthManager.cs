using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class BoxHealthManager : MonoBehaviour {
    int is_touching = 0;
    private void OnTriggerEnter(Collider other)
    {
        is_touching++;
    }
    private void OnTriggerExit(Collider other)
    {
        is_touching--;
        if (is_touching != 0)
        {
            Debug.Log(other.transform.name);
            Kill();
        }
    }
    GameObject MainCamera;
    [SerializeField]
    int health = 10;
    [SerializeField]
    PostProcessingProfile MainProfile;
    [SerializeField]
    PostProcessingProfile DamageProfile;

    [SerializeField]
    PostProcessingProfile DyingProfile;
    private void Awake()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MyCamera");
    }
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            is_touching = 0;
            gameObject.SetActive(false);
        }
        else if(health>=1&&health<=2)
        {
            is_touching = 0;
            MainCamera.GetComponent<PostProcessingBehaviour>().profile = DyingProfile;
        }
    }
    IEnumerator StartDamageGet()
    {
        is_start_damage_get = true;
        MainCamera.GetComponent<PostProcessingBehaviour>().profile = DamageProfile;
        yield return new WaitForSecondsRealtime(0.1f);
        MainCamera.GetComponent<PostProcessingBehaviour>().profile = MainProfile;
        is_start_damage_get = false;
    }
    bool is_start_damage_get = false;
    public bool Kill()
    {
        if (is_undamaging)
        {
            return true;
        }
        if (health > 0)
        {
            if(!is_start_damage_get)
                StartCoroutine("StartDamageGet");
            health--;
            return true;
        }
        return false;
    }
    public int GetHealth()
    {
        return health;
    }
    IEnumerator UnDamagingBoxWhenSpawn()
    {
        SetUnDamaging();
        yield return new WaitForSecondsRealtime(3.0f);
        SetGetDamage();
    }
    private void OnEnable()
    {
        StartCoroutine("UnDamagingBoxWhenSpawn");
        health = 10;
        is_start_damage_get = false;
    }
    bool is_undamaging = false;
    public void SetUnDamaging()
    {
        is_undamaging = true;
    }
    public void SetGetDamage()
    {
        is_undamaging = false;
    }
}
