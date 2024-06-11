using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementTile : MonoBehaviour
{
    public Tower tower;
    public int rotate;
    public GameObject highlightPrefab;
    public GameObject highlight;

    private void Start()
    {
        float meshBoundY = GetComponent<MeshRenderer>().bounds.size.y;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + meshBoundY / 2 + 0.075f, transform.position.z);
        highlight = Instantiate(highlightPrefab, pos, Quaternion.identity);
        highlight.SetActive(false);
    }

    private void FixedUpdate()
    {
        highlight.SetActive(GameManager._instance.GetComponent<BuildHandler>().isBuilding && tower == null);
    }
}
