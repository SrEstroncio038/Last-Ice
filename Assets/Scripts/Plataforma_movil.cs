using UnityEngine;

public class Plataforma_movil : MonoBehaviour
{
    private Vector2 puntoA;
    public Vector2 desplacamiento;
    private Vector2 puntoB;
    public float velocidad = 2f;

    private bool moviendoHaciaB = true;

    void Start()
    {
        puntoA = transform.position;
        puntoB = puntoA + desplacamiento;
    }

    void Update()
    {
        Vector2 destino;
        if (moviendoHaciaB)
        {
            destino = puntoB;
        }
        else
        {
            destino = puntoA;
        }
        transform.position = Vector2.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);
        if (Vector2.Distance(transform.position, destino) < 0.01f)
        {
            moviendoHaciaB = !moviendoHaciaB;
        }
    }
}