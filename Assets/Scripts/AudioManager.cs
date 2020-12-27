//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update
    private void Awake() {
        for(var i = 0; i < sounds.Length; i++) {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
        }
    }

    private void playSound(string filename) {
        for (var i = 0; i < sounds.Length; i++) {
            if (sounds[i].name == filename) {
                sounds[i].source.Play();
                return;
            }
        }
    }

    public void playSound(SoundEffect effect) {
        switch (effect) {
            case SoundEffect.Jump:
                playSound("Jump");
                break;
            case SoundEffect.NextLevel:
                playSound("Restart");
                break;
            case SoundEffect.Fire:
                playSound("Firing");
                break;
            case SoundEffect.HitMarker:
                playSound("Hit");
                break;
            case SoundEffect.Recharge:
                playSound("Recharge");
                break;
            case SoundEffect.Death:
                playSound("Death");
                break;
            case SoundEffect.Explode:
                playSound("Explode");
                break;
            default:
                return;
        }
    }


    public enum SoundEffect {
        Jump,
        NextLevel,
        Fire,
        HitMarker,
        Recharge,
        Death,
        Explode
    }
}
