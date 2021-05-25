using UnityEngine;

public class Hero : Entity
{
    [SerializeField] private float speed = 3f;  // Скорость движения
    [SerializeField] private int lives = 5;  // Количество жизней
    [SerializeField] private float jumpForce = 15f;  // Сила прыжка
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (isGrounded) State = States.idle; // Состояние покоя

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
        if (isGrounded) State = States.run;

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

        if (!isGrounded) State = States.jump;
    }

    public override void GetDamage()
    {
        lives -= 1;
        Debug.Log(lives);
    }
}

public enum States
{
    idle,
    run,
    jump
}