﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer characterRender;
    [SerializeField]
    Camera camera;
    public GameObject weaponContainer;
    [SerializeField]
    private float speed;

    // Direction
    private Vector2 direction;
    private Vector3 mousePosition;

    //Health
    private int health = 70;
    private Rigidbody2D playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        //Ignore collision with Collectibles
        Physics2D.IgnoreLayerCollision(gameObject.layer, 8);
    }
    void Update()
    { 
        //get mouse position and convert the 2D position of the camera into a 3d vector
        mousePosition = Input.mousePosition;
        mousePosition = camera.ScreenToWorldPoint(mousePosition);

        UpdatePosition();
        UpdateCharacherSprite();
    }

    private void UpdatePosition()
    {
        // get movement keyboard input
        float _xPos = Input.GetAxisRaw("Horizontal");// * speed ;
        float _yPos = Input.GetAxisRaw("Vertical");// * speed;

        //update transform position based on input movements and speed;
        //transform.Translate(_xPos, _yPos, 0);

        //new position update
        playerRigidBody.MovePosition(new Vector2(transform.position.x + _xPos * speed * Time.deltaTime,
            transform.position.y + _yPos * speed * Time.deltaTime));

        //get the direction the gameobject should look at and update the up coordinates to follow the mouse cursor
        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        weaponContainer.transform.up = direction;
    }

    private void UpdateCharacherSprite()
    {
        characterRender.flipX = (mousePosition.x - transform.position.x > 0) ? false : true;
    }

    public void AddDamage(int dmg)
    {
        health -= dmg;
        Debug.Log(gameObject.name + " has taken " + dmg + " damages");
    }


    // Health
    public bool AddHealth(int _health)
    {
        if (health == 100)
            return false;

        if (health + _health > 100)
            health = 100;
        else
            health += _health;
        return true;
    }
    public int GetHealth()
    {
        return health;
    } 
}
