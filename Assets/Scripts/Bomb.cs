//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using System;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    
    public float explosiveSize = 20f;
    public GameObject explosion;
    public Sprite explosionEffect;

    private Vector3 currScale => transform.localScale;
    private bool _bombing;
    private SpriteRenderer _renderer;

    private void Start() {
        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update() {
        if (!_bombing) return;
        expand();

        if(currScale.x >= explosiveSize) Destroy(gameObject);
    }

    private void expand() {
        for (var i = 0; i < 3; i++) {
            transform.localScale = new Vector3(currScale.x * 1.1f, currScale.y * 1.1f);
            _renderer.color = new Color(1, 1, 1, _renderer.color.a  * 0.9f);
        }
    }

    private void createParticle() {
        FindObjectOfType<AudioManager>().playSound(AudioManager.SoundEffect.Explode);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        var sets = new HashSet<string> {"Player", "Enemy", "Bullet"};
        if (!sets.Contains(other.collider.tag)) return;
        _bombing = true;
        _renderer.sprite = explosionEffect;
        Invoke(nameof(createParticle), 0.1f);
    }
}
