//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    // Play the selected level
    public void selectLevel(int index) {
        SceneManager.LoadScene(index);
    }
    
    // Go Back to the Main Menu
    public void mainMenu() {
        SceneManager.LoadScene(0);
    }
}
