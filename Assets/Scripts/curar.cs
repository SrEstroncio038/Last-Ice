using System;
using UnityEngine;

public class curar : MonoBehaviour
{

    [SerializeField] UIController controller;

    void OnTriggerEnter2D(Collider2D  collider)
    {
        controller.curando = true;
        controller.curar();
    }

    void OnTriggerExit2D(Collider2D  collider)
    {
        controller.curando = false;
    }
}
