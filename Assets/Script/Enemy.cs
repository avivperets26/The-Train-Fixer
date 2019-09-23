using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    BoxCollider2D myWeakness;
    CapsuleCollider2D myBody;
    Vector2 floatY;
    float originalY;
    [SerializeField] float FloatStrength;

    /*
    public BoxCollider2D MyWeakness
    {
        get
        {
            return myWeakness;
        }
    }
    */

    void Start()
    {
        myWeakness = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        this.originalY = this.originalY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(transform.position.x,
            originalY + ((float)Math.Sin(Time.time) * FloatStrength),
            transform.position.z);
        /*
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }   
        */
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }


    public void Hurt()
    {
        Debug.Log("Object Destroyd");
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myWeakness.IsTouchingLayers(LayerMask.GetMask("Hero"))){
            FindObjectOfType<Hero>().HeroBounce();
            Hurt();
        }
    }
    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    */

}
