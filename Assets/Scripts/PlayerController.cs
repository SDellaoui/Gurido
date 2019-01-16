using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponContainer = null;
    [SerializeField]
    private float speed;

    // Direction
    private Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        speed /= 50;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        // get movement keyboard input
        float _xPos = Input.GetAxisRaw("Horizontal") * speed ;
        float _yPos = Input.GetAxisRaw("Vertical") * speed;
        
        //update transform position based on input movements and speed;
        transform.Translate(_xPos, _yPos, 0);

        //get mouse position and convert the 2D position of the camera into a 3d vector
        Vector3 _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);


        //get the direction the gameobject should look at and update the up coordinates to follow the mouse cursor
        direction = new Vector2(_mousePosition.x - transform.position.x, _mousePosition.y - transform.position.y);
        weaponContainer.transform.up = direction;
    }
}
