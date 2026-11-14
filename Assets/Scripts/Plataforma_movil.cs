using UnityEngine;

public class Plataforma_movil : MonoBehaviour
{
    public Vector2 puntoA;
    public Vector2 puntoB;
    public float velocidad = 2f;

    private bool moviendoHaciaB = true;

    void Start()
    {
        transform.position = puntoA;
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
        if (Vector2.Distance(transform.position, destino) == 0)
        {
            moviendoHaciaB = !moviendoHaciaB;
        }
    }
}