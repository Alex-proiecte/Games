using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gamestate { FreeRoam, Chest}
public class gamecontroller : MonoBehaviour
{
    [SerializeField] Move Move;

    gamestate state;
    private void Start()
    {
        Dialogmanager.Instance.onShowDialog += () =>
        {
            state = gamestate.Chest;
        };
        Dialogmanager.Instance.onHideDialog += () =>
        {
            if(state == gamestate.Chest)
            {
                state = gamestate.FreeRoam;
            }
        };
    }

    private void Update()
    {
        if(state == gamestate.FreeRoam)
        {
            Move.HandleUpdate();
        }else if(state == gamestate.Chest)
        {
            Dialogmanager.Instance.HandleUpdate();
        }
    }
}
