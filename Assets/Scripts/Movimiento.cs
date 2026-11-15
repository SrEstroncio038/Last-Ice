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
    private AudioSource audioSource;
    public AudioClip sonidoSalto;
    public AudioClip sonidoMuerte;
    public AudioClip sonidoCaminar;
    private float tiempoEntreSonidos = 0.3f;
    private float siguienteSonidoPermitido = 0f;
    [SerializeField] UIController controller;
    private bool estaMuerto = false;
    private bool enSuelo = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!estaMuerto)
        {
            if (int.Parse(controller.textoVida.text) > 0)
            {
                Vector2 input = InputSystem.actions["Move"].ReadValue<Vector2>();
                rb.linearVelocity = new Vector2(input.x * velocidad, rb.linearVelocity.y);
                animator.SetFloat("velocidad", Mathf.Abs(rb.linearVelocityX));
                if (Mathf.Abs(input.x) > 0.1f && enSuelo)
                {
                    ReproducirSonidoCaminar();
                }
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
                    Saltar();
                    enSuelo = false;
                }
            }
            else
            {
               muerte();
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
            muerte();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("deathZone"))
        {
            muerte();
        }

    }

    public void muerte()
    {
        enSuelo = true;
        controller.textoVida.text = "0";
        animator.SetTrigger("death");
        audioSource.PlayOneShot(sonidoMuerte);
        estaMuerto = true;
    }

    public void Saltar()
    {
        if (sonidoSalto != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoSalto);
        }    
    }

    public void ReproducirSonidoCaminar()
    {
    if (Time.time >= siguienteSonidoPermitido)
    {
        if (sonidoCaminar != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoCaminar, 0.1f);
        }
        
        siguienteSonidoPermitido = Time.time + tiempoEntreSonidos;
        }
    }
}