using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    GameObject recipeController;

    [SerializeField]
    GameObject buttonNice;
    [SerializeField]
    GameObject buttonMean;
    [SerializeField]
    GameObject dialogueBox;
    [SerializeField]
    GameObject clickableButton;
    [SerializeField]
    GameObject blockingSprite;

    [SerializeField]
    GameObject workspace;

    [SerializeField]
    List<Sprite> dialogueTree;

    [SerializeField]
    Sprite hint;

    [SerializeField]
    Sprite wrong;

    [SerializeField]
    Sprite meanAnswer;

    [SerializeField]
    Sprite niceAnswer;

    int dialogueIndex = 0;
    int dialogueCount;

    bool dialogueClickable = false;

    // Sound

    [SerializeField]
    List<AudioClip> voiceClips;

    [SerializeField]
    private GameObject player;

    private void Start()
    {
        dialogueBox.SetActive(false);
        buttonMean.SetActive(false);
        buttonNice.SetActive(false);
        blockingSprite.SetActive(true);
        clickableButton.SetActive(false);
        dialogueCount = dialogueTree.Count - 1;
        workspace.SetActive(false);
    }

    public void StartDialogue()
    {
        dialogueBox.SetActive(true);
        clickableButton.SetActive(true);
        dialogueBox.GetComponent<SpriteRenderer>().sprite = dialogueTree[dialogueIndex];
        dialogueClickable = true;
        PlayRandomSound();
    }

    private void PlayRandomSound()
    {
        player.GetComponent<Player>().StopAudio();
        int randomNum = Random.Range(0, voiceClips.Count);
        player.GetComponent<Player>().PlayAudio(voiceClips[randomNum]);
    }

    public void WrongDrink()
    {
        dialogueBox.GetComponent<SpriteRenderer>().sprite = wrong;
        blockingSprite.SetActive(true);
        dialogueClickable = true;
        PlayRandomSound();
        clickableButton.SetActive(true);
    }

    public void RightDrink()
    {
        dialogueIndex++;
        dialogueBox.GetComponent<SpriteRenderer>().sprite = dialogueTree[dialogueIndex];
        dialogueClickable = true;
        PlayRandomSound();
        clickableButton.SetActive(true);
    }

    public void Clicked(GameObject g)
    {
        if(g == buttonMean)
        {
            buttonNice.SetActive(false);
            buttonMean.SetActive(false);
            PlayRandomSound();
            dialogueBox.GetComponent<SpriteRenderer>().sprite = meanAnswer;
            dialogueIndex++;
        }
        else if(g == buttonNice)
        {
            buttonNice.SetActive(false);
            buttonMean.SetActive(false);
            PlayRandomSound();
            dialogueBox.GetComponent<SpriteRenderer>().sprite = niceAnswer;
            dialogueIndex++;
        }
        else if((g == dialogueBox || g == clickableButton) && dialogueClickable)
        {
            if(dialogueBox.GetComponent<SpriteRenderer>().sprite == wrong)
            {
                PlayRandomSound();
                dialogueBox.GetComponent<SpriteRenderer>().sprite = hint;
            }
            else if(dialogueBox.GetComponent<SpriteRenderer>().sprite == meanAnswer || dialogueBox.GetComponent<SpriteRenderer>().sprite == niceAnswer)
            {
                PlayRandomSound();
                dialogueBox.GetComponent<SpriteRenderer>().sprite = dialogueTree[dialogueIndex];
            }
            else if(dialogueBox.GetComponent<SpriteRenderer>().sprite == hint)
            {
                dialogueClickable = false;
                clickableButton.SetActive(false);
                blockingSprite.SetActive(false);
            }
            else if(dialogueIndex == dialogueCount)
            {
                dialogueBox.SetActive(false);
                blockingSprite.SetActive(false);
                clickableButton.SetActive(false);
                recipeController.GetComponent<RecipeList>().DialogueOver();
            }
            else if(dialogueIndex == 0)
            {
                buttonMean.SetActive(true);
                buttonNice.SetActive(true);
            }
            else if(dialogueIndex == 4)
            {
                blockingSprite.SetActive(false);
                clickableButton.SetActive(false);
                workspace.SetActive(true);
            }
            else
            {
                dialogueIndex++;
                PlayRandomSound();
                dialogueBox.GetComponent<SpriteRenderer>().sprite = dialogueTree[dialogueIndex];
            }
        }
    }
}
