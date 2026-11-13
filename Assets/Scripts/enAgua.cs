using UnityEngine;

public class enAgua : MonoBehaviour
{
    [SerializeField] UIController controller;

    void OnTriggerEnter2D(Collider2D collider)
    {
        controller.enAgua = true;
        controller.bajarVida();
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        controller.enAgua = false;
    }
}
