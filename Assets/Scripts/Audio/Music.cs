using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{

    [SerializeField]
    AudioSource bgmPlayer;

    [SerializeField]
    AudioClip menuBGM;

    [SerializeField]
    AudioClip gameBGM;

    [SerializeField]
    AudioClip BGM2;

    [SerializeField]
    AudioClip BGM3;

    [SerializeField]
    AudioClip BGM4;

    [SerializeField]
    AudioClip BGM5;

    [SerializeField]
    AudioClip wrong;

    public void PlayWrongSound()
    {
        GameObject.Find("Player").GetComponent<Player>().PlayAudio(wrong);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("MusicManager") != null && GameObject.Find("MusicManager") != gameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            bgmPlayer = GetComponent<AudioSource>();
        }
    }

    public void PlayMenuMusic()
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = menuBGM;
        bgmPlayer.loop = true;
        bgmPlayer.Play();
    }

    public void PlayGameMusic()
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = gameBGM;
        bgmPlayer.loop = true;
        bgmPlayer.Play();
    }

    public void PlayMonster2Music()
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = BGM2;
        bgmPlayer.loop = true;
        bgmPlayer.Play();
    }
    public void PlayMonster3Music()
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = BGM3;
        bgmPlayer.loop = true;
        bgmPlayer.Play();
    }
    public void PlayMonster4Music()
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = BGM4;
        bgmPlayer.loop = true;
        bgmPlayer.Play();
    }
    public void PlayMonster5Music()
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = BGM5;
        bgmPlayer.loop = true;
        bgmPlayer.Play();
    }

    private void Update()
    {
        string activescene = SceneManager.GetActiveScene().name;
        if (!bgmPlayer.isPlaying)
        {
            if(activescene == "Level1" || activescene == "Tutorial" || activescene == "RecipeTutorial")
            {
                PlayGameMusic();
            }
            else if(activescene == "Level2")
            {
                PlayMonster2Music();
            }
            else if (activescene == "Level3")
            {
                PlayMonster3Music();
            }
            else if (activescene == "Level4")
            {
                PlayMonster4Music();
            }
            else if (activescene == "Level5")
            {
                PlayMonster5Music();
            }
            else if (activescene == "Menu")
            {
                PlayMenuMusic();
            }
        }
    }
}
