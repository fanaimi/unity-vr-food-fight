using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{

    [SerializeField] private GameObject targetArea;
    [SerializeField] private Target targetPrefab;
    [SerializeField] private Vector3 targetPos = new Vector3(-5.5f, 2.8f, 6.6f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnNewTarget()
    {
        // Debug.Log("yeah");
        
        if (Instantiate(targetPrefab, targetPos, Quaternion.Euler(90, 0, 0), targetArea.transform))
        {
            // Debug.Log("instantiated");
        }
    }

}
