using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CerebrosUI : MonoBehaviour
{
    public List<Image> listaCerebros; // Lista de imágenes de cerebros
    public GameObject cerebroPrefab; // Prefab de cerebro
    public MortyHealth vidaMorty; // Script de vida del jugador
    public Sprite cerebroLleno; // Sprite del cerebro lleno
    public Sprite cerebroVacio; // Sprite del cerebro vacío

    private void Awake()
    {
        // Inicializar la lista de cerebros
        listaCerebros = new List<Image>();

        for (int i = 0; i < vidaMorty.maxHealth; i++)
        {
            GameObject cerebro = Instantiate(cerebroPrefab, transform);
            listaCerebros.Add(cerebro.GetComponent<Image>());
        }

        // Actualizar la UI inicialmente
        ActualizarCerebrosUI();
    }

    private void OnEnable()
    {
        // Suscribirse a los eventos de vida si tienes uno
        vidaMorty.OnHealthChanged += ActualizarCerebrosUI;
    }

    private void OnDisable()
    {
        vidaMorty.OnHealthChanged -= ActualizarCerebrosUI;
    }

    public void ActualizarCerebrosUI()
    {
        for (int i = 0; i < listaCerebros.Count; i++)
        {
            if (i < vidaMorty.currentHealth)
            {
                listaCerebros[i].sprite = cerebroLleno;
            }
            else
            {
                listaCerebros[i].sprite = cerebroVacio;
            }
        }
    }
}
