using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f; // Vida del enemigo
    public float damage = 10f; // Daño que hace
    public float speed = 2f; // Velocidad de movimiento

    private Transform player;

    void Start()
    {
        // Busca el objeto del jugador
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Mueve al enemigo hacia el jugador
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Calcula la dirección hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Opcional: rotar hacia el jugador
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el enemigo colisiona con el jugador
        if (other.CompareTag("Player"))
        {
            // Aquí puedes llamar a un método en el script del jugador para hacer daño
            // Ejemplo: other.GetComponent<Player>().TakeDamage(damage);
            Debug.Log("El enemigo hace daño al jugador: " + damage);
        }
    }

    // Método para recibir daño
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Aquí puedes añadir la lógica de muerte (ej. destruir el objeto)
        Destroy(gameObject);
    }
}
