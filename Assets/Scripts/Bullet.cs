using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletSpeed = 15f;
    public Rigidbody2D rb;

    private void FixedUpdate() {
        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnCollisionEnter2D() {
        Destroy(gameObject);
    }
}
