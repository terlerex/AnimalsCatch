using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickItem : MonoBehaviour
{
    public GameObject[] itemsToPickFrom;

    private void Start()
    {
        InvokeRepeating(nameof(Pick), 0, 1f);
    }

    void Pick()
    {
        int randomIndex = Random.Range(0, itemsToPickFrom.Length);
        GameObject item = Instantiate(itemsToPickFrom[randomIndex], transform.position, Quaternion.identity);
    }
}
