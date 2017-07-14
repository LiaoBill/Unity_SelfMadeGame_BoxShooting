using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlay : MonoBehaviour {
    [SerializeField]
    AudioClip[] BGMS;
	// Use this for initialization
	void Start () {
        StartCoroutine("StartGameBGM");
	}
    int current_BGM = 0;
	IEnumerator StartGameBGM()
    {
        gameObject.GetComponent<Animator>().SetBool("is_on", true);
        yield return new WaitForSecondsRealtime(1.0f);
    }
	// Update is called once per frame
	void Update () {
        if (!gameObject.GetComponent<AudioSource>().isPlaying)
        {
            NextIndex();
            gameObject.GetComponent<AudioSource>().clip = BGMS[current_BGM];
            gameObject.GetComponent<AudioSource>().Play();
        }
	}
    void NextIndex()
    {
        if(current_BGM == BGMS.Length-1)
        {
            current_BGM = 0;
        }
        else
        {
            current_BGM++;
        }
    }
}
