using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next : MonoBehaviour
{
    [SerializeField]
    private GameObject _inkManager;

    [SerializeField]
    private bool isTutorial = false;

    [SerializeField]
    private bool isEnding = false;

    void Start()
    {
        if (_inkManager == null)
        {
            Debug.LogError("Ink Manager was not found!");
        }
    }

    public void OnMouseDown()
    {
        if(isTutorial)
        {
            _inkManager.GetComponent<Tutorial>().DisplayNextLine();
        }
        else if(isEnding)
        {
            _inkManager.GetComponent<Ending>().DisplayNextLine();
        }
        else
        {
            _inkManager.GetComponent<InkManager>().DisplayNextLine();
        }
    }
}
