using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDataGraphicView : MonoBehaviour
{
    public string FullName { get => fullName; set => fullName = value;  }
    [SerializeField] private string fullName;
    
    public string StressLevel 
    {
        get {  return stressLevel; }
        set
        {
            stressLevel = value;
            //stressLevelText.text = stressLevel;
        } 
    }
    [SerializeField] private string stressLevel;
    
    public int Height {get => height; set => height = value;}
    [SerializeField] private int height;
    
    public int Weight {get => weight; set => weight = value;}
    [SerializeField] private int weight;
    
    public string HeartRateGraph { get => heartRateGraph; set => heartRateGraph = value; }
    [SerializeField] private string heartRateGraph;
    
    public int[] HeartRateHistory { get => heartRateHistory; set => heartRateHistory = value; }
    [SerializeField] private int[] heartRateHistory;
    
    public int[] StressLevelHistory { get => stressLevelHistory; set => stressLevelHistory = value; }
    [SerializeField] private int[] stressLevelHistory;
    
    public int[] BreathingLevelHistory { get => breathingLevelHistory; set => breathingLevelHistory = value; }
    [SerializeField] private int[] breathingLevelHistory;
    
    public int[] HealthScores { get => healthScores; set => healthScores = value; }
    [SerializeField] private int[] healthScores;
    
    
    [SerializeField] private TMP_Text stressLevelText;
    
    [SerializeField] private TMP_Text breathingLevelText;
    
    [SerializeField] private int breathingLevel;
    public int BreathingLevel 
    { 
        get { return breathingLevel; }
        set
        {
            breathingLevel = value;
            //breathingLevelText.text = breathingLevel.ToString();
        }
    }

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text heightText;
    [SerializeField] private TMP_Text weightText;
    [SerializeField] private Text scoresText;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void GetHealthDataGraphicView()
    {
        GameManager.Instance.APIManager.ExecuteHealthDataCoroutine();
    }

    public void SetHealthDataGraphicView()
    {
        nameText.text = FullName;
        stressLevelText.text = stressLevelHistory[0].ToString();
        breathingLevelText.text = breathingLevelHistory[0].ToString();
        heightText.text = Height.ToString();
        weightText.text = Weight.ToString();
        scoresText.text = HealthScores[0].ToString();

    }
}
