using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGlass : MonoBehaviour
{
    [SerializeField]
    private GameObject workspace;

    [SerializeField]
    private GameObject tutorialInk;

    [SerializeField]
    private Player player;

    private bool hasIce;

    private void OnMouseDown()
    {
        GameObject newIngredient = player.GetHeldObject();
        Debug.Log(newIngredient.name);
        if (newIngredient.name == "Ice" && !hasIce)
        {
            tutorialInk.GetComponent<Tutorial>().AddedIce();
            hasIce = true;
        }
    }
}
