using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // public variables appear as properties in Unity's inspector window
    public float movementSpeed;

    // holds 2D points; used to represent a character's location in 2D space, or where it's moving to
    Vector2 movement = new Vector2();

    //fireball that the player shoots
    public GameObject spawnFireball;

    Animator character;

    // reference to the character's Rigidbody2D component, location, and gameObject
    Rigidbody2D rb2D;
    Transform location;
    GameObject characterObj;

    //cooldown for fireball
    float fireballRate = 0.5f;
    float fireballCooldown;

    //player's mana
    float mana;

    //player's animation state
    public string AnimationState = "AnimationState";

    //player's direction
    string direction = "down";

    enum CharacterStates
    {

        walkRight = 1,
        walkDown = 2,
        walkLeft = 3,
        walkUp = 4,
        Idle = 5,
        idleLeft = 6,
        idleRight = 7,
        idleUp = 8

    }

    // use this for initialization
    private void Start()
    {
        // get references to game object component so it doesn't have to be grabbed each time needed
        character = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        location = GetComponent<Transform>();
        characterObj = GameObject.Find("character");
        player player = character.GetComponent<player>();
        mana = player.currentMana;
        movementSpeed = player.movementSpeed;

    }

    // called once per frame
    private void Update()
    {

        UpdateState();

    }

    // called at fixed intervals by the Unity engine
    // update may be called less frequently on slower hardware when frame rate slows down
    void FixedUpdate()
    {

        shootFireball();
        MoveCharacter();

    }

    private void MoveCharacter()
    {
        // get user input
        // GetAxisRaw parameter allows us to specify which axis we're interested in
        // Returns 1 = right key or "d" (up key or "w")
        //        -1 = left key or "a"  (down key or "s")
        //         0 = no key pressed
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // keeps player moving at the same rate of speed, no matter which direction they are moving in
        movement.Normalize();

        // set velocity of RigidBody2D and move it
        rb2D.velocity = movement * movementSpeed;
    }

    private void UpdateState()
    {
        //sets the animation for walking right
        if (movement.x > 0)
        {

            character.SetInteger(AnimationState, (int)CharacterStates.walkRight);
            direction = "right";

        }

        //sets the animation for walking left
        else if (movement.x < 0)
        {

            character.SetInteger(AnimationState, (int)CharacterStates.walkLeft);
            direction = "left";

        }

        //sets the animation for walking up
        else if (movement.y > 0)
        {

            character.SetInteger(AnimationState, (int)CharacterStates.walkUp);
            direction = "up";

        }

        //sets the animation for walking down
        else if (movement.y < 0)
        {

            character.SetInteger(AnimationState, (int)CharacterStates.walkDown);
            direction = "down";

        }

        //sets the animation for Idle down
        else if (direction == "down")
        {

            character.SetInteger(AnimationState, (int)CharacterStates.Idle);

        }

        //sets the animation for Idle up
        else if (direction == "up")
        {

            character.SetInteger(AnimationState, (int)CharacterStates.idleUp);

        }

        //sets the animation for Idle left
        else if (direction == "left")
        {

            character.SetInteger(AnimationState, (int)CharacterStates.idleLeft);

        }

        //sets the animation for Idle right
        else if (direction == "right")
        {

            character.SetInteger(AnimationState, (int)CharacterStates.idleRight);

        }

    }

    private void shootFireball()
    {

        //gets a reference to the player and the mana of the player
        player player = character.GetComponent<player>();
        mana = player.currentMana;
        Vector3 mouseWorldPosition;

        //if the input for a fireball is pressed, there's enough mana, and it's not on cooldown it will spawn a fireball
        if (Input.GetMouseButton(0) && mana >= 1 && Time.time > fireballCooldown)
        {
  
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 mouseWorldPosition = Input.mousePosition;

            float angleDeg = Mathf.Rad2Deg * Mathf.Atan((mouseWorldPosition.y - location.position.y) / (mouseWorldPosition.x - location.position.x));

            if (location.position.x > mouseWorldPosition.x)
            {

                angleDeg += 90;

            }

            else {

                angleDeg -= 90;

            }

            //angleDeg *= Mathf.Deg2Rad;

            //sets the direction of the projectile so it moves the correct direction
            spawnFireball.GetComponent<projectiles>().angle = angleDeg;
            //spawnFireball.GetComponent<projectiles>().damageCharacter = false;

            //Instantiate(spawnFireball, new Vector3(location.position.x + 7.5f * Mathf.Cos(angleDeg) + movement.x, location.position.y + 9.5f * Mathf.Sin(angleDeg) + movement.y, location.position.z), Quaternion.identity);
            Instantiate(spawnFireball, new Vector3(location.position.x, location.position.y, location.position.z), Quaternion.identity);

            //decreases the mana and sets the cooldown
            player.currentMana -= 1;
            fireballCooldown = Time.time + fireballRate;

        }

    }


}