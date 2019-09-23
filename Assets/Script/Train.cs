using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Train : MonoBehaviour
{
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myRoofCollider;
    Rigidbody2D myRigidBody;
    Animator myAnimator;

    [SerializeField] float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myRoofCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {      
        float controlThrow = CrossPlatformInputManager.GetAxis("HorizontalTrain"); // value is between -1 to +1
        Vector2 TrainVelocity = new Vector2(controlThrow * moveSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = TrainVelocity;
        
        bool TrainHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isDriving", TrainHasHorizontalSpeed);
        if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)))
        {
            GetComponent<AudioSource>().UnPause();
        }
        else
        {
            GetComponent<AudioSource>().Pause();
        }
    }
    public void FrictionEventOn()
    {
        
    }
    public void FrictionEventOff()
    {
        myRoofCollider.sharedMaterial.friction = 100f;
    }
}
