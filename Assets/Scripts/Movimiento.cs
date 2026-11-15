using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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

    public int nivel;
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
                    Saltar();
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

        if (collider.gameObject.CompareTag("fin"))
        {
            if (nivel == 3)
            {
                SceneManager.LoadScene("Victoria");
            }
            else
            {
                SceneManager.LoadScene("Nivel" + (nivel+1));
            }

        }

    }




    private void CargarGameOver()
    {
        SceneManager.LoadScene("Derrota");
    }


    public void muerte()
    {
        enSuelo = true;
        controller.textoVida.text = "0";
        animator.SetTrigger("death");
        audioSource.PlayOneShot(sonidoMuerte);
        estaMuerto = true;

        Invoke(nameof(CargarGameOver), 2f); // Espera 2 segundos
    }

    public void Saltar()
    {
        if (sonidoSalto != null && audioSource != null)
        {
            rb.AddForce(transform.up * fuerzaSalto, ForceMode2D.Impulse);
            audioSource.PlayOneShot(sonidoSalto);
            enSuelo = false;
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