using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text secsLeftTxt;
    [SerializeField] private TMP_Text realScoreTxt;

    private int secsLeft = 60;
    private int score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        GetStarted();
    }

    private void GetStarted()
    {
        secsLeft = 30;
        score = 0;
        InvokeRepeating("SetTimer", 1f, 1f);
    } // GetStarted
    

    public void SetTimer()
    {
        if (secsLeft > 0)
        {
            secsLeft--;
            UpdateTimerUi();
        }
        else
        {
            GameManager.Instance.playing = false;
            Debug.Log("time expired");
            CancelInvoke("SetTimer");
        }

    } // SetTimer

    private void UpdateTimerUi()
    {
        secsLeftTxt.text = $"{secsLeft} secs";
    } // UpdateTimerUI
    
    public void UpdateTargetsUI(int scoreGain)
    {
        score += scoreGain;
        realScoreTxt.text = score.ToString();
    } // UpdateTimerUI
    

}
