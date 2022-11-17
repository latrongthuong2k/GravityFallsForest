using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour 
{
    GameObject Player;
    bool checkBool;
    private AsyncOperation asyncOperation;

    [SerializeField] private Text uiText;

    private int remainingDuration = 100;

    private bool Pause;
    private void Start()
    {
        StartCoroutine(UpdateTimer());
        Player = GameObject.FindGameObjectWithTag("Player");
        checkBool = false;
    }
    private void FixedUpdate()
    {
        if (Player == null && checkBool == false)
        {
            checkBool = true;
            StartCoroutine("LoadGame");
        }
    }

    //public void Being2(int Second)
    //{
    //    remainingDuration = Second;
    //    StartCoroutine(UpdateTimer(Second));
    //}
    IEnumerator LoadGame()
    {
        asyncOperation = SceneManager.LoadSceneAsync(3);
        yield return asyncOperation;
    }

        private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            if (!Pause)
            {
                uiText.text = remainingDuration.ToString(); //$"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                //uiFill.fillAmount = Mathf.InverseLerp(0, setTime, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
    }
}
