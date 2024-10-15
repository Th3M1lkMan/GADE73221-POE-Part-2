using System.Collections.Generic;
using UnityEngine;

public class Attackers : MonoBehaviour
{
    public float speed = 5f;
    public List<Vector3> path = new List<Vector3>();
    public int currentPathIndex = 0;

    public void Path(List<Vector3> newPath)
    {
        path = newPath;
    }

    void Update()
    {
        if (path.Count == 0) return;

        Vector3 targetPosition = path[currentPathIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPathIndex++;
            if (currentPathIndex >= path.Count)
            {
                Destroy(gameObject);
            }
        }
    }
}