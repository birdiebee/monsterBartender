using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPlayer : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private bool pickedUpIce = false;

    private void Start()
    {
        player = gameObject.GetComponent<Player>();
    }
}
