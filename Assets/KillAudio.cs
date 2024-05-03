using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var music = GameObject.Find("MusicManager");
        if(music != null)
        {
            music.GetComponent<AudioSource>().Stop();
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
