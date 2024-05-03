using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using TelemetryManagerExamples;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class RecipeBook : MonoBehaviour
{
    [SerializeField]
    private float maxTime = 5.0f;
    private float timer = 0.0f;

    private bool isViewing = false;

    [SerializeField]
    GameObject panel;

    [SerializeField]
    private TextMeshProUGUI ingredientList;

    [SerializeField]
    private RecipeList recipeController;

    [SerializeField]
    private GameObject darkBg;

    [SerializeField]
    private AudioClip openBook;

    [SerializeField]
    private AudioClip closeBook;

    private bool canView = false;

    [SerializeField]
    private GameObject ice, mermaid, holy, death, pearls, monkey, finger, sakura, beetle, ash, blood, venom, genie, sake, ambrosia, parasol, staff, fairy, apple, pom;

    private Dictionary<string, GameObject> ingredientMap;

    // Ingredient images
    [SerializeField]
    private GameObject ingredientOrganizer;

    [SerializeField]
    private GameObject recipeImage;

    [SerializeField]
    private TextMeshProUGUI recipeName;

    [SerializeField]
    private GameObject sizeRef;

    private GameObject recipeDisplay;

    [SerializeField]
    private VideoPlayer video;

    [SerializeField]
    private float startVideoTime = 5.0f;

    private bool playingVideo = false;

    private bool kill = false;

    [SerializeField]
    private AudioClip bookKill;

    [SerializeField]
    private GameObject monsterSprite;

    private int lastFrame;

    private GameObject metrics;

    // Start is called before the first frame update
    void Start()
    {
        metrics = GameObject.Find("Metrics");
        timer = maxTime;
        // Animation video
        video.Play();
        video.frame = 0;
        video.Pause();
        // Setting up ingredient map
        ingredientMap = new Dictionary<string, GameObject>();
        ingredientMap.Add("Ice", ice);
        ingredientMap.Add("Mermaid Tears", mermaid);
        ingredientMap.Add("Holy Water", holy);
        ingredientMap.Add("Death", death);
        ingredientMap.Add("Pearls", pearls);
        ingredientMap.Add("Monkey Paw", monkey);
        ingredientMap.Add("Demon Finger", finger);
        ingredientMap.Add("Nile River Blood", blood);
        ingredientMap.Add("Sake", sake);
        ingredientMap.Add("Scarab Beetle", beetle);
        ingredientMap.Add("Snake Venom", venom);
        ingredientMap.Add("Sakura Petals", sakura);
        ingredientMap.Add("Shaman Ashes", ash);
        ingredientMap.Add("Ambrosia", ambrosia);
        ingredientMap.Add("Parasol", parasol);
        ingredientMap.Add("Eye Staff", staff);
        ingredientMap.Add("Fairy Wings", fairy);
        ingredientMap.Add("Golden Apple", apple);
        ingredientMap.Add("Pomegranate Seeds", pom);
        ingredientMap.Add("Genie", genie);
    }

    // Update is called once per frame
    void Update()
    {
        if (isViewing && canView)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                if(!kill)
                {
                    Debug.Log("killed");
                    kill = true;
                    recipeController.GetComponent<RecipeList>().SetPause();
                }
            }
            else if(timer <= startVideoTime && !playingVideo)
            {
                video.Play();
                playingVideo = true;
            }
        }
    }

    public void StartViewing()
    {
        if(!isViewing)
        {
            GameObject.Find("Player").GetComponent<Player>().PlayAudio(openBook);
            panel.SetActive(true);
            DisplayRecipe();
            isViewing = true;
            if(playingVideo)
            {
                video.Play();
                video.frame = lastFrame;
            }
        }
    }

    public void StopViewing()
    {
        if (kill)
        {
            metrics.GetComponent<RecordMetricsExample>().Killed();
            metrics.GetComponent<RecordMetricsExample>().EndTimer();
            GameObject.Find("DeathController").GetComponent<DeathController>().LoadBookKillScene(GameObject.Find("Player").GetComponent<Player>().GetDeathLevel());
        }
        if (recipeDisplay != null)
        {
            Destroy(recipeDisplay);
        }
        recipeName.text = "";
        GameObject.Find("Player").GetComponent<Player>().PlayAudio(closeBook);
        isViewing = false;
        lastFrame = (int)video.frame;
        video.Pause();
    }

    private void DisplayRecipe()
    {
        if(recipeController.CanViewRecipe())
        {
            canView = true;
            ingredientList.text = "";
            // Destroys all previously created ingredient displays
            foreach (Transform child in ingredientOrganizer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            string ingString = "";
            GameObject recipe = recipeController.GetRecipe();
            recipeDisplay = Instantiate(recipe.GetComponent<Recipe>().GetRecipeSprite(), sizeRef.transform);
            recipeDisplay.GetComponent<SpriteRenderer>().sortingOrder += 700;
            foreach(Transform child in recipeDisplay.transform)
            {
                child.gameObject.GetComponent<SpriteRenderer>().sortingOrder += 700;
                if (child.gameObject.GetComponent<Animator>() != null)
                {
                    Destroy(child.gameObject.GetComponent<Animator>());
                }
            }
            recipeName.text = recipe.GetComponent<Recipe>().GetRecipeName();
            List<string> ingredients = recipe.GetComponent<Recipe>().GetRecipeStrings();
            foreach(string s in ingredients)
            {
                GameObject newIng = ingredientMap[s];
                Instantiate(newIng, ingredientOrganizer.transform);
            }
        }
        else
        {
            foreach (Transform child in ingredientOrganizer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            canView = false;
            ingredientList.text = "How are you supposed to look up a recipe when you don't even know what it looks like?\r\n\r\nUse your brain, dumbass.";
        }
    }
}
