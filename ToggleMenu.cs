using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public static bool isMenuOpen;
    public Canvas EscapeMenu;

    private void Start()
    {
        EscapeMenu.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isMenuOpen && GameManager._gameIsRunning && !GameManager._gameOvers)
        {
            MenuOpen();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isMenuOpen && GameManager._gameIsRunning && !GameManager._gameOvers)
        {
            MenuClose();
        }
    }

    /// <summary>
    /// Action when the menu is open
    /// </summary>
    public void MenuOpen()
    {
        Debug.Log("Menu Open");
        EscapeMenu.enabled = true;
        Time.timeScale = 0;
        DestroyAnimals.Clickable = false;
        isMenuOpen = true;
    }
    
    /// <summary>
    /// Action when the menu is closed
    /// </summary>
    public void MenuClose()
    {
        Debug.Log("Menu Closed");
        EscapeMenu.enabled = false;
        Time.timeScale = 1;
        DestroyAnimals.Clickable = true;
        isMenuOpen = false;
    }
    
    
}
