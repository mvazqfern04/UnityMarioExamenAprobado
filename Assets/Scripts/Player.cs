using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    /*
    Los límites definidos con bound nos hacen falta debido a que el jugador se puede salir de la pantalla
    debido a que su rigidbody es quinemático, por lo que no se ve afectado por la gravedad ni puede colisionar
    con objetos estáticos.
    */
    [SerializeField] private float bound = 4.5f; // x axis bound 
    [SerializeField] private float tiemAumento = 3f;
    private float aumentoRestante =0f;

    private bool tamanoIsAumentado = false;

    private Vector2 startPos; // Posición inicial del jugador
    private Vector3 tamanoOriginal;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; // Guardamos la posición inicial del jugador
        tamanoOriginal = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
       PlayerMovement();
        if (tamanoIsAumentado && aumentoRestante <= 0)
        {
            gameObject.transform.localScale = tamanoOriginal;
            tamanoIsAumentado = false;
            //GameObject.FindGameObjectWithTag("ball").
        }
        else if (tamanoIsAumentado)
        {
            aumentoRestante -= Time.deltaTime;
        }
    }

    void PlayerMovement()
    {
         float moveInput = Input.GetAxisRaw("Horizontal");
        // Controlaríamo el movimiento de la siguiente forma de no ser el rigidbody quinemático
        // transform.position += new Vector3(moveInput * speed * Time.deltaTime, 0f, 0f);

        Vector2 playerPosition = transform.position;
        // Mathf.Clamp nos permite limitar un valor entre un mínimo y un máximo
        playerPosition.x = Mathf.Clamp(playerPosition.x + moveInput * speed * Time.deltaTime, -bound, bound);
        transform.position = playerPosition;
    }

    public void ResetPlayer()
    {
        transform.position = startPos; // Posición inicial del jugador
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("powerUp")) // Si colisionamos con un powerUp
        {
            Destroy(collision.gameObject); // Lo destruimos
            GameManager.Instance.AddLife(); // Añadimos una vida
        }
        if (collision.CompareTag("powerDown")) // Si colisionamos con un powerUp
        {
            Destroy(collision.gameObject); // Lo destruimos
            GameManager.Instance.LoseLifePowerDown(); // Añadimos una vida
        }
        if (collision.CompareTag("powerTamano")) // Si colisionamos con un powerUp
        {
            Destroy(collision.gameObject); // Lo destruimos
            aumentarTamano();
        }
    }

    private void aumentarTamano()
    {
        if (!tamanoIsAumentado)
        {
            gameObject.transform.localScale =
            new Vector3(gameObject.transform.localScale.x * 2
            , gameObject.transform.localScale.y
            , gameObject.transform.localScale.z);
            aumentoRestante = tiemAumento;
            tamanoIsAumentado = true;
            if (gameObject.GetComponentsInChildren<GameObject>()[0] != null)
            {
                GameObject.FindGameObjectWithTag("ball").GetComponent<Ball>().transform.localScale = new Vector3(0.5f, 1f, 1f);
            }
        }
        else
        {
            aumentoRestante = tiemAumento;
            Debug.Log("tamaño ya aumentado");
        }
    }
}
