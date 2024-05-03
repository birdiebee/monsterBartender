using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldObject : MonoBehaviour
{
    [SerializeField]
    private GameObject innerSprite;

    private GameObject liquid;

    public void SetColor(Color color)
    {
        liquid = Instantiate(innerSprite, gameObject.transform);
        liquid.GetComponent<SpriteRenderer>().color = color;
    }

    public void ResetLiquid()
    {
        Destroy(liquid);
    }
}
