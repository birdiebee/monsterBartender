using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{

    [SerializeField]
    private GameObject splash;

    [SerializeField]
    private GameObject gross;

    [SerializeField]
    private GameObject fillGlass;

    [SerializeField]
    private GameObject bookArrow;

    private GameObject splashVFX;

    private float splashTimer = 0.0f;

    private GameObject bookVFX;

    private GameObject stinkVFX;

    private GameObject fillVFX;

    private bool isSplashing = false;

    // Colors
    private Color grossColor;
    private Color bloodDeathMermaid;
    private Color bloodHolyDeath;
    private Color deathVenomMermaid;
    private Color venomHolyBlood;
    private Color mermaidHolyVenom;
    private Color venomDeathAmbrosia;
    private Color venomDeathBlood;

    public void MakeSplash(Color c, Transform t)
    {
        if(!isSplashing)
        {
            splashVFX = Instantiate(splash, t);
            splashVFX.GetComponent<SpriteRenderer>().color = c;
        }
        else if(isSplashing && splashVFX != null)
        {
            splashVFX.GetComponent<SpriteRenderer>().color = c;
        }
    }

    public void MakeFill(Color c, Transform t)
    {
        fillVFX = Instantiate(fillGlass, t);
        fillVFX.GetComponent<SpriteRenderer>().color = c;
    }

    public void MakeStink(Transform t)
    {
        stinkVFX = Instantiate(gross, t);
        GameObject.Find("TriggerArea").GetComponent<Workspace>().Stinks(true);
    }

    public void DestroyStink()
    {
        if(stinkVFX != null)
        {
            Destroy(stinkVFX);
        }
    }

    public void MakeBookArrow()
    {
        bookVFX = Instantiate(bookArrow);
    }

    public void DestroyBookArrow()
    {
        if(bookVFX != null)
        {
            Destroy(bookVFX);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        grossColor = new Color(70f/255f, 50f/255f, 10f/255f);
        bloodDeathMermaid = new Color(31f / 255f, 63f / 255f, 54f / 255f);
        bloodHolyDeath = new Color(77f / 255f, 50f / 255f, 50f / 255f);
        deathVenomMermaid = new Color(30f / 255f, 63f / 255f, 84f / 255f);
        venomHolyBlood = new Color(122f / 255f, 42f / 255f, 72f / 255f);
        mermaidHolyVenom = new Color(94f / 255f, 72f / 255f, 126f / 255f);
        venomDeathBlood = new Color(84f / 255f, 4f / 255f, 34f / 255f);
        venomDeathAmbrosia = new Color(115f / 255f, 56f / 255f, 34f / 255f);
    }

    public Color GetStinkColor()
    {
        return grossColor;
    }

    public Color Get3MixColor(List<string> ingList, Transform t)
    {
        if(ingList.Contains("Nile River Blood") && ingList.Contains("Death") && ingList.Contains("Mermaid Tears"))
        {
            return bloodDeathMermaid;
        }
        else if (ingList.Contains("Nile River Blood") && ingList.Contains("Death") && ingList.Contains("Holy Water"))
        {
            return bloodHolyDeath;
        }
        else if (ingList.Contains("Snake Venom") && ingList.Contains("Death") && ingList.Contains("Mermaid Tears"))
        {
            return deathVenomMermaid;
        }
        else if (ingList.Contains("Snake Venom") && ingList.Contains("Holy Water") && ingList.Contains("Nile River Blood"))
        {
            return venomHolyBlood;
        }
        else if (ingList.Contains("Snake Venom") && ingList.Contains("Holy Water") && ingList.Contains("Mermaid Tears"))
        {
            return mermaidHolyVenom;
        }
        else if (ingList.Contains("Snake Venom") && ingList.Contains("Death") && ingList.Contains("Ambrosia"))
        {
            return venomDeathAmbrosia;
        }
        else if (ingList.Contains("Snake Venom") && ingList.Contains("Death") && ingList.Contains("Nile River Blood"))
        {
            return venomDeathBlood;
        }
        else
        {
            MakeStink(t);
            return grossColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isSplashing && splashVFX == null)
        {
            isSplashing = false;
        }
    }
}
