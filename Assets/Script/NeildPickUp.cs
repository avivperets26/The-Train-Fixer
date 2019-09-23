using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeildPickUp : MonoBehaviour
{    
    [SerializeField] int pointsForItemPickup = 5;

    public float speed = 1000f;
    Rigidbody2D myRigidBody;
    BoxCollider2D myBodyCollider;
    bool isCollide = false;
    Vector2 floatY;
    float originalY;
    [SerializeField] float FloatStrength;
    //public ItemToBar itemToBar;

    public Transform target;
    Vector3 targettPos;
    void Start()
    {
        // :) like here 
        myBodyCollider = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        this.originalY = this.originalY = this.transform.position.y;
        targettPos = Camera.main.ScreenToWorldPoint(target.position); // Great
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollide == false)
        {
            transform.position = new Vector3(transform.position.x,
                originalY + ((float)Math.Sin(Time.time) * FloatStrength),
                transform.position.z);
        }
        if (isCollide == true)
        {
            //itemToBar.ColliderTrigger();
            //isCollide = false;
            //Destroy(gameObject);
            /*
            targettPos = Camera.main.ScreenToWorldPoint(target.position);
            transform.position = Vector3.MoveTowards(transform.position, targettPos, speed * Time.deltaTime);
            if (transform.position == targettPos)
            {
                Destroy(gameObject);
                isCollide = false;
            }
            */
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hero")) && isCollide == false)
        {
            StartCoroutine(PickedUpRoutine());
            isCollide = true;
            //Try to cache those things before so u wont get drops on Start()
            FindObjectOfType<Hero>().BarsControl();
            FindObjectOfType<GameSession>().AddToScore(pointsForItemPickup);
            //AudioSource.PlayClipAtPoint(neilPickUpSFX, Camera.main.transform.position);
        }      
    }

    private IEnumerator PickedUpRoutine()
    {
        Debug.Log("Pick up routine Called");
        //itemToBar.ColliderTrigger();
        Transform target = GameSession.Instance.EnergyText.transform; // target taken from GameSessin that was declared as static so you can access it from anywhere
        float t = 0;
        float timeToMove = .75f; // Change Animation Time here you can expose this var to have better control over it 
        Vector3 startPos = transform.position;
        Vector3 startScale = transform.localScale;
        while(t<=timeToMove)
        {
            //Runs every frame like update 
            //Camera.main  - not efficient its doing FindObjectofType every time, Not good 
            transform.position = Vector3.Lerp(startPos, Camera.main.ScreenToWorldPoint(target.position)/*כל  פעם מחדש את הערך של המיקום וזו הבעיה שהיית לך קודם*/, t / timeToMove);
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t / timeToMove);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();

        isCollide = false;
        Destroy(gameObject);
    }

}
