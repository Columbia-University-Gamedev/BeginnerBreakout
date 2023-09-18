using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public Vector2Int brickCount;
    public Vector2 padding;

    public GameObject brickPrefab;
    Transform tr;
    Collider2D c;
    
    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
        c = GetComponent<Collider2D>();
        Vector2 stepSize = (Vector2)(c.bounds.size) - (padding * 2);
        stepSize.x /= brickCount.x - 1;
        stepSize.y /= brickCount.y - 1;

        for(int i = 0; i < brickCount.x; i++)
        {
            for(int j = 0; j < brickCount.y; j++)
            {
                Vector2 position = (Vector2)(c.bounds.min) + padding;
                position += Vector2.Scale(stepSize, new Vector2(i, j));
                Instantiate(brickPrefab, position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
