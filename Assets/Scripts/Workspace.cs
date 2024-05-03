using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;

public class Workspace : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private List<GameObject> addedIngredients;

    [SerializeField]
    private GameObject ingredientParent;

    [SerializeField]
    private GameObject ingredientParentPrefab;

    [SerializeField]
    private Vector3 parentPosition;

    [SerializeField]
    private GameObject recipeDisplay;

    [SerializeField]
    private GameObject recipeController;

    [SerializeField]
    private bool isTutorial = false;

    private GameObject pouredSprite;

    private GameObject liquidSprite;

    private GameObject liquidFront;

    private Color currentColor;

    // Lerp color mixes

    private Color lerpBegin;

    private Color lerpEnd;

    private Color lerpedColor;

    private bool isLerping = false;

    private float lerpStatus;

    private float speed = 0.02f;

    private float lerpDuration = 0.4f;

    private float lerpTime = 0.0f;

    private int numColors = 0;

    private bool hasLiquid = false;

    private bool hasSolid = false;

    private bool isHeld = false;

    private bool stinks = false;

    private List<string> liquidNames;

    private VFX vfxController;

    [SerializeField]
    private float defaultAlpha = 0.7f;

    private void Awake()
    {
        liquidNames = new List<string>();
        addedIngredients = new List<GameObject>();
        parentPosition = ingredientParent.transform.position;
        vfxController = GameObject.Find("VFXController").GetComponent<VFX>();
    }

    private void OnMouseDown()
    {
        if(player.IsHoldingObject())
        {
            GameObject newIngredient = player.GetHeldObject();
            if(newIngredient == gameObject)
            {
                player.DropObject(gameObject);
            }
            else if(!CheckIngredient(newIngredient))
            {
                AddIngredient(newIngredient);
                InstantiateSprite(newIngredient);
                player.DropObject(player.GetHeldObject());
                if(newIngredient.name == "Ice" && isTutorial)
                {
                    GameObject.Find("Tutorial").GetComponent<Tutorial>().AddedIce();
                }
                if(newIngredient.name == "Holy Water" && isTutorial)
                {
                    GameObject.Find("Tutorial").GetComponent<Tutorial>().AddedHolyWater();
                }
            }
            else
            {
                GameObject.Find("MusicManager").GetComponent<Music>().PlayWrongSound();
                Debug.Log("Ingredient already added.");
            }
        }
        else
        {
            isHeld = true;
            player.HoldObject(gameObject);
        }
    }

    private bool CheckIngredient(GameObject ingredient)
    {
        return addedIngredients.Contains(ingredient);
    }

    public void MixLiquid(Color c)
    {
        liquidSprite.GetComponent<SpriteRenderer>().color = c;
        Color b = c;
        b.a = 0.1f;
        liquidFront.GetComponent<SpriteRenderer>().color = b;
    }

    private void AddIngredient(GameObject ingredient)
    {
        addedIngredients.Add(ingredient);
    }

    public List<GameObject> GetIngredients()
    {
        return addedIngredients;
    }

    public bool CheckLiquids()
    {
        return hasLiquid;
    }

    public bool CheckSolids()
    {
        return hasSolid;
    }

    public void ChangeLiquidColor(Color c)
    {
        liquidSprite.GetComponent<SpriteRenderer>().color = c;
        Color b = c;
        b.a = 0.1f;
        liquidFront.GetComponent<SpriteRenderer>().color = b;
    }

    private void InstantiateSprite(GameObject ingredient)
    {
        player.PlayAudio(ingredient.GetComponent<Ingredients>().GetDropNoise());
        if (ingredient.tag == "Liquid")
        {
            if(!CheckLiquids())
            {
                currentColor = ingredient.GetComponent<Ingredients>().GetColor();
                pouredSprite = ingredient.GetComponent<Ingredients>().GetPouredSprite();
                currentColor.a = defaultAlpha;
                liquidSprite = Instantiate(pouredSprite, ingredientParent.transform);
                liquidSprite.GetComponent<SpriteRenderer>().color = currentColor;
                currentColor.a = 0.3f;
                liquidFront = Instantiate(pouredSprite, ingredientParent.transform);
                liquidFront.GetComponent<SpriteRenderer>().color = currentColor;
                liquidFront.GetComponent<SpriteRenderer>().sortingOrder = 98;
                numColors++;
                hasLiquid = true;
                liquidNames.Add(ingredient.name);
            }
            else
            {
                // Lerp color change
                liquidNames.Add(ingredient.name);
                lerpBegin = currentColor;
                numColors++;
                if(numColors == 2)
                {
                    currentColor += ingredient.GetComponent<Ingredients>().GetColor();
                    currentColor /= numColors;
                    currentColor.a = defaultAlpha;
                }
                else if(numColors == 3)
                {
                    currentColor = vfxController.Get3MixColor(liquidNames, ingredientParent.transform);
                }
                else if(numColors == 4 && !stinks)
                {
                    vfxController.MakeStink(ingredientParent.transform);
                    currentColor = vfxController.GetStinkColor();
                    stinks = true;
                }
                lerpEnd = currentColor;
                isLerping = true;

                
                /*liquidSprite.GetComponent<SpriteRenderer>().color = currentColor;
                currentColor.a = 0.1f;
                liquidFront.GetComponent<SpriteRenderer>().color = currentColor;*/
            }
        }
        else
        {
            GameObject newSprite = Instantiate(ingredient.GetComponent<Ingredients>().GetPouredSprite(), ingredientParent.transform);
            if(ingredient.tag == "Solid")
            {
                if (!CheckSolids()) hasSolid = true;
                newSprite.GetComponent<Solids>().SetWorkSpace(gameObject);
                if(hasLiquid)
                {
                    vfxController.MakeSplash(currentColor, ingredientParent.transform);
                }
            }
        }
    }

    public void Stinks(bool b)
    {
        stinks = b;
    }
    public void IsDropped()
    {
        isHeld = false;
        ingredientParent.transform.position = parentPosition;
    }

    public List<GameObject> GetAddedIngredients()
    {
        return addedIngredients;
    }

    public void ResetGlass()
    {
        player.DropObject(gameObject);
        liquidNames.Clear();
        Destroy(ingredientParent);
        ingredientParent = Instantiate(ingredientParentPrefab, gameObject.transform);
        addedIngredients.Clear();
        Destroy(liquidSprite);
        numColors = 0;
        hasLiquid = false;
        hasSolid = false;
        stinks = false;
    }
    private void Update()
    {
        if (isHeld)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            ingredientParent.transform.position = mousePosition;
        }
        if(isLerping)
        {
            lerpTime += Time.deltaTime;
            lerpTime = Mathf.Clamp(lerpTime, 0.0f, lerpDuration);
            float t = lerpTime / lerpDuration;
            lerpedColor = Color.Lerp(lerpBegin, lerpEnd, t);
            MixLiquid(lerpedColor);
            if(lerpTime >= lerpDuration)
            {
                lerpTime = 0.0f;
                isLerping = false;
            }
        }
    }

    public void VerifyRecipe()
    {
        if (recipeController.GetComponent<RecipeList>().VerifyRecipe(addedIngredients))
        {
            ResetGlass();
        }
    }
}
