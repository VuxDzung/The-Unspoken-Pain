using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [Header("Player state:")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float weightForce = 1f;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private PlayerController p_ctrl => GetComponent<PlayerController>();
    void Start()
    {
        
    }

    void Move()
    {
        rb.velocity = new Vector2(p_ctrl.MoveDir() * speed, -weightForce);
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
