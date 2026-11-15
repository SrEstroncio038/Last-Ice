using UnityEngine;

public class puentelava : MonoBehaviour
{
    public float velocidad = 2f;
    private bool activado = false;

    void Update()
    {
        if (activado)
        {
            transform.position += new Vector3(0, 1, 0) * velocidad * Time.deltaTime;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            activado = true;
        }
    }
}