using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steerSpeed = 1f;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float slowSpeed = 5f;
    [SerializeField] float boostSpeed = 25f;

    void OnCollisionEnter2D(Collision2D other)
    {
        this.moveSpeed = this.slowSpeed;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Boost") 
        {
            this.moveSpeed = this.boostSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
        
    }
}
