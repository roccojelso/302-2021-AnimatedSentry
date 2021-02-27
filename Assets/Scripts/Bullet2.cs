using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{

    public float movementSpeed;
    private GameObject target;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player)
            {
                HealthSystem playerHealth = player.GetComponent<HealthSystem>();
                if (playerHealth)
                {
                    playerHealth.TakeDamage(10);
                }
                Destroy(gameObject);
            }
        }
    }
}
