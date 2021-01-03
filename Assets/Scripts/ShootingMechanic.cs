//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using UnityEngine;

public class ShootingMechanic : MonoBehaviour {
    
    // Inspector public properties
    public bool startWithAmmo;
    public SpriteRenderer battery;
    public SpriteRenderer ammoBar;
    public int ammoCount = 20;
    public float fireRate = 0.2f;
    public Transform gunPoint;
    public GameObject bulletPrefab;
    public AudioManager audioManager;
    
    // Private reference properties
    private float _timeTillFire;
    private PlayerMovement _movement;
    private int _startingAmmo;

    private void Start() {
        // Set variables as references
        _movement = gameObject.GetComponent<PlayerMovement>();
        _startingAmmo = ammoCount;
        if (startWithAmmo) return;
        ammoCount = 0;
    }

    private void Update() {
        // Change the battery pack charge depending on the current ammo
        changeAmmoPercentage();
        battery.color = new Color(1f, 1f, 1f, ammoCount <= 0 ? 0f : 1f);
        // Check if there are ammo left, if not leave early
        if (ammoCount <= 0) return;
        if (!Input.GetKey(shootKey) || !(_timeTillFire < Time.time) || !_movement.isNormal()) return;
        shoot(); // Shoot method
            
        // Apply fire rate delay and reduce ammo count
        _timeTillFire = Time.time + fireRate;
        ammoCount--;
    }

    private void shoot() {
        // Get the angle of the player
        var angle = _movement.isFacingRight ? 0f : 180f;
        
        // Create a bullet prefab from the gun point and angled in the correct direction
        Instantiate(bulletPrefab, gunPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        audioManager.playSound(AudioManager.SoundEffect.Fire);
    }

    public void reload() {
        ammoCount = _startingAmmo;
    }

    private void changeAmmoPercentage() {
        var percentage = (double) ammoCount * 100 / _startingAmmo;
        if (percentage <= 0) {
            ammoBar.color = new Color(1f, 1f, 1f, 0f);
        } else if (percentage <= 25f) {
            ammoBar.color = new Color(1f, 0f, 0.22f, 1f);
        } else if (percentage <= 50f) {
            ammoBar.color = new Color(1f, 0.55f, 0.02f, 1f);
        } else if (percentage <= 75f) {
            ammoBar.color = new Color(1f, 0.89f, 0.02f, 1f);
        }
        else {
            ammoBar.color = new Color(0.29f, 1f, 0.03f, 1f);
        }
    }

    public static KeyCode shootKey = KeyCode.F;
}
