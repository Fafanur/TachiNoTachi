using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb;

    [SerializeField]
    private float _force = 5;
    private HealthComponent myHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<HealthComponent>();
    }

    void FixedUpdate()
    {
        Vector2 mov = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            mov += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            mov += -Vector2.right;
        }

        if (Input.GetKey(KeyCode.S))
        {
            mov += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            mov += Vector2.right;
        }

        rb.AddForce(mov * _force);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ItemComponent item = collision.gameObject.GetComponent<ItemComponent>();
        if (item != null)
        {
            item.PickedUp();
        }
    }
}
