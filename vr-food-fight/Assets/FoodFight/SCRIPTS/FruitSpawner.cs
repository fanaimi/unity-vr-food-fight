using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] fruitPrefabs = new GameObject[9];
    public Transform spawnParent;
    
    
    public void SpawnNewFruit(Vector3 originalFruitPosition)
    {
        // Debug.Log("fruit spawning");
        int randomIndex = Random.Range(0, fruitPrefabs.Length);
        
        Instantiate(
            fruitPrefabs[randomIndex],
            originalFruitPosition,
            Quaternion.identity,
            spawnParent
       );

    }
}
