using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndVideo : MonoBehaviour
{
    [SerializeField]
    VideoPlayer video;

    private bool timerStarted;

    [SerializeField]
    private float timeToShow = 1.6f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        video.loopPointReached += StartTimer;
    }

    void StartTimer(VideoPlayer vp)
    {
        timerStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerStarted)
        {
            timer += Time.deltaTime;
            if(timer > timeToShow)
            {
                GameObject.Find("DeathController").GetComponent<DeathController>().LoadDeathScene();
            }
        }
    }
}
