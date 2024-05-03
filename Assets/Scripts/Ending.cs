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

public class Ending : MonoBehaviour
{
    [SerializeField]
    private TextAsset endingStart;

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

    // Possible endings
    public TextAsset cursed;
    public TextAsset fairy;
    public TextAsset genie;
    public TextAsset gross;
    public TextAsset mermaid;
    public TextAsset poison;
    public TextAsset vanilla;

    // No liquid
    public TextAsset noLiquid;

    // Ending bools
    private bool deadEnding = false;
    private bool escapeEnding = false;
    private bool stayEnding = false;
    private bool monsterEnding = false;

    // Drink button
    [SerializeField]
    private GameObject drinkButton;

    private bool madeDrink = false;

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
        GameObject.Find("ClickBlock").GetComponent<ClickBlock>().DisableAllColliders();
        dialogueBox.SetActive(true);
        dialogue = new Story(endingStart.text);
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
            if(!madeDrink)
            {
                EnableWorkspace();
                GameObject.Find("ClickBlock").GetComponent<ClickBlock>().EnableAllColliders();
                drinkButton.SetActive(true);
                clickIcon.SetActive(false);
            }
            else
            {
                if(escapeEnding)
                {
                    SceneManager.LoadScene("EscapeEnding");
                    // Load escape scene
                }
                else if(deadEnding)
                {
                    SceneManager.LoadScene("DeathEnding");
                }
                else if(stayEnding)
                {
                    SceneManager.LoadScene("StayEnding");
                }
                else if (monsterEnding)
                {
                    SceneManager.LoadScene("MonsterEnding");
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
        StartStory();
    }

    private List<string> GetStringList(List<GameObject> list)
    {
        List<string> newList = new List<string>();
        for(int i = 0; i < list.Count; i++)
        {
            newList.Add(list[i].name);
        }
        return newList;
    }

    public void Drink()
    {
        GameObject.Find("ClickBlock").GetComponent<ClickBlock>().DisableAllColliders();
        if (!GameObject.Find("TriggerArea").GetComponent<Workspace>().CheckLiquids())
        {
            dialogue = new Story(noLiquid.text);
            clickIcon.SetActive(true);
            drinkButton.SetActive(false);
            DisplayNextLine();
        }
        else
        {
            madeDrink = true;
            List<GameObject> ingredients = GameObject.Find("TriggerArea").GetComponent<Workspace>().GetIngredients();
            List<string> ingredientNames = GetStringList(ingredients);
            if (ingredientNames.Contains("Death") || ingredientNames.Contains("Snake Venom"))
            {
                deadEnding = true;
                dialogue = new Story(poison.text);
                clickIcon.SetActive(true);
                drinkButton.SetActive(false);
                DisplayNextLine();
            }
            else if (ingredientNames.Contains("Genie"))
            {
                escapeEnding = true;
                dialogue = new Story(genie.text);
                clickIcon.SetActive(true);
                drinkButton.SetActive(false);
                DisplayNextLine();
            }
            else if (ingredientNames.Contains("Fairy Wings"))
            {
                stayEnding = true;
                dialogue = new Story(fairy.text);
                clickIcon.SetActive(true);
                drinkButton.SetActive(false);
                DisplayNextLine();
            }
            else if (ingredientNames.Contains("Mermaid Tears"))
            {
                stayEnding = true;
                dialogue = new Story(mermaid.text);
                clickIcon.SetActive(true);
                drinkButton.SetActive(false);
                DisplayNextLine();
            }
            else if (ingredientNames.Contains("Nile River Blood") || ingredientNames.Contains("Shaman Ashes") || ingredientNames.Contains("Demon Finger"))
            {
                monsterEnding = true;
                dialogue = new Story(cursed.text);
                clickIcon.SetActive(true);
                drinkButton.SetActive(false);
                DisplayNextLine();
            }
            else if (ingredientNames.Contains("Monkey Paw") || ingredientNames.Contains("Scarab Beetle") || ingredientNames.Contains("Eye Staff"))
            {
                monsterEnding = true;
                dialogue = new Story(gross.text);
                clickIcon.SetActive(true);
                drinkButton.SetActive(false);
                DisplayNextLine();
            }
            else
            {
                stayEnding = true;
                dialogue = new Story(vanilla.text);
                clickIcon.SetActive(true);
                drinkButton.SetActive(false);
                DisplayNextLine();
            }
        }
    }
}
