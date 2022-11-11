using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    //private float GameStartCount;
    //public float time = 100f;
    //public Text timeText;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    GameStartCount = 1f;
    //    time = 100;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //        time -= Time.deltaTime;
    //        timeText.text = time.ToString();
    //}
    [SerializeField] private Text uiText;

    private int remainingDuration = 100;

    private bool Pause;
    private void Start()
    {
        StartCoroutine(UpdateTimer());
    }

    //public void Being2(int Second)
    //{
    //    remainingDuration = Second;
    //    StartCoroutine(UpdateTimer(Second));
    //}

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
