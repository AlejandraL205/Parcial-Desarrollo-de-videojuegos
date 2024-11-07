using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NotaController : MonoBehaviour
{

    public Canvas canvaNota;
    public TextMeshProUGUI text;
    public string nota;

    private void Awake()
    {
        text.text = nota;
    }

    public void Cerrar()
    {
        canvaNota.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { 
            canvaNota.enabled = true;
        }
    }

}