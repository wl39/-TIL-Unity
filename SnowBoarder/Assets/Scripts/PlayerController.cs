using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float boostSpeed = 50f;
    [SerializeField] float baseSpeed = 30f;
    [SerializeField] float dragSpeed = 15f;

    new Rigidbody2D rigidbody2D = null;
    SurfaceEffector2D surfaceEffector2D;

    bool canMove = true;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) {
            RotatePlayer();
            RespondToBoost();
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody2D.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody2D.AddTorque(-torqueAmount);
        }
    }

    void RespondToBoost() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            surfaceEffector2D.speed = boostSpeed;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            surfaceEffector2D.speed = dragSpeed;
        } else {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    public void disableMove() {
        this.canMove = false;
    }
}
