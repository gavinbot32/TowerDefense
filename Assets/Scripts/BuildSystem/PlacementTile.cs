using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementTile : MonoBehaviour
{
    public Tower tower;
    public int rotate;
    public GameObject highlightPrefab;
    public GameObject highlight;

    private void Update()
    {
        if (GameManager._instance.GetComponent<BuildHandler>().isBuilding && tower == null)
        {
            if (highlight == null)
            {
                float meshBoundY = GetComponent<MeshRenderer>().bounds.size.y;
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + meshBoundY / 2 + 0.075f, transform.position.z);
                highlight = Instantiate(highlightPrefab, pos, Quaternion.identity);
            }
        }
        else if (highlight != null && GameManager._instance.GetComponent<BuildHandler>().isBuilding == false)
        {
            Destroy(highlight);
            highlight = null;
        }

        if (tower != null && highlight != null)
        {
            Destroy(highlight);
            highlight = null;
        }
    }
}
