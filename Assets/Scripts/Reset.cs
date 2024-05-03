using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    [SerializeField]
    private GameObject recipeController;

    [SerializeField]
    private GameObject creditsUI;

    public void ResetTheGame()
    {
        GameObject.Find("MusicManager").GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("Menu");
    }

    public void ShowCredits()
    {
        creditsUI.SetActive(true);
    }

    public void HideCredits()
    {
        creditsUI.SetActive(false);
    }

    public void Restart1()
    {
        var audio = GameObject.Find("Audio");
        if (audio != null)
        {
            audio.GetComponent<AudioSource>().Stop();
            Destroy(audio);
        }
        SceneManager.LoadScene("Level1");
    }
    public void Restart2()
    {
        var audio = GameObject.Find("Audio");
        if (audio != null)
        {
            audio.GetComponent<AudioSource>().Stop();
            Destroy(audio);
        }
        SceneManager.LoadScene("Level2");
    }
    public void Restart3()
    {
        var audio = GameObject.Find("Audio");
        if(audio != null)
        {
            audio.GetComponent<AudioSource>().Stop();
            Destroy(audio);
        }
        SceneManager.LoadScene("Level3");
    }
    public void Restart4()
    {
        var audio = GameObject.Find("Audio");
        if (audio != null)
        {
            audio.GetComponent<AudioSource>().Stop();
            Destroy(audio);
        }
        SceneManager.LoadScene("Level4");
    }
    public void Restart5()
    {
        var audio = GameObject.Find("Audio");
        if (audio != null)
        {
            audio.GetComponent<AudioSource>().Stop();
            Destroy(audio);
        }
        SceneManager.LoadScene("Level5");
    }
    public void EndGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResumeGame()
    {
        recipeController.GetComponent<RecipeList>().Unpause();
    }

    public void StartGame()
    {
        GameObject.Find("MusicManager").GetComponent<Music>().PlayGameMusic();
        SceneManager.LoadScene("Tutorial");
    }
}
