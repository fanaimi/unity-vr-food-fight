using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text secsLeftTxt;
    [SerializeField] private TMP_Text numOfTargetsTxt;

    private int secsLeft = 60;
    private int numOfTargets = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        GetStarted();
    }

    private void GetStarted()
    {
        secsLeft = 60;
        numOfTargets = 0;
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
            CancelInvoke("SetTimer");
        }

    } // SetTimer

    private void UpdateTimerUi()
    {
        secsLeftTxt.text = $"{secsLeft} secs";
    } // UpdateTimerUI
    
    public void UpdateTargetsUI()
    {
        numOfTargets++;
        numOfTargetsTxt.text = numOfTargets.ToString();
    } // UpdateTimerUI
    

}
