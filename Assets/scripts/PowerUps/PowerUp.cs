using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    // collect Power up
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // add power up to player
            other.gameObject.GetComponent<PlayerController>().AddPowerUp(this);
            // destroy power up
            Destroy(gameObject);
        }
    }
}