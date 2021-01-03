//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using System;
using UnityEngine;

public class GhostBot : MonoBehaviour {
    
    public float range = 20f;
    public float moveSpeed = 10f;
    public Rigidbody2D body;
    
    // Private properties
    private GameObject _target;
    private float _mx;
    private float _my;
    private Vector3 targetPos => _target != null ? _target.transform.position : Vector3.zero;
    private float deltaX => Math.Abs(targetPos.x - transform.position.x);
    private float deltaY => Math.Abs(targetPos.y - transform.position.y);

    private void Start() {
        _target = FindObjectOfType<PlayerMovement>().gameObject;
    }

    private void Update() {
        if(!_target) return;

        var position = transform.position;
        _mx = targetPos.x >= position.x ? 1f : -1f;
        _my = targetPos.y >= position.y ? 1f : -1f;

    }

    private void FixedUpdate() {
        if (deltaX >= range || deltaY >= range) return;

        var movement = new Vector2(_mx * moveSpeed * Time.deltaTime, _my * moveSpeed * Time.deltaTime);
        body.velocity = movement;

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Bullet")) {
            Destroy(gameObject);
        } 
    }
}
