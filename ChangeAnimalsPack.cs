using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimalsPack : MonoBehaviour
{
    [Header("Animals Pack")]
    public List<GameObject> animalsPack;
    
    private void Start()
    {
        var packIndex = PlayerPrefs.GetInt("PackIndex");
        ItemAreaSpawner.Instance.itemToSpread = animalsPack[packIndex];
    }
    
    /// <summary>
    /// Change the animals pack
    /// </summary>
    /// <param name="packIndex"></param>
    public void ChangePack(int packIndex)
    {
        ItemAreaSpawner.Instance.itemToSpread = animalsPack[packIndex];
        PlayerPrefs.SetInt("PackIndex", packIndex);
    }
    
    
}
