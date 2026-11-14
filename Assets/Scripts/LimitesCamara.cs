using UnityEngine;

public class LimitesCamara : MonoBehaviour
{
    [Header("Arrastra aquí tu fondo desde la jerarquía")]
    public SpriteRenderer fondoSprite;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (fondoSprite == null)
        {
            Debug.LogError("¡Olvidaste arrastrar el fondo al script de la cámara!");
        }
    }

    void LateUpdate()
    {
        if (fondoSprite == null) return;

        // Calculamos el tamaño de la vista de la cámara
        float altoCamara = cam.orthographicSize;
        float anchoCamara = altoCamara * cam.aspect;

        // Obtenemos los límites exactos del sprite (incluyendo su posición y escala)
        Bounds limites = fondoSprite.bounds;

        float minX = limites.min.x + anchoCamara;
        float maxX = limites.max.x - anchoCamara;
        float minY = limites.min.y + altoCamara;
        float maxY = limites.max.y - altoCamara;

        // Comprobación de seguridad por si la cámara es más grande que el fondo
        if (minX > maxX) minX = maxX = (limites.min.x + limites.max.x) / 2f;
        if (minY > maxY) minY = maxY = (limites.min.y + limites.max.y) / 2f;

        // Aplicamos los límites a la posición actual de la cámara
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}