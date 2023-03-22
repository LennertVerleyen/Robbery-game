using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class MoveCycle : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public float Speed = 1f;
    public int Size = 1;

    

    private void Start()
    {
        DataStore.Instance.LeftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        DataStore.Instance.RightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    private void Update()
    {
        if(direction.x > 0 && (transform.position.x - Size) > DataStore.Instance.RightEdge.x )
        {
            Vector3 position = transform.position;
            position.x = DataStore.Instance.LeftEdge.x - Size;
            transform.position = position;
        }
        else if (direction.x < 0 && (transform.position.x + Size) < DataStore.Instance.LeftEdge.x)
        {
            Vector3 position = transform.position;
            position.x = DataStore.Instance.RightEdge.x + Size;
            transform.position = position;
        }
        else
        {
            transform.Translate(direction * Speed * Time.deltaTime);
        }

    }
}
