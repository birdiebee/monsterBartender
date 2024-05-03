using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    private GameObject parent;

    [SerializeField]
    private List<GameObject> addedIngredients;

    private void Awake()
    {
        parent = transform.parent.gameObject;
        addedIngredients = new List<GameObject>();

    }

    public GameObject GetParent()
    {
        return parent;
    }
}
