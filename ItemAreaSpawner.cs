using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemAreaSpawner : MonoBehaviour
{

    public GameObject itemToSpread;
    public int numberItemsToSpawn = 20;
    public static int currentAnimal;
    
    public float itemXSpread = 10;
    public float itemYSpread = 0;
    public float itemZSpread = 10;
    
    //Singleton
    [NonSerialized] public static ItemAreaSpawner Instance;
    
    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    private void Start()
    {
        InvokeRepeating(nameof(SpreadItem), 0.5f, 0.4f);
    }

    /// <summary>
    ///  Spreads the item in the area defined by the itemXSpread, itemYSpread and itemZSpread variables
    /// </summary>
    private void SpreadItem()
    {
        if (currentAnimal <= numberItemsToSpawn && GameManager._gameIsRunning)
        {
            Vector3 randPosition = new Vector3(Random.Range(-itemXSpread, itemXSpread), Random.Range(-itemYSpread, itemYSpread), Random.Range(-itemZSpread, itemZSpread)) + transform.position;
            GameObject clone = Instantiate(itemToSpread, randPosition, Quaternion.identity);
            currentAnimal++;
            Debug.Log("Spawned " + currentAnimal + " of " + numberItemsToSpawn);
        }
    }
    
}
