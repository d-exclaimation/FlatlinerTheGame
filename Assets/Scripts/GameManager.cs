//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // NOTE: Unity Accessible variables
    public float delay = 0.125f;
    public GameObject deathUI;
    public GameObject proceedUI;

    bool _hasEnded;
    
   // You die 
   public void endGame() {
       // Check if game has not yet ended
       if (_hasEnded) return;
       
       deathUI.SetActive(true);
       _hasEnded = true;
       FindObjectOfType<PlayerMovement>().enabled = false;
       FindObjectOfType<AudioManager>().playSound(AudioManager.SoundEffect.Death);
 
       // Restart the scene with delay
       Invoke(nameof(restart), delay);
   }

   
   // You pass
   public void completeGame() {
       // Check if player died
       if (_hasEnded) return;
       
       // Find all patrol scripts, and check them
       Patrol[] allScripts = FindObjectsOfType<Patrol>();
       foreach (var patrol in allScripts) {
           if (patrol.enabled) {
               return;
           }
       }
        
       
       // Find all bot scripts, and check them
       CrazyBot[] allBots = FindObjectsOfType<CrazyBot>();
       foreach (var bot in allBots) {
           if (bot.enabled) {
               return;
           }
       }
        
       // Check if there is a proceed UI, otherwise, just restart
       if (!proceedUI) {
           endGame();
           return;
       }
       
       proceedUI.SetActive(true);
       FindObjectOfType<AudioManager>().playSound(AudioManager.SoundEffect.NextLevel);
   }

   // Restart levels
   private void restart()
    {
        // Reload the scene and reactive the counter
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   // Load the menu
   public void loadMenu() {
       if (_hasEnded) return;
       
       // Load the first scene
       SceneManager.LoadScene(0);
   }
}
