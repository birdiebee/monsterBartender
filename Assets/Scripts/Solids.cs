using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solids : MonoBehaviour
{
    private bool hasLiquid = false;
    [SerializeField]
    private GameObject workspace;

    [SerializeField]
    private GameObject floatSprite;

    private bool isDry = true;

    private bool isLerping = false;

    Vector3 lerpStart;
    Vector3 lerpEnd;

    float lerpTime = 0.0f;
    float lerpDuration = 0.6f;

    GameObject floating;

    private float dist;

    private float speed = 0.04f;

    public void SetWorkSpace(GameObject ws)
    {
        workspace = ws;
        if (workspace.GetComponent<Workspace>().CheckLiquids())
        {
            hasLiquid = true;
            isDry = false;
            Instantiate(floatSprite, gameObject.transform.parent.gameObject.transform);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasLiquid && workspace != null)
        {
            if(workspace.GetComponent<Workspace>().CheckLiquids())
            {
                hasLiquid = true;
            }
        }
        if(hasLiquid && isDry)
        {
            isDry = false;
            floating = Instantiate(floatSprite, gameObject.transform.parent.gameObject.transform);
            Color a = floating.GetComponent<SpriteRenderer>().color;
            a.a = 0f;
            floating.GetComponent<SpriteRenderer>().color = a;
            lerpStart = gameObject.transform.position;
            lerpEnd = floating.transform.position;
            dist = Vector3.Distance(lerpStart, lerpEnd);
            isLerping = true;
            //Destroy(gameObject);
        }
        if(isLerping)
        {
            lerpTime += Time.deltaTime;
            lerpTime = Mathf.Clamp(lerpTime, 0.0f, lerpDuration);
            float t = lerpTime / lerpDuration;
            transform.position = Vector3.Lerp(lerpStart, lerpEnd, t);
            if(lerpTime >= lerpDuration)
            {
                lerpTime = 0.0f;
                isLerping = false;
                Color a = floating.GetComponent<SpriteRenderer>().color;
                a.a = 1f;
                floating.GetComponent<SpriteRenderer>().color = a;
                Destroy(gameObject);
            }
        }
    }
}
