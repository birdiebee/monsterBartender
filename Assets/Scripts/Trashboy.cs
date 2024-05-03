using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashboy : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private AudioClip chomp;

    [SerializeField]
    private bool isTutorial = false;

    [SerializeField]
    private Sprite chompSprite;

    private Sprite normalSprite;

    bool tutorialComplete = false;

    private void Start()
    {
        normalSprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnMouseOver()
    {
        if(player.IsHoldingObject())
        {
            GetComponent<SpriteRenderer>().sprite = chompSprite;
        }
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = normalSprite;
    }

    private void OnMouseDown()
    {
        if(player.IsHoldingObject())
        {
            player.PlayAudio(chomp);
            GetComponent<SpriteRenderer>().sprite = normalSprite;
            if (player.GetHeldObject().tag == "Glass")
            {
                player.GetHeldObject().GetComponent<Workspace>().ResetGlass();
                if(isTutorial && !tutorialComplete)
                {
                    tutorialComplete = true;
                    GameObject.Find("Tutorial").GetComponent<Tutorial>().UsedTrashboy();
                    player.DoneTrashboy();
                }
            }
            else
            {
                player.DropObject(player.GetHeldObject());
            }
        }
    }
}
