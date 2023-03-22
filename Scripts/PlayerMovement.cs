using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Sprite deadSprite;
    public Sprite Winsprite;

    private void Awake()
    {
        DataStore.Instance.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Move(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            Move(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            Move(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            Move(Vector3.left);
        }


    }
    private void Move(Vector3 direction)
    {

        Vector3 destination = transform.position + direction;

        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));
        Collider2D Winpoint = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Winpoint"));



        if (barrier != null)
        {
            return;
        }
        if (platform != null)
        {
            transform.SetParent(platform.transform);
        }
        else
        {
            transform.SetParent(null);
        }
        if (obstacle != null && platform == null)
        {
            transform.position = destination;
            Death();
            SceneManager.LoadScene("GameOverScene");
        }
        else
        {
            StartCoroutine(Leap(destination));
        }
        if (Winpoint != null)
        {
            DataStore.Instance.spriteRenderer.sprite = Winsprite;
            SceneManager.LoadScene("WinScene");
        }

    }

    private IEnumerator Leap(Vector3 destination)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = destination;
    }

    private void Death()
    {
        transform.rotation = Quaternion.identity;
        DataStore.Instance.spriteRenderer.sprite = deadSprite;
        enabled = false;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") && transform.parent == null)
        {
            Death();
        }
    }
}
