using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    Transform tr;

    [Range(0, 10)]
    public float moveSpeed = 5;

    [Range(0, 20)]
    public float maxMoveRange;
    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        float moveDirection = 0;
        if(Input.GetKey(KeyCode.A))
        {
            moveDirection += -1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            moveDirection += 1;
        }

        Vector3 newPosition = tr.position;
        newPosition.x = math.clamp(newPosition.x + moveDirection* moveSpeed * Time.deltaTime, -maxMoveRange, maxMoveRange);

        tr.position = newPosition;
    }
}
