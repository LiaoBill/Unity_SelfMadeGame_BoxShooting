using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISwitchManager : MonoBehaviour {
    [SerializeField]
    GameObject Box;
    [SerializeField]
    GameObject EnemySpawner;
    [SerializeField]
    GameObject VSWelcome;
    [SerializeField]
    GameObject WelcomeUI;
    [SerializeField]
    GameObject HUD;
    [SerializeField]
    Text Welcome;
    [SerializeField]
    Text StartText;
    [SerializeField]
    AudioClip[] ButtonClips;
    // Use this for initialization
    void Start () {
        //Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator StartingGame()
    {
        is_starting = true;
        Welcome.text = "Nice Choice";
        int count_down = 3;
        while (true)
        {
            StartText.text = "Start in " + count_down.ToString()+" s";
            yield return new WaitForSecondsRealtime(1.0f);
            count_down--;
            if(count_down == -1)
            {
                break;
            }
        }
        WelcomeUI.GetComponent<Animator>().SetBool("is_off", true);
        yield return new WaitForSecondsRealtime(1.2f);
        WelcomeUI.SetActive(false);
        Box.GetComponent<BoxLookAt>().enabled = true;
        Box.GetComponent<AttackManager>().enabled = true;
        Box.GetComponent<LightningAttack>().enabled = true;
        Box.GetComponent<FreezeAttack>().enabled = true;
        Box.GetComponent<HighSpeedMovement>().enabled = true;
        Box.GetComponent<LightBunchAttack>().enabled = true;
        Box.GetComponent<BoxMotion>().enabled = true;
        Box.GetComponent<BoxHealthManager>().enabled = true;
        Box.GetComponentInChildren<LittleBoxAttack>().enabled = true;
        EnemySpawner.GetComponent<SpawnEnemy>().enabled = true;
        VSWelcome.SetActive(false);
        HUD.SetActive(true);
        //Time.timeScale = 1;
    }
    bool is_starting = false;
    public void StartGame()
    {
        if (is_starting == false)
        {
            StartCoroutine("StartingGame");
        }
    }
    public void PlayStartGameMusic()
    {
        gameObject.GetComponent<AudioSource>().clip = ButtonClips[0];
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void PlayEndGameMusic()
    {
        gameObject.GetComponent<AudioSource>().clip = ButtonClips[1];
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void ExitGame()
    {
        StartCoroutine("EndGame");
    }
    IEnumerator EndGame()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        Application.Quit();
    }
}
