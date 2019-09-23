using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image healthLine;

    [SerializeField]
    private Text valueText;

    public Transform energyPos;
    Vector3 targettPos;
    Bar Hbar ;
    public float MaxValue{ get; set; }

    public float Value
    {
        get
        {
            return fillAmount;
        }
        set
        {
            string[] tmp = valueText.text.Split(':');
            //valueText.text = tmp[0] + ": " + value;
            fillAmount = Map(value, 0, MaxValue, 0, 1);

        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        Hbar = GetComponent<Bar>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }
    private void HandleBar()
    {

        if (fillAmount != healthLine.fillAmount)
        {
            healthLine.fillAmount = Mathf.Lerp(healthLine.fillAmount,fillAmount,Time.deltaTime*lerpSpeed);
        }
        
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    public Vector2 EnergyBarPosition()
    {       
        Vector3 viewPos = Camera.main.ScreenToViewportPoint(GameObject.FindGameObjectWithTag("EnergyBar").transform.position);
        Vector3 Pos = new Vector2(viewPos.x, viewPos.y);

        return Pos;
    }
    public void HealthBarPosition()
    {
        Vector3 HealthPos = GameObject.FindGameObjectWithTag("HealthBar").transform.position;
    }

}
