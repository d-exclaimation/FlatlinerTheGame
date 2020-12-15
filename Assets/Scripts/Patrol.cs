﻿using UnityEngine;

public class Patrol : MonoBehaviour {
    
    // Inspector Public properties
    public float patrolSpeed = 2f;
    public Rigidbody2D patrolBody;
    public LayerMask hitLayer;
    public Transform checker;
    public Transform healthBar;
    public SpriteRenderer spriteRenderer;
    public Sprite dead;
    
    
    // Private reference properties
    private bool _isFacingRight = true;
    private RaycastHit2D _hit2D;
    private float _compensation = 0.05f;
    private int _health = 5;

    private void Update() {
        _hit2D = Physics2D.Raycast(checker.position, -transform.up, 1f, hitLayer);
        
        // Kill self whenever health reaches 0 or less
        if (_health <= 0) {
            killSelf();
            return;
        }
        
        // Adjust health bar accordingly
        healthBar.localScale = new Vector3(_health / 5f, 0.1f, 1f);
    }

    private void FixedUpdate() {
        
        // If object is stuck try flipping
        if (patrolBody.velocity.x < _compensation && patrolBody.velocity.x > -_compensation) {
            flipPatrol();
        }
        
        // Check if the checker is still colliding with the ground
        if (_hit2D.collider) {
            patrolling();
        }
        else {
            flipPatrol();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        // Reduce health point when hit with a bullet
        if (other.collider.CompareTag("Bullet")) {
            FindObjectOfType<AudioManager>().playSound(AudioManager.SoundEffect.HitMarker);
            _health -= 1;
        }
    }

    private void flipPatrol() {
        // Change the variable saying the face has flipped, and flip the character
        _isFacingRight = !_isFacingRight;
        patrolBody.transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
    }

    private void patrolling() {
        // Check whether it is facing right or left, and move accordingly
        if (_isFacingRight) {
            patrolBody.velocity = new Vector2(patrolSpeed, patrolBody.velocity.y);
        }
        else {
            patrolBody.velocity = new Vector2(-patrolSpeed, patrolBody.velocity.y);
        }
    }

    public void killSelf() {
        // Change the sprite to a dead one, remove health bar, change tag, and disable this script
        spriteRenderer.sprite = dead;
        healthBar.localScale = new Vector3(0, 0, 0);
        gameObject.tag = "Untagged";
        enabled = false;
    }
    
}
