using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{

    [SerializeField] private GameObject targetArea;
    [SerializeField] private Target targetPrefab;
    [SerializeField] private Vector3 targetPos = new Vector3(-5.5f, 2.8f, 6.6f);


    public void WaitToSpawn()
    {
        // Debug.Log("waitTospawn");
        Invoke("SpawnNewTarget", 2.5f);
    }


    private void SpawnNewTarget()
    {
        // Debug.Log("yeah");
        if (GameManager.Instance.playing == true)
        {
            if (Instantiate(targetPrefab, targetPos, Quaternion.Euler(90, 0, 0), targetArea.transform))
                    {
                        //Debug.Log("instantiated");
                    }
        } // playing

        
    }

}
