using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    // Unity common datatypes:
    // Float - the building block of everything. Unity internally uses floats instead of doubles and expects you to use them too.
    // Vector2/Vector3/Vector4 - a vector made up of 2, 3, or 4 FLOATS. Variants exist for integers as well.
    // Quaternion - cursed math to represent rotations internally. Don't ever modify them directly; use the functions in the Quaternion class (read the documentation!)
    //      Note: to apply a rotation in the form of a Quaternion to a vector, you can do (Q * v) (if you know matrix math, think of quaternions as affine transformation matrices! (they're kind of not, but close enough))
    //      Note: in 2D, we can get away with a lot, because all our rotations will really just be on the Z axis only, so we can do a lot with Quaternion.Euler(0,0,z).
    
    // Unity common classes:
    // GameObject - an object in the scene, or a prefab. The most important class. Ever.
    // Transform - the second most important. Every GameObject has a Transform, which keeps track of the GameObject's spatial characteristics (position, rotation, scale)
    //      Note: transform.rotation is a Quaternion! transform.eulerAngles gets you what you see in the inspector.
    //      Note: We didn't talk about parenting, but you should look into parenting GameObjects/Transforms. It's used a lot!
    // MonoBehaviour - the base class of a script attached to a GameObject. In general, you will want to be more specific.
    //      For example, to change a variable on another GameObject: otherGameObject.GetComponent<BrickSpawner>().padding = new Vector2(1,1)
    // Collider(2D) - Note that neither Collider nor Collider2D exist as components that can directly be attached. However, it is the base class of all colliders,
    //                so generally you will say Collider in code rather than SphereCollider because we don't usually care about the shape of the collider from a code perspective.
    //      Note: Be VERY careful that you always use the appropriate dimensionality - 2D physics components DO NOT interact with 3D ones!
    public Vector2Int brickCount;
    public Vector2 padding;

    public GameObject brickPrefab;
    Transform tr;
    Collider2D c;
    
    // Start is called before the first frame update
    void Start()
    {
        // Most of the math here is nothing special.
        tr = transform;
        c = GetComponent<Collider2D>();
        // collider.bounds.size gets you the dimensions of the axis-aligned bounding box (AABB) of the collider. Don't think about it too hard.
        // Here, we use it to get the size of the rectangle we've attached this script to, which represents the area in which we want to spawn bricks.
        Vector2 stepSize = (Vector2)(c.bounds.size) - (padding * 2);
        stepSize.x /= brickCount.x - 1;
        stepSize.y /= brickCount.y - 1;

        for(int i = 0; i < brickCount.x; i++)
        {
            for(int j = 0; j < brickCount.y; j++)
            {
                Vector2 position = (Vector2)(c.bounds.min) + padding;
                position += Vector2.Scale(stepSize, new Vector2(i, j));
                // Instantiate is a VERY important function! In its most commonly used form, we pass a prefab (think blueprint), a position to spawn it at, and a rotation for it to spawn with.
                // Note that we're using Quaternion.identity to produce no rotation on the spawned object. This is equivalent to (but faster to execute than) Quaternion.Euler(0,0,0).
                // I highly recommend looking at the documentation to see what other forms this can be called in.
                Instantiate(brickPrefab, position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
