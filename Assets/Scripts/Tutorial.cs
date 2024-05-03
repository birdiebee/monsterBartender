using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using TelemetryManagerExamples;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private List<TextAsset> dialogueStep;
    private int step = 0;
    private Story dialogue;

    [SerializeField]
    private GameObject dialogueBox;

    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private VerticalLayoutGroup buttons;

    [SerializeField]
    private Button buttonPrefab;

    [SerializeField]
    private GameObject clickIcon;

    [SerializeField]
    private GameObject recipeController;

    [SerializeField]
    private GameObject workspace;

    [SerializeField]
    private GameObject metrics;

    [SerializeField]
    private TextAsset wrongDialogue;

    [SerializeField]
    private TextAsset rightDialogue;

    // Arrow prefabs
    [SerializeField]
    private GameObject arrowHolyWater;

    [SerializeField]
    private GameObject arrowIce;

    [SerializeField]
    private GameObject arrowGlass;

    [SerializeField]
    private GameObject arrowTrashboy;

    [SerializeField]
    private GameObject arrowIngredient;

    [SerializeField]
    private GameObject arrowBook;

    [SerializeField]
    private GameObject particles;

    private bool displayingParticles = false;

    private ClickBlock clickBlocker;


    // Tutorial bools
    private bool pickedUpIce = false;
    private bool hasIce = false;
    private bool pickedUpHolyWater = false;
    private bool hasHolyWater = false;
    private bool usedTrashboy = false;
    private bool pickedUpObject = false;
    private bool droppedObject = false;
    private bool correctDrinkMade = false;
    private bool wrongDrinkMade = false;
    private bool startedLevel = false;
    private bool displayedPearlArrow = false;

    [SerializeField]
    private bool isRecipeTutorial = false;

    [SerializeField]
    private List<GameObject> newIng;

    public void AddedIce()
    {
        hasIce = true;
        step++;
        dialogue = new Story(dialogueStep[step].text);
        DisplayNextLine();
        arrowGlass.SetActive(false);
        arrowHolyWater.SetActive(true);
        clickBlocker.DisableAllColliders();
        clickBlocker.EnableCollider(GameObject.Find("Holy Water"));
    }

    public void PickedUpIce()
    {
        if(!pickedUpIce)
        {
            pickedUpIce = true;
            step++;
            dialogue = new Story(dialogueStep[step].text);
            DisplayNextLine();
            arrowIce.SetActive(false);
            arrowGlass.SetActive(true);
            clickBlocker.DisableAllColliders();
        }
    }

    public void PickedUpHolyWater()
    {
        if(!pickedUpHolyWater)
        {
            pickedUpHolyWater = true;
            step++;
            dialogue = new Story(dialogueStep[step].text);
            DisplayNextLine();
            arrowHolyWater.SetActive(false);
            arrowGlass.SetActive(true);
            clickBlocker.DisableAllColliders();
        }
    }

    public void AddedHolyWater()
    {
        if (!hasHolyWater)
        {
            hasHolyWater = true;
            step++;
            dialogue = new Story(dialogueStep[step].text);
            clickIcon.SetActive(true);
            DisplayNextLine();
            arrowGlass.SetActive(false);
        }
    }

    public void UsedTrashboy()
    {
        if(!usedTrashboy)
        {
            DisableWorkspace();
            usedTrashboy = true;
            step++;
            dialogue = new Story(dialogueStep[step].text);
            clickIcon.SetActive(true);
            DisplayNextLine();
            arrowTrashboy.SetActive(false);
            clickBlocker.DisableAllColliders();
        }
    }

    public void PickedUpObject()
    {
        if (!pickedUpObject)
        {
            pickedUpObject = true;
            step++;
            dialogue = new Story(dialogueStep[step].text);
            clickIcon.SetActive(false);
            DisplayNextLine();
            arrowIngredient.SetActive(false);
            clickBlocker.DisableAllColliders();
        }
    }

    public void DroppedObject()
    {
        if(!droppedObject)
        {
            droppedObject = true;
            step++;
            dialogue = new Story(dialogueStep[step].text);
            clickIcon.SetActive(true);
            DisplayNextLine();
            //EnableWorkspace();
            arrowIngredient.SetActive(false);
        }
    }

    private void DisableWorkspace()
    {
        workspace.SetActive(false);
    }

    private void EnableWorkspace()
    {
        workspace.SetActive(true);
    }

    private void StartStory()
    {
        dialogueBox.SetActive(true);
        dialogue = new Story(dialogueStep[step].text);
        DisableWorkspace();
        clickIcon.SetActive(true);
        DisplayNextLine();
        metrics = GameObject.Find("Metrics");
        metrics.GetComponent<RecordMetricsExample>().StartTimer();
    }
    public void DisplayNextLine()
    {
        if(!startedLevel)
        {
            if (dialogue.canContinue)
            {
                string text = dialogue.Continue();
                clickIcon.SetActive(true);
                text = text?.Trim();
                dialogueText.text = text;
            }
            else if (dialogue.currentChoices.Count > 0)
            {
                clickIcon.SetActive(false);
                DisplayChoices();
            }
            else if (!dialogue.canContinue)
            {
                clickIcon.SetActive(false);
                EnableWorkspace();
                if(!isRecipeTutorial)
                {
                    if (step == 0)
                    {
                        clickBlocker.DisableAllColliders();
                        clickBlocker.EnableCollider(GameObject.Find("Ice"));
                        arrowIce.SetActive(true);
                    }
                    if (pickedUpHolyWater && !usedTrashboy)
                    {
                        arrowTrashboy.SetActive(true);
                        clickBlocker.DisableAllColliders();
                        clickBlocker.EnableCollider(GameObject.Find("Trashboy"));
                    }
                    if (usedTrashboy && !droppedObject)
                    {
                        DisableWorkspace();
                        if(!displayedPearlArrow)
                        {
                            displayedPearlArrow = true;
                            arrowIngredient.SetActive(true);
                        }
                        clickBlocker.DisableAllColliders();
                        clickBlocker.EnableCollider(GameObject.Find("Pearls"));
                    }
                    if (step == dialogueStep.Count - 1 || wrongDrinkMade)
                    {
                        wrongDrinkMade = false;
                        dialogueBox.SetActive(false);
                        dialogueText.text = "";
                        clickIcon.SetActive(false);
                        if (step == dialogueStep.Count - 1)
                        {
                            startedLevel = true;
                            clickBlocker.EnableAllColliders();
                            GameObject.Find("RecipeController").GetComponent<RecipeList>().StartGame();
                        }
                    }
                }
                else
                {
                    if (step == 0)
                    {
                        foreach(GameObject ing in newIng)
                        {
                            Debug.Log("startsparkle");
                            ing.GetComponent<Ingredients>().StartSparkle();
                        }
                        step++;
                        dialogue = new Story(dialogueStep[step].text);
                        clickIcon.SetActive(true);
                    }
                    else if (step == 1)
                    {
                        step++;
                        dialogue = new Story(dialogueStep[step].text);
                        clickIcon.SetActive(true);
                        arrowBook.SetActive(true);
                    }
                    else if(step == 2)
                    {
                        arrowBook.SetActive(false);
                        GameObject.Find("Player").GetComponent<Player>().LoadNextLevel();
                    }
                }
            }
        }
        else
        {
            if (dialogue.canContinue)
            {
                if (workspace.activeInHierarchy == true)
                {
                    DisableWorkspace();
                }
                string text = dialogue.Continue();
                text = text?.Trim();
                dialogueText.text = text;
            }
            else if (dialogue.currentChoices.Count > 0)
            {
                clickIcon.SetActive(false);
                DisplayChoices();
            }
            else if (!dialogue.canContinue)
            {
                if (correctDrinkMade)
                {
                    dialogueBox.SetActive(false);
                    dialogueText.text = "";
                    clickIcon.SetActive(false);
                    GameObject.Find("Player").GetComponent<Player>().LoadNextLevel();
                }
                else if(wrongDrinkMade)
                {
                    dialogueBox.SetActive(false);
                    dialogueText.text = "";
                    clickIcon.SetActive(false);
                    EnableWorkspace();
                    clickBlocker.EnableAllColliders();
                    GameObject.Find("RecipeController").GetComponent<RecipeList>().ShowRecipe();
                    GameObject.Find("RecipeController").GetComponent<RecipeList>().ShowRecipe();
                }
                else
                {
                    EnableWorkspace();
                    clickBlocker.EnableAllColliders();
                    clickIcon.SetActive(false);
                }
            }
        }
    }
    Button CreateChoiceButton(string text)
    {
        // creates the button from a prefab
        var choiceButton = Instantiate(buttonPrefab);
        choiceButton.transform.SetParent(buttons.transform, false);

        // sets text on the button
        var buttonText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = text;

        return choiceButton;
    }

    void OnClickChoiceButton(Choice choice)
    {
        dialogue.ChooseChoiceIndex(choice.index);
        dialogue.Continue();
        RefreshChoiceView();
        clickIcon.SetActive(true);
        DisplayNextLine();
    }
    void RefreshChoiceView()
    {
        if (buttons != null)
        {
            foreach (var button in buttons.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
        }
    }
    private void DisplayChoices()
    {
        // checks if choices are already being displaye
        if (buttons.GetComponentsInChildren<Button>().Length > 0) return;

        for (int i = 0; i < dialogue.currentChoices.Count; i++)
        {

            var choice = dialogue.currentChoices[i];
            var button = CreateChoiceButton(choice.text);
            button.onClick.AddListener(() => OnClickChoiceButton(choice));
        }
    }
    void Start()
    {
        clickBlocker = GameObject.Find("ClickBlock").GetComponent<ClickBlock>();
        newIng = new List<GameObject>();
        StartStory();
    }

    public void WrongDrink()
    {
        wrongDrinkMade = true;
        clickBlocker.DisableAllColliders();
        dialogue = new Story(wrongDialogue.text);
        dialogueBox.SetActive(true);
        clickIcon.SetActive(true);
        GameObject.Find("RecipeController").GetComponent<RecipeList>().HideRecipe();
        DisplayNextLine();
    }

    public void RightDrink()
    {
        clickBlocker.DisableAllColliders();
        correctDrinkMade = true;
        dialogueBox.SetActive(true);
        dialogue = new Story(rightDialogue.text);
        clickIcon.SetActive(true);
        DisplayNextLine();
    }
}
