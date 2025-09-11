using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumObjectPooler : MonoBehaviour
{
    [Header("Pooling Properties")]
    [SerializeField] GameObject[] vacuumObjectPrefab;
    [SerializeField] Transform[] pooledVacuumObjectGroup; // Use this to organised the pooled object into a group 
    [SerializeField] private int amountToPoolPerObject; // Amount to pool per object
    private void Start()
    {
        for(int i = 0; i < vacuumObjectPrefab.Length; i++)
        {
            for(int j = 0; j < amountToPoolPerObject; j++)
            {
                GameObject vacuumObject = Instantiate(vacuumObjectPrefab[i], pooledVacuumObjectGroup[i]);
                vacuumObject.SetActive(false);
            }
        }
    }
    public GameObject EnableVacuumObject(int vacuumObjectArrIndex)
    {
        for(int i = 0; i < pooledVacuumObjectGroup[vacuumObjectArrIndex].childCount; i++)
        {
            if (pooledVacuumObjectGroup[vacuumObjectArrIndex].GetChild(i).gameObject.activeSelf == true)
            {
                continue;
            }
            else
            {
                return pooledVacuumObjectGroup[vacuumObjectArrIndex].GetChild(i).gameObject;
            }
        }
        return null;
    }
}
