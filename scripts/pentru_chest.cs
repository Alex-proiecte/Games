using System;
using UnityEngine;

public class pentru_chest : MonoBehaviour
{
    private Animator animator;
    private Dialogmanager dialogManager;
    private bool isInteractKeyPressed = false;
    private bool shouldClose = false; // Proprietate pentru a urmări dacă acțiunea "inchide" trebuie declanșată
    public event Action OnInteractAction;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogManager = Dialogmanager.Instance;
        dialogManager.OnInteractKeyPressed += OnInteractKeyPressed;
    }

    private void Update()
    {
        if (isInteractKeyPressed)
        {
            if (shouldClose)
            {
                animator.SetTrigger("deschide");
                shouldClose = false; 
            }
            else
            {
                animator.SetTrigger("inchide");
            }

            OnInteractAction?.Invoke();
            isInteractKeyPressed = false;
        }
    }

    private void OnDestroy()
    {
        dialogManager.OnInteractKeyPressed -= OnInteractKeyPressed;
    }

    private void OnInteractKeyPressed()
    {
        isInteractKeyPressed = true;
        shouldClose = true;
    }
}
