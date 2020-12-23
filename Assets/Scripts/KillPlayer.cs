//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using UnityEngine;

public class KillPlayer : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<GameManager>().endGame();
        } else if (other.CompareTag("Enemy")) {
            killObject(other.gameObject);
        }
    }

    private void killObject(GameObject other) {
        Patrol patrol = other.GetComponent<Patrol>();
        if (patrol) {
            patrol.killSelf();
            return;
        }

        CrazyBot bot = other.GetComponent<CrazyBot>();
        if (bot) {
            bot.killSelf();
        }
    }
}
