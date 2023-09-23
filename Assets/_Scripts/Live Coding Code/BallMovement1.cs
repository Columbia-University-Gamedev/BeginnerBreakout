using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallMovement1 : MonoBehaviour
{
    public float startAngleRange = 45f;

    public float startSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(GetRandomVelocity(), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetRandomVelocity()
    {
        float angle = Random.Range(-startAngleRange, startAngleRange) + 90;
        Vector2 startVelocity = startSpeed * new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        return startVelocity;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Breakable"))
        {
            Destroy(other.gameObject);
        }
    }
}
