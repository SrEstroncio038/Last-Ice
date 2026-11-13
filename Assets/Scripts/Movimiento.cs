using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class scriptMovimiento : MonoBehaviour
{
    public float velocidad = 6f;
    public float fuerzaSalto = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    [SerializeField] UIController controller;
    private bool enSuelo = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (int.Parse(controller.textoVida.text) > 0)
        {
            Vector2 input = InputSystem.actions["Move"].ReadValue<Vector2>();
            rb.linearVelocity = new Vector2(input.x * velocidad, rb.linearVelocity.y);
            animator.SetFloat("velocidad", Mathf.Abs(rb.linearVelocityX));
            if (input.x > 0)
            {
                sr.flipX = false;

            }
            else if (input.x < 0)
            {
                sr.flipX = true;
            }

            if (InputSystem.actions["Jump"].WasPressedThisFrame() && enSuelo)
            {
                rb.AddForce(transform.up * fuerzaSalto, ForceMode2D.Impulse);
                enSuelo = false;
            }
        }


    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
        {
            enSuelo = true;

        }
        if (collision.gameObject.CompareTag("lava"))
        {
            enSuelo = true;
            controller.textoVida.text = "0";
            animator.SetTrigger("death");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("deathZone"))
        {
            controller.textoVida.text = "0";
            animator.SetTrigger("death");
        }

    }
}