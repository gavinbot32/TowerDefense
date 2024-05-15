using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class BuildHandler : MonoBehaviour
{
    public bool isBuilding;
    public GameObject towerBuilding;
    private TowerData towerData;
    [SerializeField] private Material buildMaterial;
    [SerializeField] private GameObject rangePrefab;
    private Transform rotatingPart;
    private UIManager uiManager;

    private GameObject previewTower;
    private Vector3 awayPosition;
    private PlacementTile tile;

    public LayerMask tileLayer;

    private PlayerInput playerInput;

    private float buildCooldown;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        awayPosition = new Vector3(100, -500, 100);
        uiManager = GetComponent<UIManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isBuilding = false;
    }

    private void Update()
    {
        if(buildCooldown > 0)
        {
            buildCooldown -= Time.deltaTime;
        }
        TileCastUpdate();

    }

    private void TileCastUpdate()
    {
        if (isBuilding)
        {
            bool overTile = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, tileLayer))
            {
                print(hit);
                if (hit.collider.GetComponent<PlacementTile>())
                {
                    if (hit.collider.GetComponent<PlacementTile>().tower == null)
                    {
                        overTile = true;
                        tile = hit.collider.GetComponent<PlacementTile>();
                    }
                }
            }

            if (overTile)
            {
                previewTower.transform.position = new Vector3(tile.transform.position.x,
                       tile.transform.position.y + previewTower.GetComponentInChildren<MeshRenderer>().bounds.size.y
                       , tile.transform.position.z);
                Transform nearestWaypoint = null;
                foreach(GameObject waypointGO in GameManager._instance._waypointManager.waypoints)
                {
                    Transform waypoint = waypointGO.transform;
                    if(nearestWaypoint == null)
                    {
                        nearestWaypoint = waypoint;
                    }
                    if (Vector3.Distance(previewTower.transform.position, waypoint.position) < Vector3.Distance(previewTower.transform.position, nearestWaypoint.position))
                    {
                        nearestWaypoint=waypoint;
                    }
                }
                rotatingPart.transform.LookAt(nearestWaypoint.transform.position);
            }
            else
            {
                previewTower.transform.position = awayPosition;
                tile = null;
            }
        }
        else
        {
            tile = null;
            towerData = null;
            rotatingPart = null;
            if (previewTower != null)
            {
                Destroy(previewTower);
                previewTower = null;
            }
        }
    }

    public void startBuilding(TowerData tower)
    {
        if (GameManager._instance.points < tower.cost) return;
        towerData = tower;
        playerInput.SwitchCurrentActionMap("Building");
        isBuilding = true;
        towerBuilding = tower.prefab;

        previewTower = Instantiate(tower.prefab, awayPosition, Quaternion.identity);
        foreach (MeshRenderer mr in previewTower.GetComponentsInChildren<MeshRenderer>())
        {
            mr.material = buildMaterial;
        }

        if (tower.type == TowerType.Ranged)
        {
            GameObject cylinder = Instantiate(rangePrefab, previewTower.GetComponent<Tower>().rotatingPart.transform.position, Quaternion.identity);
            cylinder.transform.localScale = new Vector3(tower.range, 0.2f, tower.range);
            cylinder.transform.SetParent(previewTower.transform);
        }

        rotatingPart = previewTower.GetComponent<Tower>().rotatingPart;


        foreach(MonoBehaviour x in previewTower.GetComponents<MonoBehaviour>())
        {
            Destroy(x);
        }
        uiManager.toggleBuildUI(true);
    }

    public void stopBuilding()
    {
        uiManager.toggleBuildUI(false);
        playerInput.SwitchCurrentActionMap("Main");
        isBuilding = false;
        towerBuilding=null;
        towerData = null;
        Destroy(previewTower);

    }

    public void placeBuilding()
    {
        if (buildCooldown > 0) return;
        if (isBuilding)
        {
            if (tile)
            {
                if (towerBuilding)
                {

                    Vector3 defaultAngles = rotatingPart.eulerAngles;

                    buildCooldown = 0.5f;
                    GameManager._instance.RemovePoints(towerData.cost);
                    GameObject x = Instantiate(towerBuilding, new Vector3(tile.transform.position.x,
                       tile.transform.position.y + towerBuilding.GetComponentInChildren<MeshRenderer>().bounds.size.y
                       , tile.transform.position.z), Quaternion.identity);
                    tile.tower = x.GetComponent<Tower>();
                    if(x.GetComponent<ProjectileLauncher>() != null)
                    {
                        x.GetComponent<ProjectileLauncher>().SetDefaultAngles(defaultAngles);
                    }
                    if (GameManager._instance.points < towerData.cost)
                    {
                        stopBuilding();
                    }

                }
            }
        }
    }

}
