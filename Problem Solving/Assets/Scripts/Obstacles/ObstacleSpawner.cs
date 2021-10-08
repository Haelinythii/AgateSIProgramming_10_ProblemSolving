using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private int initialObstacleCount = 3;

    private int spawnedObstacleCount = 0, maxObstacleCount;

    private void Start()
    {
        maxObstacleCount = initialObstacleCount;
    }


}
