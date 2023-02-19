using UnityEngine;
using System.Collections;
using RPG;

public class PowerUp : MonoBehaviour
{

    // collect Power up
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.ShowFloatingText($"{this.gameObject.name} collected!", 20, Color.black, transform.position, Vector3.up, 1f);

            // add power up to player
            other.gameObject.GetComponent<PlayerController>().AddPowerUp(this);

            // destroy power up
            Destroy(gameObject);
        }
    }


}