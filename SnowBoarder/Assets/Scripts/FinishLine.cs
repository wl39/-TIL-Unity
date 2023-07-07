using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float loadDelay = 1.2f;
    [SerializeField] ParticleSystem finishEffect;

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            finishEffect.Play();
            GetComponent<AudioSource>().Play();

            Invoke("reloadScene", loadDelay);
        }
    }

    void reloadScene() {
        SceneManager.LoadScene(0);
    }
}
