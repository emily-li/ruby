using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour {

    private Rigidbody2D rigidbody2d;

    public int maxHealth = 5;
    public int health { get { return currentHealth; }}
    public int currentHealth;
    
    public float timeInvincible = 2.0f;
    private bool isInvincible;
    private float invincibleTimer;
    
    private float speed = 10f;

    void Start()    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update() {
        move();
        if (isInvincible) {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    public void ChangeHealth(int amount) {
        int healthImpact = amount;
        if (healthImpact < 0) {
            if (isInvincible) {
                healthImpact = 0;
            } else {
                isInvincible = true;
                invincibleTimer = timeInvincible;
            }
        }

        currentHealth = Mathf.Clamp(currentHealth + healthImpact, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);    
    }

    private void move() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector2 position = rigidbody2d.position;
        if (horizontal > 0 || horizontal < 0) {
            position.x = position.x + speed * (horizontal * Time.deltaTime);   
        } else {
            position.y = position.y + speed * (vertical * Time.deltaTime);    
        }
        rigidbody2d.MovePosition(position);
    }
}
