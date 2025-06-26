using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogmanager : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] Text instructiuni;

    [SerializeField] int lettersPerSecond;
    public event Action onShowDialog;
    public event Action onHideDialog;
    public event Action OnInteractKeyPressed;

    public static Dialogmanager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    Dialog dialog;
    int currentLine = 0;
    bool isTyping;

    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();
        onShowDialog?.Invoke();
        this.dialog = dialog;
        inventory.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isTyping)
        {
            ++currentLine;
            if (currentLine < dialog.Lines.Count)
            {
                OnInteractKeyPressed?.Invoke();
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                inventory.SetActive(false);
                currentLine = 0;
                onHideDialog?.Invoke();
            }
        }
    }

    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        instructiuni.text = "";
        foreach (var letter in line.ToCharArray())
        {
            instructiuni.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }
}
