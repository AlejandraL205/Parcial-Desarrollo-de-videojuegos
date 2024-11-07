using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChimeneaController : MonoBehaviour
{
    public SpriteRenderer llave;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            llave.enabled = true;
        }
    }

}
