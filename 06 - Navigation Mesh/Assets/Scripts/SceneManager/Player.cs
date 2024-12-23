using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBarController healthBar;

    private int numberOfItemsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            TakeDamage(20);
        }
    }

    public int getCollectedItems()
    {
        return this.numberOfItemsCollected;
    }

    void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "ItemsToCollect":
                Destroy(col.gameObject);
                numberOfItemsCollected++;
                break;
            case "Dragon":
                TakeDamage(40);
                break;
            case "Condor":
                TakeDamage(20);
                break;
            case "Chicken":
                TakeDamage(10);
                break;
        }
    }
}
