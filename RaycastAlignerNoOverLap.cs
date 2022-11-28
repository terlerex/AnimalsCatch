using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RaycastAlignerNoOverLap : MonoBehaviour
{
    public GameObject[] AnimalsToPickFrom;
    [SerializeField] private float raycastDistance = 100f;
    [SerializeField] private float overlapTestBoxSize = 1f;
    [SerializeField] private LayerMask spawnedObjectLayer;
    
    private void Start()
    {
        PositionRayCast();
    }

    /// <summary>
    /// This method will cast a ray from the top of the screen and spawn an object at the point of impact.
    /// </summary>
    private void PositionRayCast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            Vector3 overlapTextBoxScale = new Vector3(overlapTestBoxSize, overlapTestBoxSize, overlapTestBoxSize);
            Collider[] collidersInsideOverlapBox = new Collider[1];
            var numberOfCollidersFound = Physics.OverlapBoxNonAlloc(hit.point, overlapTextBoxScale,
                collidersInsideOverlapBox, spawnRotation, spawnedObjectLayer);

            if (numberOfCollidersFound == 0)
            {
                Debug.Log("Spawned");
                Pick(hit.point, spawnRotation);
            }
            else
            {
                Debug.Log("Name of collider 0 found: " + collidersInsideOverlapBox[0].name);
                ItemAreaSpawner.currentAnimal--;
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Picks a random animal from the array and spawns it at the given position and rotation
    /// </summary>
    /// <param name="positionToSpawn"></param>
    /// <param name="rotationToSpawn"></param>
    private void Pick(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        var randomIndex = Random.Range(0, AnimalsToPickFrom.Length);
        var animal = Instantiate(AnimalsToPickFrom[randomIndex], positionToSpawn, rotationToSpawn);
    }
}

