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

    private bool enSuelo = false;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        Vector2 input = InputSystem.actions["Move"].ReadValue<Vector2>();
        Vector3 direccion = new Vector3(input.x, 0, input.y);
        transform.position += direccion * velocidad * Time.deltaTime;
        animator.SetFloat("v", Mathf.Abs(direccion.x));
        if (input.x > 0)
        {
            sr.flipX=false;

        }else if(input.x < 0){
            sr.flipX=true;  
        }
    
        if (InputSystem.actions["Jump"].WasPressedThisFrame() && enSuelo)
        {
            rb.AddForce(transform.up * fuerzaSalto, ForceMode2D.Impulse);
            animator.SetTrigger("Salto");     
            enSuelo=false;
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
            animator.SetBool("IsFalling", false);
        }
    }
}