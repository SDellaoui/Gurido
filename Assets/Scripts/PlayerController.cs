using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer characterRender;
    [SerializeField]
    private GameObject weaponContainer = null;
    [SerializeField]
    private float speed;

    // Direction
    private Vector2 direction;
    private Vector3 mousePosition;
    private bool preventCollisionMove = false;

    //Health
    private int health = 100;

    private Rigidbody2D playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        //speed /= 50;
    }
    void Update()
    { 
        //get mouse position and convert the 2D position of the camera into a 3d vector
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

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

        /*
        //get mouse position and convert the 2D position of the camera into a 3d vector
        Vector3 _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
        */

        //get the direction the gameobject should look at and update the up coordinates to follow the mouse cursor
        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        weaponContainer.transform.up = direction;
    }

    private void UpdateCharacherSprite()
    {
        characterRender.flipX = (mousePosition.x - transform.position.x > 0) ? false : true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            preventCollisionMove = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            preventCollisionMove = false;
    }

    public void AddHealth(int _health)
    {
        if (health + _health > 100)
            health = 100;
        else
            health += _health;
    }
}
