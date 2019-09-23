using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Include it here 
using UnityEngine.UI;
public class ItemToBar : MonoBehaviour
{
    Vector3 targetPosition;
    Vector3 currentPosition;
    public Transform current;
    public RectTransform target;
    public float speed = 1f;
    bool isCollide = false;


    private Image image;

    
    private void Awake()
    {
        image = GetComponent<Image>();

        HideImage();
    }
    
    
    void Start()
    {

        //Vector2 pos = gameObject.transform.position;  // get the game object position
        //Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);  //convert game object position to VievportPoint

        // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
        


        Debug.Log("Item: " + current.position);
        Debug.Log("Target: " + target.position);
        Debug.Log("Converter Item: " + (transform.TransformPoint(current.position)));
        Debug.Log("Camera Item: " + (Camera.main.ScreenToWorldPoint(transform.TransformPoint(current.position))));

        //GetComponent<RectTransform>().position = Camera.main.ScreenToWorldPoint(transform.TransformPoint(current.position));
        GetComponent<RectTransform>().position = transform.TransformPoint(current.position);
        Debug.Log("Rec: "+ GetComponent<RectTransform>().localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollide == false)
        {
            GetComponent<RectTransform>().position = transform.TransformPoint(current.position); // BAD !
        }
        if (isCollide == true)
        {            
            ShowImage();           
            //Debug.Log("Converter Item: " + (transform.TransformPoint(current.position)));
            //currentPosition = Camera.main.ScreenToWorldPoint(transform.TransformPoint(current.position));
            GetComponent<RectTransform>().position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
            //Same dont GetComponent Every Frame
            if (target.position == GetComponent<RectTransform>().position)
            {
                Debug.Log("Equal: " + GetComponent<RectTransform>().position);
                Debug.Log("Target: " + target.position);
                Destroy(gameObject);
                isCollide = false;
            }
        }
        
       
    }
    public void ColliderTrigger()
    {
        isCollide = true;
    }
    public void HideImage()
    {
        image.enabled = false;
    }

    public void ShowImage()
    {
        image.enabled = true;

    }
 
}
