using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IngameUI : MonoBehaviour 
{
    public Image fadeOutImage;             
    public Text currentScore;               
    public Text bestScore;                  
    public GameObject gameoverPanel;        //Game over panel object;
    public Text gameOverText;               //Game over text UI object;
    public Button replayButton;             //Replay UI button;
    public Button quitButton;               //Quit UI button;
    public AudioClip clickSFX;              //Buttons click sfx;

    private int score;
    private int best;

    private GameManager GM;
    private CanvasGroup panelAlpha;
    private CanvasGroup fadeOutAlpha;
    private AudioSource audioSource;
	// Use this for initialization
	void Start () 
    {
        //Caching components;
        GM = GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        fadeOutAlpha = fadeOutImage.gameObject.GetComponent<CanvasGroup>();
        panelAlpha = gameoverPanel.GetComponent<CanvasGroup>();

        fadeOutAlpha.alpha = 1;                 
        fadeOutAlpha.blocksRaycasts = false;    
        panelAlpha.alpha = 0;                   
        panelAlpha.interactable = false;        

        CalculateScore();                       
        best = LoadBestScore();                 

        //Add buttons listeners
        replayButton.onClick.AddListener(()=>
        {
            Utilities.PlaySFX(audioSource, clickSFX);      
            StartCoroutine("ResetGame");                    
        });

        quitButton.onClick.AddListener(()=>
        {
            Utilities.PlaySFX(audioSource, clickSFX);      
            StartCoroutine("QuitGame");                     
        });
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        if (fadeOutAlpha.alpha > 0)
            fadeOutAlpha.alpha = Mathf.MoveTowards(fadeOutAlpha.alpha, 0, 1.0F * Time.deltaTime);
       
        currentScore.text = "スコア : " + score.ToString();
        
        bestScore.text = best > 0 ? "ベスト : " + best.ToString() : "";
       
        gameOverText.text = score > best ? "NEW BEST SCORE: " + score.ToString() : "YOUR SCORE: " + score.ToString();
        //If game over,
        if (GameManager.isGameOver)
        {
            
            CancelInvoke("AddScore");
            
            panelAlpha.alpha = Mathf.MoveTowards(panelAlpha.alpha, 1, 3.0F * Time.deltaTime);
        }
        else
            panelAlpha.alpha = 0; 

        
        panelAlpha.interactable = panelAlpha.alpha > 0.5F;
	}

    
    void AddScore()
    {
        score ++;
    }
    //Calculate score function;
    public void CalculateScore()
    {
        InvokeRepeating("AddScore", 1, 1);
    }

   
    int LoadBestScore()
    {
        return PlayerPrefs.GetInt("Best");
    }
   
    void SaveScore()
    {
        PlayerPrefs.SetInt("Best", score);
    }
    //Reset function;
    public void Reset()
    {
        fadeOutAlpha.alpha = 1;     
      
        if (score > best)           
        {
            
            SaveScore();        
            best = score;
        }
        
        score = 0;
        
        CalculateScore();
    }

    
    IEnumerator ResetGame()
    {
        
        while (audioSource.isPlaying)
            yield return null;
        
        GM.ResetGame();
    }
    
    IEnumerator QuitGame()
    {
        
        while (audioSource.isPlaying)
            yield return null;
        //Quit game;
        Application.Quit();
    }
}
