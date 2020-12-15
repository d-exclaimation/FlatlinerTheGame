using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // NOTE:  Unity Accesible Variables
    public Rigidbody2D player;

    private void OnCollisionEnter2D(Collision2D other) {

        // Check what collide with the player
        switch (other.collider.tag) {
            case "JumpPad":
                var currentSpeed = player.velocity;
                player.velocity = new Vector2(currentSpeed.x, Math.Abs(currentSpeed.y) / 3 + 25);
                // Play audio
                FindObjectOfType<AudioManager>().playSound(AudioManager.SoundEffect.Jump);
                break;
            case "Goal":
                // Debug.Log("Win!!!");
                FindObjectOfType<GameManager>().completeGame();
                break;
            case "Enemy":
                player.velocity = new Vector2(-2 * (player.velocity.x - other.collider.attachedRigidbody.velocity.x), 10f);
                FindObjectOfType<GameManager>().endGame();
                break;
        }
    }
}
