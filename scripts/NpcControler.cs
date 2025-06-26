using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcControler : MonoBehaviour, interactiv
{
    [SerializeField] Dialog dialog;
    public void interact()
    {
        StartCoroutine( Dialogmanager.Instance.ShowDialog(dialog));
    }
}
