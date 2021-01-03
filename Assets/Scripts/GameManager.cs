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
    public GameObject pauseUI;
    
    private bool _hasEnded;
    private static readonly int Dead = Animator.StringToHash("dead");

    // You die 
   public void endGame() {
       // Check if game has not yet ended
       if (_hasEnded) return;
       deathUI.SetActive(true);
       _hasEnded = true;
       FindObjectOfType<PlayerMovement>().killSelf();
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

   public void togglePause() {
       if (_hasEnded || !pauseUI) return;
       
       pauseUI.SetActive(!pauseUI.activeSelf); 
       Time.timeScale = pauseUI.activeSelf ? 0 : 1;
   }
   
   public void quit() {
       Application.Quit();
   }
}
