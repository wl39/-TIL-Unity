using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmashDetector : MonoBehaviour
{
    [SerializeField] float loadDelay = 0.5f;
    [SerializeField] ParticleSystem smashEffect;
    [SerializeField] AudioClip crashSFX;

    bool hasCrashed = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground" && !hasCrashed) {
            hasCrashed = true;
            FindObjectOfType<PlayerController>().disableMove();
            smashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Invoke("reloadScene", loadDelay);
        }
    }

    void reloadScene() {
        SceneManager.LoadScene(0);
    }
}
