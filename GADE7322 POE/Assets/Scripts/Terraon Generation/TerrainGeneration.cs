using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public int gridSize = 100;
    public float tileSize = 1.0f; 
    
    public GameObject greenTile; 
    public GameObject blueTile; 
    public GameObject settlement;
    
    public GameObject enemySpawnerObject; 

    public SpawnAttackers enemySpawner;
    
    public Vector2Int center;
    public List<Vector2Int> riverPositions = new List<Vector2Int>();
    public List<List<Vector3>> riverPaths = new List<List<Vector3>>(); 
    
    public void Start()
    {
        center = new Vector2Int(gridSize / 2, gridSize / 2); 
        SpawnTerrain();
        SpawnRivers();
       
        enemySpawner = enemySpawnerObject.GetComponent<SpawnAttackers>();
        if (enemySpawner != null)
        {
            enemySpawner.SetRiverPaths(riverPaths[0], riverPaths[1], riverPaths[2]); // Set paths for the spawner
        }
    }

    public void SpawnTerrain()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 position = new Vector3(x * tileSize, 0, y * tileSize);

                if (x == center.x && y == center.y)
                {
                    Instantiate(settlement, position, Quaternion.identity);
                }
                else
                {
                    Instantiate(greenTile, position, Quaternion.identity);
                }
            }
        }
    }

    public void SpawnRivers()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2Int start = GetRandomEdgePosition(); 
            List<Vector3> riverPath = new List<Vector3>(); 
            SpawnStraightRiver(start, riverPath);
            riverPaths.Add(riverPath); 
        }
    }

    public void SpawnStraightRiver(Vector2Int start, List<Vector3> riverPath)
    {
        Vector2Int current = start;

        while (current != center)
        {
            riverPositions.Add(current);
            Vector3 position = new Vector3(current.x * tileSize, 0, current.y * tileSize);
            Instantiate(blueTile, position, Quaternion.identity);

            riverPath.Add(position);

            if (current.x < center.x)
                current.x++;
            else if (current.x > center.x)
                current.x--;
            
            if (current.y < center.y)
                current.y++;
            else if (current.y > center.y)
                current.y--;
        }

        riverPositions.Add(center);
        riverPath.Add(new Vector3(center.x * tileSize, 0, center.y * tileSize)); // Add the center position
    }
    
    Vector2Int GetRandomEdgePosition()
    {
        int side = Random.Range(0, 4); 
        int position = Random.Range(0, gridSize);

        switch (side)
        {
            case 0: return new Vector2Int(position, 0);                
            case 1: return new Vector2Int(gridSize - 1, position);      
            case 2: return new Vector2Int(position, gridSize - 1);      
            case 3: return new Vector2Int(0, position);                
            default: return new Vector2Int(0, 0);                     
        }
    }
}