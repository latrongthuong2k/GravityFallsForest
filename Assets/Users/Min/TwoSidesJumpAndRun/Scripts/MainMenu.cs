using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class MainMenu : MonoBehaviour
{
    public Text bestScoreInfo;
    public Slider volumeSlider;
    public Button playButton, quitButton;
    public Button[] ModeButton;
    public AudioClip clickSFX;
     int gameSceneIndex = 1;
    public Image loadingImage;

    public static int modeNum;

    private int bestScore;
    private AudioSource audioSource;
    private AsyncOperation asyncOperation;
    Scene scene;
    string sceneName;
    void Start()
    {
        sceneName = scene.name;
        //CaseScene();

        audioSource = GetComponent<AudioSource>();

        bestScore = PlayerPrefs.GetInt("Best");

        bestScoreInfo.text = bestScore > 0 ? "BEST SCORE" + "\n" + bestScore.ToString() : "";

        loadingImage.gameObject.SetActive(false);

        playButton.onClick.AddListener(() =>
        {
            Utilities.PlaySFX(audioSource, clickSFX);
            PlayerPrefs.SetFloat("Vol", volumeSlider.value);
            loadingImage.gameObject.SetActive(true);
            StartCoroutine("LoadGame");
        });

        quitButton.onClick.AddListener(() =>
        {
            Utilities.PlaySFX(audioSource, clickSFX);
            PlayerPrefs.SetFloat("Vol", volumeSlider.value);
            Application.Quit();
        });
        //addListenerButton();


        volumeSlider.value = PlayerPrefs.HasKey("Vol") ? PlayerPrefs.GetFloat("Vol") : 1;

        AudioListener.volume = volumeSlider.value;
    }
    
    void CaseScene() // play Button
    {
        switch (sceneName)
        {
            case "GameOverScene":
                gameSceneIndex = 1;
                break;
            case "ClearScene":
                gameSceneIndex = 1;
                break;
            //case "ThuongScene":
            //    gameSceneIndex = 1;
            //    break;
            case "ModeScene":
                gameSceneIndex = 2;
                ModeButton[0].onClick.AddListener(() =>
                {
                    Utilities.PlaySFX(audioSource, clickSFX);
                    PlayerPrefs.SetFloat("Vol", volumeSlider.value);
                    modeNum = 1;
                });
                ModeButton[1].onClick.AddListener(() =>
                {
                    Utilities.PlaySFX(audioSource, clickSFX);
                    PlayerPrefs.SetFloat("Vol", volumeSlider.value);
                    modeNum = 2;
                });
                ModeButton[2].onClick.AddListener(() =>
                {
                    Utilities.PlaySFX(audioSource, clickSFX);
                    PlayerPrefs.SetFloat("Vol", volumeSlider.value);
                    modeNum = 3;
                });
                break;
        }
    }
    void addListenerButton()
    {
       
    }
    IEnumerator LoadGame()
    {
        asyncOperation = SceneManager.LoadSceneAsync(gameSceneIndex); // menuScene
        yield return asyncOperation;
    }
}
