using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
	private Rigidbody2D rigidbody2d;
    private Animator animator;

	private float speed = 7.5f;
	private float changeDirectionTimeout = 1f;
	private float directionTimer;
	private int direction = 1;
	private bool vertical;

	void Start() {
		rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

		directionTimer = changeDirectionTimeout;
		vertical = true;
	}

    void Update() {
    	fixDirection();
    	changePosition();
    }

    void OnCollisionEnter2D(Collision2D other) {
	    RubyController player = other.gameObject.GetComponent<RubyController>();

	    if (player != null) {
	        player.ChangeHealth(-1);
	    }
	}

    private void fixDirection() {
        directionTimer -= Time.deltaTime;        
        if (directionTimer < 0) {
            directionTimer = changeDirectionTimeout;

            if (!vertical) {
            	direction = -direction;		
            }
            vertical = !vertical;
        }
    }

    private void changePosition() {
    	Vector2 position = rigidbody2d.position;
        if (vertical) {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);

            position.y = position.y + Time.deltaTime * speed * direction;
        } else {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);

            position.x = position.x + Time.deltaTime * speed * direction;
        }
        rigidbody2d.MovePosition(position);
    }
}
