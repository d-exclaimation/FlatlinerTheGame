//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using System;
using UnityEngine;

public class CrazyBot : MonoBehaviour {
    
    // Inspector variables
    public Transform player;
    public Rigidbody2D chargeBody;
    public float chargeRate = 1f;
    public SpriteRenderer spriteRenderer;
    public Sprite dead;
    
    // Private references
    private float _range = 10f;
    private float _timeTillMove;
    private int _superCharge;
    private Vector2 currentPos => transform.position;
    private float playerX => player.position.x;
    private float playerY => player.position.y;
    private float distanceX => Math.Abs(playerX - transform.position.x);
    private float distanceY => Math.Abs(playerY - transform.position.y);
    
    // Update is called once per frame
    private void Update() {
        if (distanceX >= _range || distanceY >= _range) return;

        if (!(_timeTillMove < Time.time)) return;
        if (_superCharge >= 5) { crazyCharge();}
        else { mildCharge(); }
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        
        // Reduce health point when hit with a bullet
        if (other.collider.CompareTag("Bullet")) {
            FindObjectOfType<AudioManager>().playSound(AudioManager.SoundEffect.HitMarker);
            killSelf();
        }
    }

    private void mildCharge() {
        chargeBody.velocity = new Vector2(0.5f * (playerX - currentPos.x), 0.5f * (playerY - currentPos.y));
        _timeTillMove = Time.time + chargeRate;
        _superCharge++;
    }

    private void crazyCharge() {
        chargeBody.velocity = new Vector2(2f * (playerX - currentPos.x), 3f * (playerY - currentPos.y));
        _timeTillMove = Time.time + chargeRate;
        _superCharge = 0;
    }
    
    public void killSelf() {
        // Change the sprite to a dead one, remove health bar, change tag, and disable this script
        spriteRenderer.sprite = dead;
        gameObject.tag = "Untagged";
        enabled = false;
    }
}
