using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField]
    private AudioSource deathAudio;
    [SerializeField]
    private AudioClip deathClip;
    // Start is called before the first frame update
    void Start()
    {
        deathAudio.PlayOneShot(deathClip);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
