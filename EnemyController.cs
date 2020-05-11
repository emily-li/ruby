using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
	private Rigidbody2D rigidbody2d;

	private float speed = 5f;
	private float changeDirectionTimeout = 3f;
	private float directionTimer;
	private int direction = 1;
	private bool vertical;

	void Start() {
		rigidbody2d = GetComponent<Rigidbody2D>();
		directionTimer = changeDirectionTimeout;
		vertical = true;
	}

    void Update() {
    	fixDirection();
    	changePosition();
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
            position.y = position.y + Time.deltaTime * speed * direction;
        } else {
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        rigidbody2d.MovePosition(position);
    }
}
