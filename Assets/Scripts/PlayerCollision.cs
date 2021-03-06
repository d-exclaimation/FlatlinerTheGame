﻿//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // NOTE:  Unity Accessible Variables
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
                if (isEnemyDead(other.gameObject)) return;
                FindObjectOfType<GameManager>().endGame();
                break;
        }
    }

    private bool isEnemyDead(GameObject other) {
        CrazyBot bot = other.GetComponent<CrazyBot>();
        if (bot) {
            return bot.isDead;
        }

        return false;
    }
}
