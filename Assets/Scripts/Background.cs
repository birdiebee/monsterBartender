using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Player player;

    private void OnMouseDown()
    {
        if(player.IsHoldingObject())
        {
            player.ClickedNothing();
        }
    }
}
