using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab; 
    [SerializeField] private Vector3[] spawnPositions; 

    private void Start()
    {
        SpawnCubes();
    }

    private void SpawnCubes()
    {
        foreach (Vector3 position in spawnPositions)
        {
            Instantiate(cubePrefab, position, Quaternion.identity);
        }
    }
}
