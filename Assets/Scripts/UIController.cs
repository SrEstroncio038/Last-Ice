using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textoVida;

    public int nivel;

    [HideInInspector] public bool curando;

    [HideInInspector] public bool enAgua;

    void Start()
    {
        curando = false;
        enAgua = false;
        bajarVida();
    }

    public async void bajarVida()
    {
        float espera;

        if (nivel == 1)
        {
            espera = 2;
        }
        else if (nivel == 2)
        {
            espera = 1.5f;
        }
        else
        {
            espera = 1;
        }
        while (int.Parse(textoVida.text) > 0)
        {
            if (curando)
            {
                await Awaitable.WaitForSecondsAsync(0.5f);
                curar();
            }
            else if (enAgua)
            {
                await Awaitable.WaitForSecondsAsync(0.08f); //esperar los segundos  antes de actualizar la vida de nuevo
                actualizarVida();
            }
            else
            {
                await Awaitable.WaitForSecondsAsync(espera); //esperar los segundos  antes de actualizar la vida de nuevo
                actualizarVida();
            }

        }
    }


    public void actualizarVida()
    {
        int nuevaVida = int.Parse(textoVida.text) - 1;
        if (nuevaVida <= 0)
        {
            nuevaVida = 0;
        }
        textoVida.text = nuevaVida.ToString();
    }

    public async void curar()
    {
        int nuevaVida = int.Parse(textoVida.text) + 2;
        if (nuevaVida >= 100)
        {
            nuevaVida = 100;
        }
        textoVida.text = nuevaVida.ToString();

    }

    
}
