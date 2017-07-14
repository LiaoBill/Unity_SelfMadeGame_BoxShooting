using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class GameManager : MonoBehaviour {
    [SerializeField]
    PostProcessingProfile MainProfile;
    [SerializeField]
    GameObject BoxView;
    [SerializeField]
    Text Health;
    [SerializeField]
    Text Attack;
    [SerializeField]
    GameObject Death;
    [SerializeField]
    Text Score;
    [SerializeField]
    Text CoolingDown;

    [SerializeField]
    GameObject Box;

    GameObject MainCamera;
    int[] Damages;
    // Use this for initialization
    void Start () {
        Score.text = "Score : <color=yellow><b>" + score.ToString() + "</b></color>";
        //Damage
        int attack_counts = 4;
        Damages = new int[attack_counts];
        for (int i = 0; i != attack_counts; i++)
        {
            Damages[i] = GameManager.getInstance().GetComponent<BoxSkillDataManager>().GetDamage(i);
        }
    }

    public static GameManager thisInstance;
    public static GameManager getInstance()
    {
        return thisInstance;
    }
    Vector3 box_origin_position;
    private void Awake()
    {
        if(thisInstance == null)
        {
            thisInstance = this;
        }
        else
        {
            Destroy(thisInstance);
        }
        box_origin_position = Box.transform.position;
        MainCamera = GameObject.FindGameObjectWithTag("MyCamera");
    }
    // Update is called once per frame
    void Update () {
        UpdateHealth();
        CheckHealth();
        UpdateAttack();
        UpdateCoolingDown();
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchBoxView();
        }
    }
    void SwitchBoxView()
    {
        if (BoxView.activeSelf)
        {
            BoxView.SetActive(false);
        }
        else
        {
            BoxView.SetActive(true);
        }
    }
    void UpdateHealth()
    {
        Health.text = "Health Left: <b>"+ Box.GetComponent<BoxHealthManager>().GetHealth().ToString()+"</b>";
    }
    bool already_start_coroutine = false;
    IEnumerator ShowDeathAndRestart()
    {
        already_start_coroutine = true;
        Death.SetActive(true);
        yield return new WaitForSeconds(3);
        Death.SetActive(false);
        Box.transform.position = box_origin_position;
        Box.SetActive(true);
        //effect
        GameObject spawn_effect = PoolManager.getInstance().GetObject(8);
        Vector3 spawn_position = box_origin_position;
        spawn_position.y += 1.0f;
        spawn_effect.transform.position = spawn_position;
        spawn_effect.SetActive(true);
        //logo
        GameObject spawn_point = PoolManager.getInstance().GetObject(9);
        spawn_point.transform.position = spawn_position;
        spawn_point.SetActive(true);
        //score
        score = -1;
        UpdateScore();
        //profile:
        MainCamera.GetComponent<PostProcessingBehaviour>().profile = MainProfile;
        already_start_coroutine = false;
    }
    void CheckHealth()
    {
        if (Box.GetComponent<BoxHealthManager>().GetHealth() <= 0)
        {
            if(already_start_coroutine == false)
                StartCoroutine("ShowDeathAndRestart");
        }
    }
    void UpdateCoolingDown()
    {
        int s = Box.GetComponent<AttackManager>().GetAttackStatus();
        switch (s)
        {
            case 1:
                {
                    int current_cool_down_time_point = Box.GetComponent<LightningAttack>().GetCoolDownTimes();
                    if(current_cool_down_time_point == -1)
                    {
                        CoolingDown.text = "<color=red><b>{Ready}</b></color>";
                    }
                    else
                    {
                        float showing_time_point = current_cool_down_time_point * 0.1f;
                        CoolingDown.text = "<color=red><b>{CD中}</b></color>-- " + showing_time_point.ToString() + " S";
                    }
                    break;
                }
            case 2:
                {
                    int current_cool_down_time_point = Box.GetComponent<FreezeAttack>().GetCoolDownTimes();
                    if (current_cool_down_time_point == -1)
                    {
                        CoolingDown.text = "<color=red><b>{Ready}</b></color>";
                    }
                    else
                    {
                        float showing_time_point = current_cool_down_time_point * 0.1f;
                        CoolingDown.text = "<color=red><b>{CD中}</b></color>-- " + showing_time_point.ToString() + " S";
                    }
                    break;
                }
            case 3:
                {
                    int current_cool_down_time_point = Box.GetComponent<LightBunchAttack>().GetCoolDownTimes();
                    if (current_cool_down_time_point == -1)
                    {
                        CoolingDown.text = "<color=red><b>{Ready}</b></color>";
                    }
                    else
                    {
                        float showing_time_point = current_cool_down_time_point * 0.1f;
                        CoolingDown.text = "<color=red><b>{CD中}</b></color>-- " + showing_time_point.ToString() + " S";
                    }
                    break;
                }
        }
    }
    void UpdateAttack()
    {
        int s = Box.GetComponent<AttackManager>().GetAttackStatus();
        switch (s)
        {
            case 1:
                {
                    Attack.text = "Current Attack : <b>闪电</b>";
                    break;
                }
            case 2:
                {
                    Attack.text = "Current Attack : <b>霜冻</b>";
                    break;
                }
            case 3:
                {
                    Attack.text = "Current Attack : <b>死光</b>";
                    break;
                }
        }
    }
    int score = 0;
    public void UpdateScore()
    {
        score++;
        Score.text = "Score : <color=yellow><b>" + score.ToString() + "</b></color>";
    }
    public void UpdateScore(int score)
    {
        this.score += score;
        Score.text = "Score : <color=yellow><b>" + this.score.ToString() + "</b></color>";
    }

    public int[] GetDamage()
    {
        return Damages;
    }
}
