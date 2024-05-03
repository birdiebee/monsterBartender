using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookKill : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer = 0.0f;

    private bool jumped = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f && !jumped)
        {
            jumped = true;
            Color c = gameObject.GetComponent<SpriteRenderer>().color;
            c.a = 1;
            gameObject.GetComponent<SpriteRenderer>().color = c;
        }
        if (timer >= 2.0f && jumped)
        {
            GameObject.Find("DeathController").GetComponent<DeathController>().LoadDeathScene();
        }
    }
}
