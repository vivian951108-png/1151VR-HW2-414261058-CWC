using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Vector2：人物移動方向
    public Vector2 moveDirection = new Vector2(1, 0);

    // 移動速度
    public float moveSpeed = 5f;

    // Vector2：跳躍力量
    public Vector2 jumpPower = new Vector2(0, 8);

    private Rigidbody2D rb;

    // 是否抵達終點
    private bool reachedEnd = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 已經到終點
        if (reachedEnd)
        {
            return;
        }
        // 往前移動

        Vector2 velocity = rb.linearVelocity;

        velocity.x = moveDirection.x * moveSpeed;

        rb.linearVelocity = velocity;
        // Space 跳躍
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            jumpPower.y
        );
    }

    // 碰到其他物體

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("碰到了：" + collision.gameObject.name);

        if (collision.gameObject.name == "EndPoint")
        {
            reachedEnd = true;

            // 停止所有速度
            rb.linearVelocity = Vector2.zero;
            // 停止物理移動
            rb.bodyType = RigidbodyType2D.Kinematic;

        }
    }
}
