//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start the game
    public void startGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Go to level select menu
    public void levelSelect() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(13);
    }
    
    // Quit App
    public void quit() {
        Application.Quit();
    }
}
