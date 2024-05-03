using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float monsterPatience;

    [SerializeField]
    private GameObject recipeController;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private List<AudioClip> voiceClips;

    [SerializeField]
    private AudioClip impatientSound;

    [SerializeField]
    private AudioClip killSound;

    [SerializeField]
    private string killScene;

    [SerializeField]
    private Sprite angrySprite;

    private Sprite happySprite;

    private bool isAngry = false;

    public string GetKillScene()
    {
        return killScene;
    }

    public void SwapAngrySprite()
    {
        GetComponent<SpriteRenderer>().sprite = angrySprite;
    }

    public void SwapHappySprite()
    {
        GetComponent<SpriteRenderer>().sprite = happySprite;
    }

    public List<AudioClip> GetVoice()
    {
        return voiceClips;
    }

    public AudioClip GetImpatientSound()
    {
        return impatientSound;
    }

    public AudioClip GetKillSound()
    {
        return killSound;
    }

    void Start()
    {
        happySprite = GetComponent<SpriteRenderer>().sprite;
        recipeController = gameObject.transform.parent.gameObject;
        player = recipeController.GetComponent<RecipeList>().GetPlayer();
    }

    public float GetPatience()
    {
        return monsterPatience;
    }

    private void OnMouseDown()
    {
        if(player.GetComponent<Player>().IsHoldingObject())
        {
            GameObject heldObject = player.GetComponent<Player>().GetHeldObject();
            if (heldObject.tag == "Glass")
            {
                List<GameObject> ingredients = heldObject.GetComponent<Workspace>().GetAddedIngredients();
                if (recipeController.GetComponent<RecipeList>().VerifyRecipe(ingredients))
                {
                    player.GetComponent<Player>().DropObject(heldObject);
                    heldObject.GetComponent<Workspace>().ResetGlass();
                }
                else
                {
                    player.GetComponent<Player>().DropObject(heldObject);
                }
            }
        }
    }
}
