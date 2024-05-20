using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapThreeD : MonoBehaviour
{

    public Transform[] tiles;

    [SerializeField] private float snapTo;
    [SerializeField] private float offset;

    private void OnValidate()
    {
        if(tiles == null)
        {
            tiles = GetComponentsInChildren<Transform>();
        }

    }
    public void GetTiles()
    {
        tiles = GetComponentsInChildren<Transform>();
    }

    public void UpdatePositions()
    {
        
        foreach(Transform t in tiles)
        {
            if (t != transform)
            {
                float x = 0;
                float y = 0;
                float z = 0;
                if (t.position.x != 0) { x = ((Mathf.Round((t.position.x - offset) * snapTo)) / snapTo) + offset; }
                if (t.position.y != 0) { y = ((Mathf.Round((t.position.y - offset) * snapTo)) / snapTo) + offset; }
                if (t.position.z != 0) { z = ((Mathf.Round((t.position.z - offset) * snapTo)) / snapTo) + offset; }

                if(x == float.NaN) { x = 0; }
                if (y == float.NaN) { y = 0; }
                if (z == float.NaN) {  z = 0; }

                print(new Vector3(x, y, z));
                t.position = new Vector3(x,y,z);
            
            }
        }
    }
}
