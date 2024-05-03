using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    [SerializeField]
    public List<string> ingredients;

    [SerializeField]
    private GameObject recipeSprite;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private string recipeName;

    private int numIngredients;

    private void Start()
    {
        player = GameObject.Find("Player");
        if(recipeName == null)
        {
            recipeName = recipeSprite.name;
        }
        numIngredients = ingredients.Count;
        
    }

    public int GetPatienceModifier()
    {
        Debug.Log("numing" + ingredients.Count);
        return ingredients.Count;
    }

    public string GetRecipeName()
    {
        return recipeName;
    }
    public bool CheckRecipeCorrectness(List<GameObject> drinkIngredients)
    {
        int ingredientCount = 0;
        for(int i = 0; i < drinkIngredients.Count; i++)
        {
            Debug.Log(drinkIngredients[i].name);
            // Check if ingredient in cup 
            if (!ingredients.Contains(drinkIngredients[i].name))
            {
                return false;
            }
            ingredientCount++;
        }
        if (ingredientCount != ingredients.Count)
        {
            return false;
        }
        return true;
    }

    public GameObject GetRecipeSprite()
    {
        return recipeSprite;
    }

    public List<string> GetRecipeStrings()
    {
        return ingredients;
    }
}
