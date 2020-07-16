using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float maxHealth, maxHunger, maxThirst, maxStamina, minTemperature, maxTemperature;
    public float hungerIncreaseRate, thirstIncreaseRate, staminaDecreaseRate, staminaRegenRate;
    private float health, hunger, thirst, stamina, temperature;
    private bool dead;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        health = maxHealth;
        stamina = maxStamina;
        temperature = 98.6f;
        hunger = 0;
        thirst = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead) {
            thirst += thirstIncreaseRate * Time.deltaTime;
            hunger += hungerIncreaseRate * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.LeftShift)) {
            if(stamina > 0) {
                stamina -= staminaDecreaseRate * Time.deltaTime;
                this.gameObject.GetComponent<PlayerMovement>().speed = 20;
            } else {
                print("Out of stamina, you can't sprint");
                this.gameObject.GetComponent<PlayerMovement>().speed = 12;
            }
        } else {
            this.gameObject.GetComponent<PlayerMovement>().speed = 12;
            if (stamina < maxStamina) {
                stamina += staminaRegenRate * Time.deltaTime;
            }
        }

        if(health <= 0 || hunger >= maxHunger || thirst >= maxThirst
            || temperature >= maxTemperature || temperature <= minTemperature)
            Die();
    }

    public void Die() {
        dead = true;
        print("You have died");
    }

    public void Drink(float decreaseRate) {
        thirst -= decreaseRate;
    }

    public void Eat(float decreaseRate) {
        hunger -= decreaseRate;
    }

    void OnTriggerEnter(Collider other) {
        
    }

    void OnTriggerExit(Collider other) {

    }
}
