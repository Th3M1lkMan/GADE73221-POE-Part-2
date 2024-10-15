using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Deffenders : MonoBehaviour
{
    public GameObject[] defenderPrefabs;
    public LayerMask greenTileLayer;
    public LayerMask riverLayer;
    public float placementOffset = 0.5f;
    public TextMeshProUGUI defenderNameText;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, greenTileLayer))
            {
                Vector3 hitPoint = hit.point;
                if (!IsOnRiver(hitPoint))
                {
                    hitPoint.y = TileHeight(hitPoint) + placementOffset;
                    PlaceRandomDefender(hitPoint);
                }
            }
        }
    }

    bool IsOnRiver(Vector3 position)
    {
        return Physics.Raycast(position + Vector3.up * 10, Vector3.down, out _, Mathf.Infinity, riverLayer);
    }

    float TileHeight(Vector3 position)
    {
        if (Physics.Raycast(position + Vector3.up * 10, Vector3.down, out RaycastHit hit, Mathf.Infinity, greenTileLayer))
        {
            return hit.point.y;
        }
        return 0;
    }

    void PlaceRandomDefender(Vector3 position)
    {
        int randomIndex = Random.Range(0, defenderPrefabs.Length);
        GameObject selectedDefender = defenderPrefabs[randomIndex];
        Instantiate(selectedDefender, position, Quaternion.identity);
        UpdateDefenderNameText(selectedDefender);
    }

    void UpdateDefenderNameText(GameObject defender)
    {
        defenderNameText.text = "Next Defender: " + defender.name;
    }
}