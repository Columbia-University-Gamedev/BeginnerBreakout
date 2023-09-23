using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    // A GameObject consists of:
    // GameObject
    //      Transform (access through gameObject.transform)
    //      Any other components (access through GetComponent<TYPE>())
    
    // A MonoBehaviour - a Unity script, like the one we're in right now - 
    // has access to:
    // gameObject -> the GameObject the script is attached to
    // transform -> the Transform of the GameObject the script is attached to
    // this -> the script itself
    
    // EVERY COMPONENT can call GetComponent<TYPE>() to get another component attached to the same GameObject.
    // For example, if we had the transform of another GameObject and wanted a Collider2D attached to it, we can do:
    // otherTransform.GetComponent<Collider2D>()

    // Read the documentation for GameObject to see what you can do!
    // https://docs.unity3d.com/ScriptReference/GameObject.html

    Transform tr;

    // The Range decorator makes your serialized numerical variables show up as sliders
    [Range(0, 10)]
    // Public variables, or those with the [SerializeField] decorator, are exposed in the Inspector
    // and can be edited directly from the editor on a per-instance basis. Very useful, such as for prototyping!
    public float moveSpeed = 5;

    [Range(0, 20)]
    public float maxMoveRange;

    // Awake runs before any Start() is run; like Start(), it's called once ever for each object.
    // Use it for things that NEED to happen first. Be aware that not everything might be properly initialized
    // in the scene when this is called.
    void Awake()
    {
        
    }
    
    
    // Start is called before the first frame update
    // Start() is called once ever for each object. Use it to do initialization work.
    void Start()
    {
        // Technically, it's good practice to cache the reference to your transform, even though every object has a reference to its transform.
        // This is because internally, writing "transform" actually calls GetComponent<Transform>() every time. I don't know why Unity did this.
        tr = transform;
    }

    // Update is called once per frame
    // Every object calls Update() once per frame, but the number of frames in a second is variable and depends on how performant
    // your game is. Use Time.deltaTime to minimize the effect of variable frame time on your game's performance.
    
    // Note that even if you're doing physics in LateUpdate(), input processing should be done in Update() to ensure things
    // still feel snappy and responsive. This is because Update() runs at a far faster rate than FixedUpdate().
    void Update()
    {
        float moveDirection = 0;
        
        // Note that Input.GetKey() is part of an old input system and is not the BEST solution but it is the EASIEST solution.
        // The best solution right now is the new Unity Input System, but there's a fair amount of overhead in setting it up.
        if(Input.GetKey(KeyCode.A))
        {
            moveDirection += -1;
        }
        // GetKey - returns true if the key is held, false if not.
        // GetKeyDown - returns true on the first frame the key is pressed, and false otherwise.
        // GetKeyUp - returns true on the first frame the key is released, and false otherwise.
        if(Input.GetKey(KeyCode.D))
        {
            moveDirection += 1;
        }

        Vector3 newPosition = tr.position;
        newPosition.x = math.clamp(newPosition.x + moveDirection* moveSpeed * Time.deltaTime, -maxMoveRange, maxMoveRange);

        tr.position = newPosition;
    }
    
    // FixedUpdate is more complicated. It runs a fixed number of times per second (default 25, i think) but it does NOT have a constant
    // time between its calls (though it emulates it somewhat; if you're interested, check out the documentation).
    // FixedUpdate should be used for physics especially; many a bug in real life games can be traced back to framerate-variable physics,
    // and FixedUpdate avoids that. Technically, our code in this file should be in FixedUpdate if we were to do things by the book.
    void FixedUpdate()
    {
        
    }
    
    // There are a few more functions of this form, like LateUpdate(), but besides Awake(), Start(), Update(), and FixedUpdate(), most are pretty niche.
    // If you're just starting out, you can get away with only worrying about Start() and Update() for now.
}
