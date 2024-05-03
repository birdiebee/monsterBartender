using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBookHighlight : MonoBehaviour
{
    [SerializeField]
    private float focusSizeMultiplier = 1.2f;
    [SerializeField]
    private float focusVerticalOffset = 0.1f;
    private bool isFocused = false;
    private GameObject tooltip;
    // Start is called before the first frame update
    private void Start()
    {
        tooltip = gameObject.transform.Find("Tooltip").gameObject;
    }
    private void OnMouseOver()
    {
        if (!isFocused)
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
        if (isFocused)
        {
            tooltip.SetActive(false);
            gameObject.transform.localScale /= focusSizeMultiplier;
            Vector3 newPos = gameObject.transform.position;
            newPos.y -= focusVerticalOffset;
            gameObject.transform.position = newPos;
            isFocused = false;
        }
    }
}