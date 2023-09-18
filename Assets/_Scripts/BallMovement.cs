using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallMovement : MonoBehaviour
{
    [Range(0, 10)]
    public float StartingSpeed = 5f;

    [Range(0, 90)]
    public float PaddleBounceAngleRange = 70;

    Transform tr;

    Rigidbody2D rb;

    public bool UseDeterministicBounce = false;
    // Start is called before the first frame update
    void Start()
    {

        tr = transform;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(GetPaddleRandomBounce(), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Paddle"))
        {
            rb.velocity = Vector2.zero;
            if(UseDeterministicBounce)
            {
                float percent = (other.GetContact(0).point.x - other.transform.position.x) / other.collider.bounds.extents.x;
                rb.AddForce(GetPaddleDeterministicBounce(percent), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(GetPaddleRandomBounce(), ForceMode2D.Impulse);
            }
        } else if(other.gameObject.CompareTag("Breakable"))
        {
            Destroy(other.gameObject);
        }
    }

    Vector2 GetPaddleRandomBounce()
    {
        float angle = Random.Range(-PaddleBounceAngleRange, PaddleBounceAngleRange) + 90;
        Vector2 newVelocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        newVelocity *= StartingSpeed;
        return newVelocity;
    }

    Vector2 GetPaddleDeterministicBounce(float percent)
    {
        float angle = 90 + PaddleBounceAngleRange * -percent;
        Vector2 newVelocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        newVelocity *= StartingSpeed;
        return newVelocity;
    }
}
