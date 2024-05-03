using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject splashUSC;

    [SerializeField]
    private GameObject splashBG;

    private bool fadeIn = false;
    private bool fadeOut = false;
    private bool onScreen = false;

    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private float timeOnScreen = 3.0f;

    [SerializeField]
    private float splashTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Color alpha = splashUSC.GetComponent<SpriteRenderer>().color;
        alpha.a = 0;
        splashUSC.GetComponent<SpriteRenderer>().color = alpha;
        fadeIn = true;
    }

    private void FadeOutUSC()
    {
        fadeIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeIn)
        {
            Color alpha = splashUSC.GetComponent<SpriteRenderer>().color;
            float alphaVal = alpha.a;
            alphaVal += Time.deltaTime;
            alpha.a = alphaVal;
            splashUSC.GetComponent<SpriteRenderer>().color = alpha;
            if(alpha.a >= 1.0f)
            {
                fadeIn = false;
                onScreen = true;
            }
        }
        else if(onScreen)
        {
            splashTimer += Time.deltaTime;
            if(splashTimer >= timeOnScreen)
            {
                onScreen = false;
                fadeOut = true;
            }
        }
        else if (fadeOut)
        {
            Color alpha = splashUSC.GetComponent<SpriteRenderer>().color;
            float alphaVal = alpha.a;
            alphaVal -= Time.deltaTime;
            alpha.a = alphaVal;
            splashUSC.GetComponent<SpriteRenderer>().color = alpha;
            if (alpha.a <= 0.0f)
            {
                fadeOut = false;
                menu.SetActive(true);
                splashBG.SetActive(false);
                GameObject.Find("MusicManager").GetComponent<Music>().PlayMenuMusic();
            }
        }
    }
}
