using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;  // Скорость движения
    [SerializeField] private int lives = 5;  // Количество жизней
    [SerializeField] private float jumpForce = 15f;  // Сила прыжка
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))  // Если нажимается кнопка для движения по горизонтали, то запускаем метод Run()
            Run();
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");  // Указываем направление

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);  // Задаем движение

        sprite.flipX = dir.x < 0.0f;  // Отзеркаливаем персонажа в направлении движения
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGrounded()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }
}
