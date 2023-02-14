using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int speed;
    Vector3 direction;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    
    private void FixedUpdate() {
        Move(direction);
    }
    void Move(Vector3 direction){
        rb.MovePosition(transform.position + direction*speed*Time.fixedDeltaTime);
    }
}
