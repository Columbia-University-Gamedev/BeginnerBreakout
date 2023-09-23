using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner1 : MonoBehaviour
{
    public Vector2Int BricksToSpawn;

    public GameObject BrickPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < BricksToSpawn.x; i++)
        {
            for(int j = 0; j < BricksToSpawn.y; j++)
            {
                Vector3 newPosition = new Vector3(i * 1.5f, j * 1.5f, 0);
                Instantiate(BrickPrefab, newPosition, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
