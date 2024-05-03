using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableDialogue : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueController;

    private void OnMouseDown()
    {
        dialogueController.GetComponent<Dialogue>().Clicked(gameObject);
    }
}
