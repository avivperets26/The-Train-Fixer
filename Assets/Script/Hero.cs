using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Hero : MonoBehaviour
{
    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathkick = new Vector2(-1f, 15f);


    public AudioClip[] hitRandomSound;
    public AudioClip[] jumpRandomSound;
    public AudioClip[] pickUpItemRandomSound;
    public AudioClip[] fallToWater;
    
    bool isAlive = true; //Death Stat
    bool isTouchable = true;
    bool isHurt = false;
    bool IsWinScene = false;
    bool IsLoseScene = false;
    private string state;

    float curTime = 0;
    float nextDamage = 10;
    //Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;//climbing usage



    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    //Messages then methods
    void Start()
    {
        IsWinScene = false;
        IsLoseScene = false;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;


    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!isAlive) { return; }
        //Debug.Log(myRigidBody.velocity.y);
        if ((IsWinScene == false) || (IsLoseScene = false))
        {
            if (myRigidBody.velocity.y == 0)
            {
                
                myAnimator.SetBool("isJumping", false);
                myAnimator.SetBool("isFalling", false);
                

            }
            if (myRigidBody.velocity.y > 0 && (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))))
            {
                myAnimator.SetBool("isJumping", true);
                //Debug.Log("Jump");
               
                
                //FindObjectOfType<Train>().ColliderEventOn();
            }
            if (myRigidBody.velocity.y < 0 && (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))))
            {

                myAnimator.SetBool("isJumping", false);
                myAnimator.SetBool("isFalling", true);
               
                        
            }
            if (myFeet.IsTouchingLayers(LayerMask.GetMask("Train")))
            {                
                
                myAnimator.SetBool("isJumping", false);
                myAnimator.SetBool("isFalling", false);
            }
            


            if (isHurt == false)
            {
                Jump();
                Run();
            }           
            FlipSprite();
            LayersCollosions();
        }
        else if ((IsWinScene == true) || (IsLoseScene = true))
        {
            myAnimator.SetBool("isRunning", false);
            myAnimator.SetBool("isJumping", false);
            myAnimator.SetBool("isFalling", false);
        }
        
    }

    public void BarsControl()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))){
            //Debug.Log("Damage");
            AudioSource.PlayClipAtPoint(hitRandomSound[Random.Range(0, hitRandomSound.Length)], Camera.main.transform.position);//SFX When Getting Hit
            myAnimator.SetBool("isHurt", true);
            FindObjectOfType<GameSession>().HealthPointDeacrese();
            OnTriggerStay2D();
            if(myAnimator.GetBool("isHurt") == true)
            {
                isTouchable = false;
            }
            //myAnimator.SetTrigger("isIdle");
            //myAnimator.setState
            

        }
        else if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Item")))
        {
            AudioSource.PlayClipAtPoint(pickUpItemRandomSound[Random.Range(0, pickUpItemRandomSound.Length)], Camera.main.transform.position);//SFX When Picking up an Item
            FindObjectOfType<GameSession>().EnergyPointIncrease();
            FindObjectOfType<GameSession>().ItemPointIncrease();
        }
        else if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Fuels")))
        {
            AudioSource.PlayClipAtPoint(pickUpItemRandomSound[Random.Range(0, pickUpItemRandomSound.Length)], Camera.main.transform.position);//SFX When Picking up an Item
            FindObjectOfType<GameSession>().EnergyPointIncrease();
            FindObjectOfType<GameSession>().FuelsPointIncrease();            
        }
        else if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Potions")))
        {
            AudioSource.PlayClipAtPoint(pickUpItemRandomSound[Random.Range(0, pickUpItemRandomSound.Length)], Camera.main.transform.position);//SFX When Picking up an Item
            FindObjectOfType<GameSession>().EnergyPointIncrease();
            FindObjectOfType<GameSession>().PotionsPointIncrease();           
        }


    }

    private void Run()
    {
        

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        if ((Input.GetKey(KeyCode.RightArrow) && (myFeet.IsTouchingLayers(LayerMask.GetMask("Train"))) || (Input.GetKey(KeyCode.LeftArrow) && (myFeet.IsTouchingLayers(LayerMask.GetMask("Train"))))))
        {
            
            GetComponent<AudioSource>().UnPause();
            FindObjectOfType<Train>().FrictionEventOn();

        }
        if ((Input.GetKey(KeyCode.RightArrow) && (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) || (Input.GetKey(KeyCode.LeftArrow) && (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))))){
            GetComponent<AudioSource>().UnPause();
        }
        else
        {
            FindObjectOfType<Train>().FrictionEventOff();
            GetComponent<AudioSource>().Pause();
        }
    }

    private void Jump()
    {
        if ((!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) && (!myFeet.IsTouchingLayers(LayerMask.GetMask("Train")))) { return; }
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            AudioSource.PlayClipAtPoint(jumpRandomSound[Random.Range(0, jumpRandomSound.Length)], Camera.main.transform.position);
            //Debug.Log("Jump");
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); // value is between -1 to +1
            Vector2 jumpVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * jumpSpeed);
            myRigidBody.velocity = jumpVelocity;


            //JumpingAnim();        
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void JumpingAnim()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); // value is between -1 to +1
        Vector2 jumpVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * jumpSpeed);
        myRigidBody.velocity = jumpVelocity;

    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
    private void LayersCollosions()
    {
        Physics2D.IgnoreLayerCollision(12, 9); //(12)Hero vs (9)Rails
    }
  
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        BoxCollider2D Boxcollided = collision.GetComponent<BoxCollider2D>();
        Collider2D capsuleCollided = collision.GetComponent<CapsuleCollider2D>();
        EdgeCollider2D edgeCollider = collision.GetComponent<EdgeCollider2D>();
        
        //Debug.Log(myBodyCollider.GetType());
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("WaterLines")))//Hero Die from water
        {
            AudioSource.PlayClipAtPoint(fallToWater[Random.Range(0, fallToWater.Length)], Camera.main.transform.position);//SFX When falling to water  
            yield return new WaitForSeconds(0.5f);
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            FindObjectOfType<GameSession>().processPlayerDeath();
        }
       
        
        //&& myAnimator.GetBool("isFalling") == true
        else if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) && isTouchable == true)//Enemy Kill
        {
            if (collision == capsuleCollided)
            {
                if (FindObjectOfType<GameSession>().getCurrentHealthVal() <= 10)//Hero Die
                {
                    isAlive = false;
                    myAnimator.SetTrigger("Dying");
                    Debug.Log("Dying");
                    FindObjectOfType<GameSession>().processPlayerDeath();
                }
                else//Hero getting Hurt
                {                   
                    myRigidBody.velocity = new Vector2(-3f, 30f);
                    BarsControl();
                    isHurt = true;
                    yield return new WaitForSeconds(0.5f);
                    isHurt = false;
                    myAnimator.SetBool("isHurt", false);
                    isTouchable = true;
                }
            }
        }
        else if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("House")))
        {
            IsWinScene = true;
            FindObjectOfType<GameSession>().WinScene();
        }
    }
    void OnTriggerStay2D()//Will delay the Hurt Animation State
    {
        if (curTime <= 0)
        {            
            curTime = nextDamage;
        }
        else
        {
            curTime -= Time.deltaTime;
        }
        
    }

    public void HeroBounce()
    {
        myRigidBody.velocity = new Vector2(3f, 30f);
    }
}
