using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class HeartRateSimulation : MonoBehaviour
{
    public Image CircleImage;
    public float MinScale = 0.2f;
    public float MaxScale = 0.48f;
    public float HeartRateBPM = 70f;
    public float HeartRateGoal = 90f;
    public float ElapseTime = 0;
    public float CycleDuration;

    [SerializeField]
    private List<double> _bpmHistory = new List<double>();
    public List<double> BPMHIstory
    {
        get
        {
            int count = _bpmHistory.Count;
            List<double> lastSix = _bpmHistory.GetRange(Math.Max(0, count - 6), Math.Min(6, count));
            return lastSix;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Calculate the duration of one heart beat cycle (up and down) based on heart rate BPM
        CycleDuration = 60f / HeartRateBPM;
    }

    // Update is called once per frame
    void Update()
    {
        // Update elapse time
        ElapseTime += Time.deltaTime;
        // Calculate the current scale factor using a sine wave
        float scale = Mathf.Lerp(MinScale, MaxScale, MathF.Sin((float)(ElapseTime / CycleDuration * Math.PI * 2)) * 0.5f + 0.5f);
        // Apply the scale to the circle image
        CircleImage.transform.localScale = new Vector3 (scale, scale, 1.0f);
        //Reset Elapsed time to avoid overflow
        if(ElapseTime >= CycleDuration) 
        {
            ElapseTime = 0;
        }
    }

    // This metod allows updating the heart rate dynamically
    public void UpdateHeartRate(float newHeartRateBPM) 
    {
        HeartRateBPM = newHeartRateBPM;
        CycleDuration = 60f / HeartRateBPM;
        _bpmHistory.Add(HeartRateBPM);

        if(HeartRateBPM <= HeartRateGoal)
        {
            string htmlValue = "#00D9FF"; // Blue Color
            Color newCol;
            if(ColorUtility.TryParseHtmlString(htmlValue, out newCol)) 
            {
                CircleImage.color = newCol;
            }
            GameManager.Instance.UIHealthDashboard.DialogSystemView.StartDialog
                (
                  GameManager.Instance.UIHealthDashboard.DialogSystemView.DialogNormalBPM
                );
            GameManager.Instance.MixBotAI.BotAIFSM.SendEvent("MediumBPM");
        }else if(HeartRateBPM > HeartRateGoal) 
        {
            string htmlValue = "#FF0017"; // Red Color
            Color newCol;
            if(ColorUtility.TryParseHtmlString(htmlValue, out newCol)) 
            {
                CircleImage.color = newCol;
            }

            GameManager.Instance.UIHealthDashboard.DialogSystemView.StartDialog
               (
                 GameManager.Instance.UIHealthDashboard.DialogSystemView.DialogHighBPM
               );
            GameManager.Instance.MixBotAI.BotAIFSM.SendEvent("HighBPM");
        }
        
        // If Heart Rate if more of the 180 Goal
        // Let Show de MiXBotSelectionMenuView
        if (HeartRateBPM > 150f)
        {
            GameManager.Instance.TriggerShowMixBotSelectionMenuView();
        }
        
    }

    
}
