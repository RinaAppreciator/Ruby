using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColllectibleHealth : MonoBehaviour
{
    public int healNumber;
    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Object that entered the trigger: " + other);
        character_controller controller = other.GetComponent<character_controller>();

        if (controller != null && controller.maxHealth > controller.health)
        {
            controller.ChangeHealth(healNumber);
            Destroy(gameObject);
        }



    }
}
