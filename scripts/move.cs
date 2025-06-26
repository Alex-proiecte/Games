using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    private bool isMoving;
    private Vector2 input;
    public LayerMask objectlayer;
    public LayerMask interactivelayer;

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(horizontalInput) > Mathf.Epsilon || Mathf.Abs(verticalInput) > Mathf.Epsilon)
            {
                input = new Vector2(horizontalInput, verticalInput);
                var targetPos = (Vector2)transform.position + input;
                if (iswalkable(targetPos))
                    StartCoroutine(MoveToTarget(targetPos));
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            interact();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    private void interact()
    {
        var facingDir = input.normalized;
        var interactPos = transform.position + (Vector3)facingDir;
        //Debug.DrawLine(transform.position, interactPos, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactivelayer);
        if (collider != null)
        {
            collider.GetComponent<interactiv>().interact();
        }
    }

    private IEnumerator MoveToTarget(Vector2 targetPos)
    {
        isMoving = true;
        while (((Vector2)transform.position - targetPos).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private bool iswalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, objectlayer | interactivelayer) != null)
        {
            return false;
        }
        return true;
    }
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
