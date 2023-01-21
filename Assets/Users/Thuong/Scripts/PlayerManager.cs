using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour 
{
    GameObject Player;
    private AsyncOperation asyncOperation;
    private int SceneIndex;
    [SerializeField] private Text uiText;

    private int remainingDuration = 90;

    private bool Pause;
    private void Start()
    {
        StartCoroutine(UpdateTimer());
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (Player == null || remainingDuration == 0)
        {
            SceneIndex = 4;
            StartCoroutine(LoadScene(SceneIndex)); // game overScene
        }
        if(Input.GetKeyDown(KeyCode.A) ||  Movement.Instance.isClear == true)
        {
            StartCoroutine(LoadScene(SceneIndex = 3)); // clearScene
        }
    }

    //public void Being2(int Second)
    //{
    //    remainingDuration = Second;
    //    StartCoroutine(UpdateTimer(Second));
    //}
    IEnumerator LoadScene(int index)
    {
        asyncOperation = SceneManager.LoadSceneAsync(index);
        yield return null;
    }

        private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            if (!Pause)
            {
                uiText.text = "Time left : " + remainingDuration.ToString("F1"); //$"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                //uiFill.fillAmount = Mathf.InverseLerp(0, setTime, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
    }
}
