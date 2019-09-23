using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string key;
    [SerializeField]
    private TrainMenu trainMenu;
    public bool isOver = false;
    private float ClickScale = 0.75f;

    public void PlayGame()
    {
        //SceneManager.LoadScene()
    }

    public void StartFirstLevel()
    {
        
        //trainMenu.ShowImage();
        SceneManager.LoadScene(1);

    }
    public void ExitGame()
    {
        

        Application.Quit();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse enter");
        //transform.localScale = transform.localScale * ClickScale;
        isOver = true;
        trainMenu.ShowImage();
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        //transform.localScale = Vector3.one;
        
        Debug.Log("Mouse exit");
        isOver = false;
        trainMenu.HideImage();
    }
    void OnApplicationFocus(bool isFocus)
    {
        if (!isFocus)
        {
            transform.localScale = Vector3.one;
        }
    }
}
