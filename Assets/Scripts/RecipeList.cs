using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using USCG.Core.Telemetry;
using UnityEngine.UI;
using TelemetryManagerExamples;
using TMPro;
using Unity.VisualScripting;

public class RecipeList : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> recipeList;
    private List<GameObject> usedRecipes;

    // Tutorial
    [SerializeField]
    private bool isTutorial = false;

    private bool isPaused = false;

    // Monster
    [SerializeField]
    private float monsterPatience;
    [SerializeField]
    private float patienceTimer = 0.0f;

    [SerializeField]
    private GameObject monster;

    [SerializeField]
    private GameObject[] monsterList;

    // Recipe and display
    private GameObject currRecipe;

    [SerializeField]
    private GameObject recipeDisplay;

    private GameObject recipeSprite;

    [SerializeField]
    private GameObject recipeContainer;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private AudioClip wrongRecipe;

    [SerializeField]
    private AudioClip rightRecipe;

    [SerializeField]
    private bool skipDialogue = false;

    private bool fadeIn = false;
    private bool fadeOut = false;

    // Dialogue event
    private bool storyEvent = false;

    [SerializeField]
    private GameObject storyRecipe;

    int monsterCount = 0;

    [SerializeField]
    int maxMonsters = 5;

    [SerializeField]
    GameObject dialogue;

    [SerializeField]
    GameObject resetButton;

    [SerializeField]
    List<AudioClip> voiceClips;

    // Pause menu
    [SerializeField]
    private GameObject pauseMenu;

    // Metrics
    [SerializeField]
    private GameObject metrics;

    [SerializeField]
    private GameObject recipeBook;

    private bool isImpatient = false;

    private bool viewingRecipeBook = false;

    private bool levelOver = false;

    public bool CanViewRecipe()
    {
        if (!isTutorial && !storyEvent && !fadeIn && !fadeOut)
        {
            return true;
        }
        return false;
    }
    private void Start()
    {
        usedRecipes = new List<GameObject>();
        metrics = GameObject.Find("Metrics");
        if(skipDialogue)
        {

        }
        if (!isTutorial)
        {
            StartGame();
            recipeContainer.SetActive(false);
            dialogue.SetActive(false);
            metrics.GetComponent<RecordMetricsExample>().StartTimer();
        }
    }

    public void PlayRandomSound()
    {
        player.GetComponent<Player>().StopAudio();
        int randomNum = Random.Range(0, voiceClips.Count);
        player.GetComponent<Player>().PlayAudio(voiceClips[randomNum]);
    }

    public void StartGame()
    {
        if (!skipDialogue)
        {
            if (!isTutorial) storyEvent = true;
            monster = Instantiate(monsterList[0], gameObject.transform);
            voiceClips = monsterList[0].GetComponent<Monster>().GetVoice();
            Color c = monster.GetComponent<SpriteRenderer>().color;
            c.a = 0;
            monster.GetComponent<SpriteRenderer>().color = c;
            fadeIn = true;
            fadeOut = false;
            currRecipe = Instantiate(storyRecipe, gameObject.transform);
            if (isTutorial)
            {
                recipeSprite = Instantiate(currRecipe.GetComponent<Recipe>().GetRecipeSprite(), recipeDisplay.transform);
            }
        }
        else
        {
            GameObject.Find("InkManager").GetComponent<InkManager>().EnableWorkspace();
            StartRecipeAndTimer();
        }
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public Monster GetMonster()
    {
        return monster.GetComponent<Monster>();
    }

    public void DialogueOver()
    {
        storyEvent = false;
        fadeOut = true;
    }

    private void StartRecipeAndTimer()
    {
        GetRandomMonster();
        Color c = monster.GetComponent<SpriteRenderer>().color;
        c.a = 0;
        monster.GetComponent<SpriteRenderer>().color = c;
        GetRandomRecipe();
        patienceTimer = monster.GetComponent<Monster>().GetPatience();
        if(currRecipe.GetComponent<Recipe>().GetPatienceModifier() > 3)
        {
            Debug.Log("Patience before: " + patienceTimer);
            patienceTimer += (currRecipe.GetComponent<Recipe>().GetPatienceModifier());
            Debug.Log("Patience after: " + patienceTimer);
        }
        fadeIn = true;
    }

    private void Update()
    {
        if(!isPaused)
        {
            if (!fadeIn && !fadeOut && !storyEvent && monsterCount <= maxMonsters && !isTutorial && !levelOver)
            {
                patienceTimer -= Time.deltaTime;
                if (patienceTimer <= 0.0f)
                {
                    player.GetComponent<Player>().StopAudio();
                    isImpatient = false;
                    metrics.GetComponent<RecordMetricsExample>().Killed();
                    metrics.GetComponent<RecordMetricsExample>().EndTimer();
                    GameObject.Find("DeathController").GetComponent<DeathController>().LoadKillScene(monster.GetComponent<Monster>(), player.GetComponent<Player>().GetDeathLevel());
                }
                else if (patienceTimer < 8.0f && !isImpatient)
                {
                    isImpatient = true;
                    Debug.Log("playing impatient sound");
                    monster.GetComponent<Monster>().SwapAngrySprite();
                    player.GetComponent<Player>().PlayAudio(monster.GetComponent<Monster>().GetImpatientSound());
                }
            }
            else if (fadeIn)
            {
                if (monster.GetComponent<SpriteRenderer>().color.a < 1)
                {
                    Color c = monster.GetComponent<SpriteRenderer>().color;
                    c.a += Time.deltaTime;
                    monster.GetComponent<SpriteRenderer>().color = c;
                    if (c.a >= 1)
                    {
                        fadeIn = false;
                        if (!storyEvent)
                        {
                            recipeContainer.SetActive(true);
                            PlayRandomSound();
                        }
                        else
                        {
                            dialogue.SetActive(true);
                        }
                    }
                }
            }
            else if (fadeOut)
            {
                if (monster.GetComponent<SpriteRenderer>().color.a >= 0)
                {
                    Color c = monster.GetComponent<SpriteRenderer>().color;
                    c.a -= Time.deltaTime;
                    monster.GetComponent<SpriteRenderer>().color = c;
                    if (c.a <= 0)
                    {
                        if (storyEvent)
                        {
                            storyEvent = false;
                        }
                        else
                        {
                            fadeOut = false;
                            Destroy(monster);
                            Destroy(recipeSprite);
                            Destroy(currRecipe);
                            if (monsterCount < maxMonsters && !isTutorial)
                            {
                                monsterCount++;
                                StartRecipeAndTimer();
                            }
                            else if (isTutorial)
                            {
                                dialogue.GetComponent<Tutorial>().RightDrink();
                            }
                            else
                            {
                                //player.GetComponent<Player>().LoadNextLevel();
                                // Start transitioning.
                                levelOver = true;
                                if(!skipDialogue)
                                {
                                    GameObject.Find("TransitionManager").GetComponent<Transition>().StartTransition();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void GetRandomMonster()
    {
        int randomNum = Random.Range(0, monsterList.Length);
        monster = Instantiate(monsterList[randomNum], gameObject.transform);
        voiceClips = monster.GetComponent<Monster>().GetVoice();
        fadeIn = true;
        fadeOut = false;
    }

    public void GetRandomRecipe()
    {
        if(recipeList.Count == 0)
        {
            // Add all used recipes into recipelist
            foreach(GameObject g in usedRecipes)
            {
                recipeList.Add(g);
            }
            usedRecipes.Clear();
        }
        int randomNum = Random.Range(0, recipeList.Count);
        currRecipe = Instantiate(recipeList[randomNum], gameObject.transform);
        usedRecipes.Add(recipeList[randomNum]);
        recipeList.Remove(recipeList[randomNum]);
        recipeSprite = Instantiate(currRecipe.GetComponent<Recipe>().GetRecipeSprite(), recipeDisplay.transform);
        foreach(Transform child in recipeSprite.transform)
        {
            if(child.gameObject.GetComponent<Animator>() != null)
            {
                Destroy(child.gameObject.GetComponent<Animator>());
            }
        }
    }

    public bool VerifyRecipe(List<GameObject> glass)
    {
        if (currRecipe.GetComponent<Recipe>().CheckRecipeCorrectness(glass))
        {
            if(storyEvent)
            {
                dialogue.GetComponent<InkManager>().RightDrink();
            }
            else
            {
                if(isImpatient)
                {
                    player.GetComponent<Player>().StopAudio();
                    monster.GetComponent<Monster>().SwapHappySprite();
                    isImpatient = false;
                }
                player.GetComponent<Player>().PlayAudio(rightRecipe);
                recipeContainer.SetActive(false);
                fadeOut = true;
                fadeIn = false;
            }
            return true;
        }
        player.GetComponent<Player>().PlayAudio(wrongRecipe);
        if (storyEvent)
        {
            dialogue.GetComponent<InkManager>().WrongDrink();
        }
        if(isTutorial)
        {
            dialogue.GetComponent<Tutorial>().WrongDrink();
        }
        metrics.GetComponent<RecordMetricsExample>().Wrong();
        return false;
    }

    public void Unpause()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        if(!viewingRecipeBook)
        {
            isPaused = true;
            pauseMenu.SetActive(true);
        }
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }

    public void SetPause()
    {
        isPaused = true;
    }

    public void ShowRecipe()
    {
        recipeContainer.SetActive(true);
    }

    public void HideRecipe()
    {
        recipeContainer.SetActive(false);
    }

    public GameObject GetRecipe()
    {
        return currRecipe;
    }

    public void ShowRecipeBook()
    {
        viewingRecipeBook = true;
        recipeBook.SetActive(true);
        recipeBook.GetComponent<RecipeBook>().StartViewing();
    }
    public void HideRecipeBook()
    {
        viewingRecipeBook = false;
        recipeBook.GetComponent<RecipeBook>().StopViewing();
        recipeBook.SetActive(false);
    }
}
