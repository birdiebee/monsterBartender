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
using System.Threading;

public class InkManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkJson;
    private Story dialogue;

    [SerializeField]
    private GameObject dialogueBox;

    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private VerticalLayoutGroup buttons;

    [SerializeField]
    private Button buttonPrefab;

    private bool correctDrinkMade = false;
    private bool wrongDrinkMade = false;

    [SerializeField]
    private GameObject clickIcon;

    [SerializeField]
    private GameObject recipeController;

    [SerializeField]
    private TextAsset killDialogue;

    [SerializeField]
    private TextAsset wrongDialogue;

    [SerializeField]
    private TextAsset rightDialogue;

    [SerializeField]
    private GameObject workspace;

    [SerializeField]
    private GameObject metrics;

    private bool killed = false;

    private bool canCheckKilled = true;

    [SerializeField]
    private bool isTutorial = false;

    private ClickBlock clickBlocker;

    public void DisableWorkspace()
    {
        workspace.SetActive(false);
    }

    public void EnableWorkspace()
    {
        workspace.SetActive(true);
    }

    private void StartStory()
    {
        dialogueBox.SetActive(true);
        dialogue = new Story(inkJson.text);
        DisableWorkspace();
        clickIcon.SetActive(true);
        DisplayNextLine();
        metrics = GameObject.Find("Metrics");
        metrics.GetComponent<RecordMetricsExample>().StartTimer();
    }
    public void DisplayNextLine()
    {
        if (dialogue.canContinue)
        {
            clickBlocker.DisableAllColliders();
            if (workspace.activeInHierarchy == true)
            {
                DisableWorkspace();
            }
            string text = dialogue.Continue();
            text = text?.Trim();
            dialogueText.text = text;
            recipeController.GetComponent<RecipeList>().PlayRandomSound();
        }
        else if (dialogue.currentChoices.Count > 0)
        {
            clickIcon.SetActive(false);
            DisplayChoices();
        }
        else if(!dialogue.canContinue)
        {
            if(canCheckKilled && !killed && (bool)dialogue.variablesState["kill"] == true)
            {
                killed = true;
                dialogue = new Story(killDialogue.text);
                DisplayNextLine();
            }
            else if(killed)
            {
                metrics.GetComponent<RecordMetricsExample>().Killed();
                metrics.GetComponent<RecordMetricsExample>().EndTimer();
                //GameObject.Find("Player").GetComponent<Player>().Kill();
                GameObject.Find("DeathController").GetComponent<DeathController>().LoadKillScene(recipeController.GetComponent<RecipeList>().GetMonster(), GameObject.Find("Player").GetComponent<Player>().GetDeathLevel());
            }
            else if(correctDrinkMade)
            {
                recipeController.GetComponent<RecipeList>().DialogueOver();
                dialogueBox.SetActive(false);
                dialogueText.text = "";
                clickIcon.SetActive(false);
                EnableWorkspace();
                clickBlocker.EnableAllColliders();
            }
            else
            {
                clickBlocker.EnableAllColliders();
                EnableWorkspace();
                clickIcon.SetActive(false);
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
        StartStory();
    }

    public void WrongDrink()
    {
        Debug.Log("wrong");
        canCheckKilled = false;
        wrongDrinkMade = true;
        dialogue = new Story(wrongDialogue.text);
        clickIcon.SetActive(true);
        DisplayNextLine();
    }

    public void RightDrink()
    {
        canCheckKilled = false;
        correctDrinkMade = true;
        dialogue = new Story(rightDialogue.text);
        clickIcon.SetActive(true);
        DisplayNextLine();
    }
}
