using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour
{
    // Start is called before the first frame update
    private string deathLevel;
    private string deathScene;
    private bool isKilling = false;
    private float killTimer = 0.0f;
    private float killMax = 1.0f;

    void Start()
    {
        killTimer = killMax;
        if(GameObject.Find("DeathController") != null && GameObject.Find("DeathController") != gameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadKillScene(Monster monster, string level)
    {
        deathLevel = level;
        GameObject.Find("MusicManager").GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene(monster.GetKillScene());
    }

    public void LoadBookKillScene(string level)
    {
        deathLevel = level;
        GameObject.Find("MusicManager").GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("BookDeath");
    }

    public void StartBookKill(string level, string death)
    {
        GameObject.Find("MusicManager").GetComponent<AudioSource>().Stop();
        isKilling = true;
        deathLevel = level;
        deathScene = death;
    }

    private void Update()
    {

    }

    public void LoadDeathScene()
    {
        GameObject.Find("MusicManager").GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene(deathLevel);
    }
}
