using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPrefab : MonoBehaviour
{
    public GameObject prefabToRespawn;  // Reference to the prefab you want to respawn
    public Transform respawnPoint;      // The position where the prefab should respawn
    private GameObject currentPrefabInstance;  // Reference to the current instance in the scene

    
    // Method to respawn the prefab
    public void RespawnPrefab1()
    {
        // Check if there is an existing instance in the scene
        if (currentPrefabInstance != null)
        {
            // Destroy the existing instance before respawning
            Destroy(currentPrefabInstance);
        }

        // Check if the prefabToRespawn is not null
        if (prefabToRespawn != null)
        {
            // Instantiate a new instance of the prefab at the respawn point
            currentPrefabInstance = Instantiate(prefabToRespawn, respawnPoint.position, respawnPoint.rotation);

            // You might want to do additional setup for the new instance if needed
            // For example, reset any state or variables on the prefab

            // Optional: Set the new instance as a child of the respawn manager (or another appropriate parent)
            currentPrefabInstance.transform.parent = transform;
        }
        else
        {
            Debug.LogError("Prefab to respawn is not assigned in the inspector!");
        }
    }
}


