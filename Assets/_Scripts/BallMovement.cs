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
        
        // Unlike Transform, other components are not guaranteed to be on a GameObject
        // so they need to be fetched with GetComponent<TYPE>() to get a reference to them.
        // Note that any component can use component.gameObject to get a reference back to the GameObject that owns it.
        rb = GetComponent<Rigidbody2D>();
        
        // Impulses apply immediate acceleration for a one-off change in velocity. The default behavior of AddForce
        // is to apply a force as you normally learned in physics, applying a constant acceleration.
        // Physics generally works the way you expect it to.
        rb.AddForce(GetPaddleRandomBounce(), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // The OnCollisionEnter(2D) callback is called when a rigidbody(2D) starts a collision with another collider. The information about the collision
    // is stored in the variable in the argument.
    // Variations of this function include OnCollisionStay and OnCollisionExit. There are also corresponding versions of this for trigger colliders, which
    // are a bit out of scope for this right now.
    void OnCollisionEnter2D(Collision2D other)
    {
        // Tags on GameObjects are a simple way to carry more information in a lightweight way. Each GameObject can have up to one tag. Use CompareTag rather than string comparison - it's faster
        // (i think some stuff is done at compile time for this stuff to make it fast?)
        if(other.gameObject.CompareTag("Paddle"))
        {
            rb.velocity = Vector2.zero;
            if(UseDeterministicBounce)
            {
                // This is out of scope for now. don't worry about it too much
                float percent = (other.GetContact(0).point.x - other.transform.position.x) / other.collider.bounds.extents.x;
                rb.AddForce(GetPaddleDeterministicBounce(percent), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(GetPaddleRandomBounce(), ForceMode2D.Impulse);
            }
        } else if(other.gameObject.CompareTag("Breakable"))
        {
            // Destroy destroys ANYTHING (not just GameObjects). If we did Destroy(this), it would destroy this instance of a BallMovement script. If we did Destroy(this.gameObject), it would destroy the ball
            // and everything attached to it, including this script.
            // Optionally, we can call Destroy after a delay with Destroy(Object target, float delay).
            Destroy(other.gameObject);
        }
    }

    Vector2 GetPaddleRandomBounce()
    {
        // Note that we add 90 so that our cone faces up instead of right.
        float angle = Random.Range(-PaddleBounceAngleRange, PaddleBounceAngleRange) + 90;
        // Unity math uses radians. Use Mathf.Deg2Rad as a constant to convert degrees to radians.
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
