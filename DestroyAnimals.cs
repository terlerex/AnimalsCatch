using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DestroyAnimals : AudioManager{

    public static bool Clickable = true;
    private const string TagEvilAnimal = "EvilAnimal";
    private Coroutine reScaleCoroutine;
    

    /// <summary>
    ///  Destroy the animal if it is clicked on and add to the score
    /// </summary>
    public void OnMouseDown()
    {
        if (Clickable && !ToggleMenu.isMenuOpen)
        {
            ItemAreaSpawner.currentAnimal--;
            Debug.Log(ItemAreaSpawner.currentAnimal);
            PlayRandomSound();
            reScaleCoroutine = StartCoroutine(ReScale(new Vector3(0.7f, 0.7f), new Vector3(0.1f, 0.1f)));
        }

            
        //If the animal is a raccoon, then the game is over
        if (gameObject.CompareTag(TagEvilAnimal) && GameManager.BInsaneMode && !ToggleMenu.isMenuOpen)
        {
            PlayRandomSound();
            GameManager.GameOver();
            ScoreManager.Instance.score--;
        }
    }

    /// <summary>
    ///  Destroy the animal and if the animal is a raccoon, destroy
    /// </summary>
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && Clickable && gameObject.CompareTag(TagEvilAnimal) && GameManager.BInsaneMode && !ToggleMenu.isMenuOpen)
        {
            ItemAreaSpawner.currentAnimal--;
            Debug.Log(ItemAreaSpawner.currentAnimal);
            PlaySwoochSound();
            reScaleCoroutine = StartCoroutine(ReScale(new Vector3(0.7f, 0.7f), new Vector3(0.1f, 0.1f)));
        }
    }
    
    /// <summary>
    ///  Rescale the animal to make it look like it is being destroyed
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private IEnumerator ReScale(Vector3 from, Vector3 to)
    {
        ScoreManager.Instance.score++;
        var collider = GetComponent<CapsuleCollider>();
        collider.enabled = false;
        
        float progress = 0;
        while (progress <= 1)
        {
            gameObject.transform.localScale = Vector3.Lerp(from, to, progress);
            progress += Time.deltaTime;
            yield return null;
        }
        gameObject.transform.localScale = to;
        Destroy(gameObject);
    }

}
   


