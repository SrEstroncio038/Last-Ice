using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
     void Start()
    {
        GetComponent<Button>().onClick.AddListener(IrAlNivel1);
    }

    void IrAlNivel1()
    {
   
        SceneManager.LoadScene("Nivel1");
    }
}