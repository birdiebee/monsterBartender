using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private bool isHolding = false;
    [SerializeField]
    private GameObject heldObject = null;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip woosh;

    [SerializeField]
    private RecipeList recipeController;

    [SerializeField]
    private string nextLevel;

    [SerializeField]
    private string deathLevel;

    [SerializeField]
    private string currLevel;

    [SerializeField]
    private bool isTutorial = false;
    private bool doneIce = false;
    private bool doneDrink = false;
    private bool doneTutorial = false;
    private bool doneTrashboy = false;
    private bool droppedTutorial = false;

    private void Start()
    {
        currLevel = SceneManager.GetActiveScene().name;
    }

    public string GetCurrLevel()
    {
        return currLevel;
    }

    public string GetDeathScene()
    {
        return deathLevel;
    }
    public void Kill()
    {
        SceneManager.LoadScene(deathLevel);
    }
    public void DoneTrashboy()
    {
        doneTrashboy = true;
    }
    public void PlayAudio(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
    public bool IsHoldingObject()
    {
        return isHolding;
    }

    public GameObject GetHeldObject()
    {
        return heldObject;
    }

    public string GetDeathLevel()
    {
        return deathLevel;
    }

    public void DropObject(GameObject obj)
    {
        isHolding = false;
        heldObject = null;
        if (obj.tag == "Glass")
        {
            obj.GetComponent<Workspace>().IsDropped();
            PlayAudio(woosh);
        }
        else
        {
            obj.GetComponent<Ingredients>().IsDropped();
        }
    }

    public void HoldObject(GameObject obj)
    {
        if(isHolding)
        {
            Debug.Log("Called HoldObject without checking if object is already held!");
        }
        else
        {
            isHolding = true;
            heldObject = obj;
            if(obj.name == "Ice" && isTutorial && !doneIce)
            {
                doneIce = true;
                GameObject.Find("Tutorial").GetComponent<Tutorial>().PickedUpIce();
            }
            else if (obj.name == "Holy Water" && isTutorial && doneIce)
            {
                doneDrink = true;
                GameObject.Find("Tutorial").GetComponent<Tutorial>().PickedUpHolyWater();
            }
            else if(isTutorial && doneDrink && !doneTutorial && doneTrashboy)
            {
                doneTutorial = true;
                GameObject.Find("Tutorial").GetComponent<Tutorial>().PickedUpObject();
            }
        }
    }

    public void ClickedNothing()
    {
        DropObject(heldObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && isHolding)
        {
            DropObject(heldObject);
            if(isTutorial && doneTrashboy && !droppedTutorial)
            {
                droppedTutorial = true;
                GameObject.Find("Tutorial").GetComponent<Tutorial>().DroppedObject();
            }
        }

        if (Input.GetKey("escape"))
        {
            if(!recipeController.GetIsPaused())
            {
                if(isHolding) DropObject(heldObject);
                recipeController.Pause();
            }
        }
    }

    public void LoadNextLevel()
    {
        GameObject musicManager = GameObject.Find("MusicManager");
        if(nextLevel == "Level2")
        {
            musicManager.GetComponent<Music>().PlayMonster2Music();
        }
        else if (nextLevel == "Level3")
        {
            musicManager.GetComponent<Music>().PlayMonster3Music();
        }
        else if (nextLevel == "Level4")
        {
            musicManager.GetComponent<Music>().PlayMonster4Music();
        }
        else if (nextLevel == "Level5")
        {
            musicManager.GetComponent<Music>().PlayMonster5Music();
        }
        else
        {
            musicManager.GetComponent<Music>().PlayGameMusic();
        }
        SceneManager.LoadScene(nextLevel);
    }
}
