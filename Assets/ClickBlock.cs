using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBlock : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> colliderObjects;
    // Start is called before the first frame update

    private void Start()
    {

    }
    public void DisableAllColliders()
    {
        foreach(GameObject g in colliderObjects)
        {
            if(g.GetComponent<Collider2D>() != null)
            {
                g.GetComponent<Collider2D>().enabled = false;
            }
            foreach(Transform child in g.transform)
            {
                child.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void EnableAllColliders()
    {
        foreach (GameObject g in colliderObjects)
        {
            if (g.GetComponent<Collider2D>() != null)
            {
                g.GetComponent<Collider2D>().enabled = true;
            }
            foreach (Transform child in g.transform)
            {
                child.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    public void EnableCollider(GameObject g)
    {
        g.GetComponent<Collider2D>().enabled = true;
    }

    public void DisableCollider(GameObject g)
    {
        g.GetComponent<Collider2D>().enabled = false;
    }
}
