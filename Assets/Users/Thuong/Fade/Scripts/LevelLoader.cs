using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0.5f;
    int LevelIndex = 1;
    string sceneName;
    Scene Scene;
    private GameObject player = null ;
    private bool rockingChange;
    // Update is called once per frame
    private void Start()
    {
        sceneName = Scene.name;
        if (sceneName == "ThuongScene")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            rockingChange = false;
        }
    }
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        if (player == null && rockingChange == false)
        {
            rockingChange = true;
            if(rockingChange == true)
            {
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex +1)); // 
            }
        }
        return;
    }
    public void LoadNextLevel()
    {
        if(sceneName == "GameOverScene")
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 2)); // retry
        }
        return;
    }
    
    IEnumerator LoadLevel(int LevelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(LevelIndex);
    }
}
