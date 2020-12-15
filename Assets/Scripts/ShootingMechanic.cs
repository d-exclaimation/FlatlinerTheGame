//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using UnityEngine;

public class ShootingMechanic : MonoBehaviour {
    
    // Inspector public properties
    public bool startWithAmmo;
    public SpriteRenderer battery;
    public Transform ammoBar;
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
        ammoBar.localScale = new Vector3(ammoBar.localScale.x, (ammoCount * 0.5f / _startingAmmo), 1f);
        battery.color = new Color(1f, 1f, 1f, ammoCount <= 0 ? 0 : 1);
        
        // Check if there are ammo left, if not leave early
        if (ammoCount <= 0) return;
        if ((Input.GetKey(KeyCode.F) || Input.GetMouseButton(0)) && _timeTillFire < Time.time && _movement.isNormal()) {
            shoot(); // Shoot method
            
            // Apply fire rate delay and reduce ammo count
            _timeTillFire = Time.time + fireRate;
            ammoCount--;
        }
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
}
