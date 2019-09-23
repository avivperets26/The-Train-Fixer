using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] float itemScore;
    [SerializeField] float fuelScore;
    [SerializeField] float potionScore;

    [SerializeField] Text liveText;
    [SerializeField] Text scoreText;

    [SerializeField] public Text EnergyText;
    [SerializeField] public Text HealthText;

    [SerializeField] public Text ScrewScoreText;
    [SerializeField] public Text FuelScoreText;
    [SerializeField] public Text PotionScoreText;

    [SerializeField] private GameObject winSceneMenu;
    [SerializeField] private GameObject loseSceneMenu;

    [SerializeField] public Stat health;
    [SerializeField] public Stat energy;

    public bool IsWinScene = false;
    public bool IsLoseScene = false;
    private void Awake()
    {
        health.Initialize();
        energy.Initialize();
        if (Instance==null)
        {
            Instance = this;
        }

        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {      
        ScrewScoreText.text = "12/" + itemScore;
        FuelScoreText.text = "12/" + fuelScore;
        PotionScoreText.text = "12/" + potionScore;
        liveText.text = "X" + playerLives.ToString();
        scoreText.text = score.ToString();
        
        string[] tmpHealth = HealthText.text.Split(':');
        HealthText.text = tmpHealth[0] + ": " + health.CurrentVal;

        string[] tmpEnergy = EnergyText.text.Split(':');
        EnergyText.text = tmpEnergy[0] + ": " + energy.CurrentVal;
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }
    public void processPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            LoseScene();            
        }
    }

    private void TakeLife()
    {
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        string[] tmpHealth = HealthText.text.Split(':');
        health.CurrentVal = health.MaxVal;
        HealthText.text = tmpHealth[0] + ": " + health.CurrentVal;    
        liveText.text = "X" + playerLives.ToString();      
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void WinScene()
    {
        IsWinScene = true;
        winSceneMenu.SetActive(true);    
    }
    public void LoseScene()
    {
        IsLoseScene = true;
        loseSceneMenu.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;//make sure that everything will setting in place on time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        winSceneMenu.SetActive(false);
        loseSceneMenu.SetActive(false);
        Debug.Log(SceneManager.GetActiveScene().name);
        Destroy(gameObject);
    }
    public void NextLevel()
    {
        Time.timeScale = 1;//make sure that everything will setting in place on time
        SceneManager.LoadScene(SceneManager.sceneCount + 1);
        winSceneMenu.SetActive(false);
        loseSceneMenu.SetActive(false);
        Destroy(gameObject);
    }
    public void MainManu()
    {
        Time.timeScale = 1;//make sure that everything will setting in place on time
        SceneManager.LoadScene(0);
        winSceneMenu.SetActive(false);
        loseSceneMenu.SetActive(false);
        Destroy(gameObject);
    }
    public void HealthPointDeacrese()
    {
        health.CurrentVal -= 10;
        string[] tmpHealth = HealthText.text.Split(':');
        HealthText.text = tmpHealth[0] + ": " + health.CurrentVal;
    }

    public void EnergyPointIncrease()
    {
        energy.CurrentVal += 5;
        string[] tmpEnergy = EnergyText.text.Split(':');
        EnergyText.text = tmpEnergy[0] + ": " + energy.CurrentVal;
    }

    public void ItemPointIncrease()
    {
        energy.CurrentVal += 5;
        itemScore += 1;
        ScrewScoreText.text = "12/" + itemScore;
    }
    public void FuelsPointIncrease()
    {
        energy.CurrentVal += 5;
        fuelScore += 1;
        FuelScoreText.text = "12/" + fuelScore;
    }

    public void PotionsPointIncrease()
    {
        energy.CurrentVal += 5;
        potionScore += 1;
        PotionScoreText.text = "12/" + potionScore;
    }
    public float getCurrentHealthVal()
    {
        return health.CurrentVal;
    }

}
