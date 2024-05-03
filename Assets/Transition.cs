using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer night;

    [SerializeField]
    private SpriteRenderer tarot;

    bool fadingInBG = false;
    bool fadingInTarot = false;
    bool finishedFading = false;

    public void StartTransition()
    {
        fadingInBG = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadingInBG)
        {
            Color ncolor = night.color;
            ncolor.a += Time.deltaTime * .5f;
            night.color = ncolor;
            if(ncolor.a >= 1)
            {
                fadingInBG = false;
                fadingInTarot = true;
            }
        }
        if(fadingInTarot)
        {
            Color ncolor = tarot.color;
            ncolor.a += Time.deltaTime * .5f;
            tarot.color = ncolor;
            if (ncolor.a >= 1)
            {
                fadingInTarot = false;
                finishedFading = true;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    private void OnMouseDown()
    {
        if(finishedFading)
        {
            GameObject.Find("Player").GetComponent<Player>().LoadNextLevel();
        }
    }
}
