using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    Hero hero;
    // Start is called before the first frame update
    void Start()
    {

        hero = FindObjectOfType<Hero>();
    }

    // Update is called once per frame
    void Update()
    {

            Vector2 newCamPos = new Vector2(hero.transform.position.x, hero.transform.position.y);
            transform.position = new Vector3(newCamPos.x, newCamPos.y, transform.position.z);

       
    }

   
}
