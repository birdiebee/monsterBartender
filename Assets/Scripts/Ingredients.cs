using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
    // Follow mouse
    private Vector3 mousePosition;
    private bool inWorkArea = false;

    [SerializeField]
    private Player player;
    private bool isHeld = false;
    private GameObject heldObject = null;
    private string objectTag;

    // Sprites
    [SerializeField]
    private Color liquidColor;
    [SerializeField]
    private GameObject heldSprite;
    [SerializeField]
    private GameObject pouredSprite;

    // Audio
    [SerializeField]
    private AudioClip pickUpNoise;

    [SerializeField]
    private AudioClip dropNoise;

    // RecipeController
    [SerializeField]
    private RecipeList recipeController;

    [SerializeField]
    private bool isNew = false;

    [SerializeField]
    private GameObject sparklePrefab;

    private GameObject sparkleObj;

    private float sparkleTime = 3.0f;

    /*
    [SerializeField]
    private Vector3 normalScale;

    [SerializeField]
    private Vector3 focusScale;

    [SerializeField]
    private Vector3 normalPos;

    [SerializeField]
    private Vector3 focusPos;
    */

    [SerializeField]
    private float focusSizeMultiplier = 1.2f;

    //[SerializeField]
    private float focusVerticalOffset = 0.1f;

    private GameObject tooltip;

    private bool isFocused = false;

    [SerializeField]
    private bool isRecipeTutorial;

    public void StartSparkle()
    {
        sparklePrefab = Resources.Load<GameObject>("SparkleFX.prefab");
        sparkleObj = Instantiate(sparklePrefab, gameObject.transform);
        sparkleObj.transform.position = gameObject.transform.position;
        if (gameObject.name == "Genie")
        {
            Vector3 newPos = new Vector3(0.494f, -0.171f, 0);
            sparkleObj.transform.localPosition = newPos;
        }
        if (gameObject.name == "Ambrosia")
        {
            Vector3 newPos = new Vector3(0, -0.173f, 0);
            sparkleObj.transform.localPosition = newPos;
        }
        if (gameObject.name == "Sake")
        {
            Vector3 newPos = new Vector3(-0.007f, -0.15f, 0);
            sparkleObj.transform.localPosition = newPos;
        }
        isRecipeTutorial = false;
    }

    public AudioClip GetDropNoise()
    {
        return dropNoise;
    }
    private void Start()
    {
        if(isNew && !isRecipeTutorial)
        {
            sparklePrefab = Resources.Load<GameObject>("SparkleFX");
            sparkleObj = Instantiate(sparklePrefab, gameObject.transform);
            sparkleObj.transform.position = gameObject.transform.position;
            if (gameObject.name == "Genie")
            {
                Vector3 newPos = new Vector3(0.494f, -0.171f, 0);
                sparkleObj.transform.localPosition = newPos;
            }
            if (gameObject.name == "Ambrosia")
            {
                Vector3 newPos = new Vector3(0, -0.173f, 0);
                sparkleObj.transform.localPosition = newPos;
            }
            if (gameObject.name == "Sake")
            {
                Vector3 newPos = new Vector3(-0.007f, -0.15f, 0);
                sparkleObj.transform.localPosition = newPos;
            }
        }
        tooltip = gameObject.transform.Find("Tooltip").gameObject;
        recipeController = GameObject.Find("RecipeController").GetComponent<RecipeList>();
        player = GameObject.Find("Player").GetComponent<Player>();
        objectTag = gameObject.tag;
    }

    private void OnMouseOver()
    {
        if(!isFocused)
        {
            tooltip.SetActive(true);
            gameObject.transform.localScale *= focusSizeMultiplier;
            Vector3 newPos = gameObject.transform.position;
            newPos.y += focusVerticalOffset;
            gameObject.transform.position = newPos;
            isFocused = true;
        }
    }

    private void OnMouseExit()
    {
        if(isFocused)
        {
            tooltip.SetActive(false);
            gameObject.transform.localScale /= focusSizeMultiplier;
            Vector3 newPos = gameObject.transform.position;
            newPos.y -= focusVerticalOffset;
            gameObject.transform.position = newPos;
            isFocused = false;
        }
    }
    void OnMouseDown()
    {
        if(!recipeController.GetIsPaused())
        {
            if (player.IsHoldingObject())
            {
                if (player.GetHeldObject() == gameObject)
                {
                    player.DropObject(gameObject);
                }
                else
                {
                    isHeld = true;
                    player.DropObject(player.GetHeldObject());
                    player.HoldObject(gameObject);
                    SpawnHeldSprite();
                }
            }
            else
            {
                isHeld = true;
                player.HoldObject(gameObject);
                SpawnHeldSprite();
            }
        }
    }

    public void IsDropped()
    {
        isHeld = false;
        Destroy(heldObject);
        heldObject = null;
    }

    public void SpawnHeldSprite()
    {
        Destroy(heldObject);
        heldObject = Instantiate(heldSprite, player.transform);
        if(objectTag == "Liquid")
        {
            heldObject.GetComponent<HeldObject>().SetColor(liquidColor);
        }
        player.PlayAudio(pickUpNoise);
    }

    public Color GetColor()
    {
        return liquidColor;
    }

    private void Update()
    {
        if (isHeld && heldObject != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
            heldObject.transform.position = mousePosition;
        }
        if(isNew && sparkleTime > 0.0f && !isRecipeTutorial)
        {
            sparkleTime -= Time.deltaTime;
            if(sparkleTime <= 0.0f)
            {
                Destroy(sparkleObj);
            }
        }
    }
    public void MouseInWorkArea(bool b)
    {
        inWorkArea = b;
    }

    public GameObject GetPouredSprite()
    {
        return pouredSprite;
    }
}
