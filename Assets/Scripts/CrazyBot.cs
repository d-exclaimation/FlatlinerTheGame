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
    public GameObject explosive;
    public Animator animator;
    public float range = 10f;
    public float multiplier = .5f;
    [HideInInspector]
    public bool isDead;

    // Private references
    private float _timeTillMove;
    private int _superCharge;
    private float _delay = .75f;
    private string _lockOnBool = "lockedOn";
    private Vector2 currentPos => transform.position;
    private float playerX => player ? player.position.x : 0;
    private float playerY => player ? player.position.y : 0;
    private float distanceX => Math.Abs(playerX - transform.position.x);
    private float distanceY => Math.Abs(playerY - transform.position.y);
    
    private const float Compensation = 0.005f;
    
    // Update is called once per frame
    private void Update() {
        if (isDead || !player) return;
        animator.SetBool(_lockOnBool, isMoving());
        
        if (distanceX >= range || distanceY >= range) return;
        
        if (!(_timeTillMove < Time.time)) return;
        if (_superCharge >= 5) { crazyCharge();}
        else { mildCharge(); }
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (isDead) return;
        
        // Reduce health point when hit with a bullet
        if (other.collider.CompareTag("Bullet")) {
            FindObjectOfType<AudioManager>().playSound(AudioManager.SoundEffect.HitMarker);
            isDead = true;
            Invoke(nameof(killSelf), _delay);
        }
    }

    private void mildCharge() {
        chargeBody.velocity = new Vector2(multiplier * (playerX - currentPos.x), multiplier * (playerY - currentPos.y));
        transform.localScale = playerX - currentPos.x >= 0 ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f);
        _timeTillMove = Time.time + chargeRate;
        _superCharge++;
    }

    private void crazyCharge() {
        chargeBody.velocity = new Vector2(multiplier * 4 * (playerX - currentPos.x), multiplier * 4 * (playerY - currentPos.y));
        _timeTillMove = Time.time + chargeRate;
        _superCharge = 0;
    }
    
    public void killSelf() {
        FindObjectOfType<AudioManager>().playSound(AudioManager.SoundEffect.Explode);
        Instantiate(explosive, chargeBody.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private bool isMoving() {
        if (Math.Abs(chargeBody.velocity.y) >= Compensation) {
            return true;
        }

        if (Math.Abs(chargeBody.velocity.x) >= Compensation) {
            return true;
        }

        return false;
    }
}
