using UnityEngine;

public class Walk : MonoBehaviour
{ 
    private float maxSpeed = 4f;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        var keyLeftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        var keyRightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        var keyUpPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        var keyDownPressed = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

        if (keyLeftPressed)
        {
            anim.SetBool("facingLeft", true);
            anim.SetBool("facingTop", false);
            anim.SetBool("facingRight", false);
            anim.SetBool("facingBottom", false);
        }
        else if (keyRightPressed)
        {
            anim.SetBool("facingLeft", false);
            anim.SetBool("facingTop", false);
            anim.SetBool("facingRight", true);
            anim.SetBool("facingBottom", false);
        }
        else if (keyUpPressed)
        {
            anim.SetBool("facingLeft", false);
            anim.SetBool("facingTop", true);
            anim.SetBool("facingRight", false);
            anim.SetBool("facingBottom", false);
        }
        else if (keyDownPressed)
        {
            anim.SetBool("facingLeft", false);
            anim.SetBool("facingTop", false);
            anim.SetBool("facingRight", false);
            anim.SetBool("facingBottom", true);
        }
        else
        {
            anim.SetBool("facingLeft", false);
            anim.SetBool("facingTop", false);
            anim.SetBool("facingRight", false);
            anim.SetBool("facingBottom", false);
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float newHorizontal = h * maxSpeed;
        float newVertical = v * maxSpeed;

        anim.SetFloat("speedX", newHorizontal);
        anim.SetFloat("speedY", newVertical);

        rb.velocity = new Vector2(newHorizontal, newVertical);
    }
}
