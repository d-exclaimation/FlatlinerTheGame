using UnityEngine;

public class Recharge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        
        // Check if the pack triggers collide with the player
        if (other.CompareTag("Player")) {
            
            // Refill the current ammo of player with the references
            other.GetComponent<ShootingMechanic>().reload();
            FindObjectOfType<AudioManager>().playSound(AudioManager.SoundEffect.Recharge);
            Destroy(gameObject);
        }
    }
}
