using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MixBotRegainControlView : MonoBehaviour
{
    [SerializeField] 
    private Image regainCircleImage;
    
    [SerializeField]
    private Image regainBackgroundImage;
    
    [SerializeField] private Sprite regainCircleSprite1;
    [SerializeField] private Sprite regainCircleSprite2;
    [SerializeField] private Sprite backgroundSprite1;
    [SerializeField] private Sprite backgroundSprite2;

    [SerializeField] private string hexColorBackground1 = "FF32A6";
    [SerializeField] private string hexColorBackground2 = "00B2FF";
    
    [SerializeField] 
    private int countDownTime = 15;

    [SerializeField] 
    private float timer;
    
    [SerializeField]
    private TMP_Text countDownDisplay;
    
    // Scaling parameters
    public float ScaleSpeed = 0.5f;     // How fast the circle scales
    public float MinScale = 0.8f;       // Minimum scale factor
    public float MaxScale = 1.2f;       // Maximum scale factor
    
    // Rotation parameters
    public float RotationSpeed = 50f;   // How fast the circle rotates (degrees per second)

    private bool scalingUp = true;      // To track if we are scaling up or down
    
    private Coroutine coroutine;

    public ButtonManager RegainControlButton;
    // Start is called before the first frame update

    private void Awake()
    {
        // Set the timer
        timer = countDownTime;

        if (countDownDisplay != null)
        {
            countDownDisplay.text = countDownTime.ToString();
        }
        
        
        coroutine = StartCoroutine(StartCountDown());
        regainCircleImage.sprite = regainCircleSprite1;
        SetBackgroundColor("#FF32A6");
        RegainControlButton.Interactable(false);
        RegainControlButton.onClick.AddListener(() =>
        {
            ExecuteRegainControlBtn();
        });

    }

    void Start()
    {
        
        gameObject.SetActive((false));
    }

    private void OnEnable()
    {
        // Set the timer
        timer = countDownTime;

        if (countDownDisplay != null)
        {
            countDownDisplay.text = countDownTime.ToString();
        }
        
        if (coroutine != null)
        {
            coroutine = null;
        }
        coroutine = StartCoroutine(StartCountDown());
        regainCircleImage.sprite = regainCircleSprite1;
        SetBackgroundColor("#FF32A6");
        
        RegainControlButton.onClick.AddListener(() =>
        {
            ExecuteRegainControlBtn();
        });
        
        
    }

    private void Update()
    {
        ScaleCircle();
        RotateCircle();
    }

    IEnumerator StartCountDown()
    {
        while (timer > 0)
        {
            // wait 1 second
            yield return new WaitForSeconds(1.0f);
            
            // Decrease the timer
            timer--;
            
            // Optionally update the countdown display
            if (countDownDisplay != null)
            {
                countDownDisplay.text = timer.ToString();
            }
        }
        
        // When timer reached 0, execute the function
       TimerFinished();
    }

    public void TimerFinished()
    {
        // Your code to execute when the countdown ends
        Debug.Log("Countdown finished! Execute your function here.");
        if (GameManager.Instance.HeartRateSimulation.HeartRateBPM < 150)
        {
            GameManager.Instance.TriggerShowModalMessage("Congratulations! You Regain Control of your heart rate!");
            regainCircleImage.sprite = regainCircleSprite2;
            SetBackgroundColor("#00B2FF");
            RegainControlButton.Interactable(true);
        }
        else
        {
            if (coroutine != null)
            {
                coroutine = null;
            }
            timer = countDownTime;
            coroutine = StartCoroutine(StartCountDown());
            //RegainControlButton.Interactable(false);
        }
    }
    
    void ScaleCircle()
    {
        // If scaling up and the image is smaller than the max scale
        if (scalingUp && regainCircleImage.transform.localScale.x < MaxScale)
        {
            // Scale the image up
            regainCircleImage.transform.localScale += Vector3.one * ScaleSpeed * Time.deltaTime;
        }
        else if (!scalingUp && regainCircleImage.transform.localScale.x > MinScale)
        {
            // Scale the image down
            regainCircleImage.transform.localScale -= Vector3.one * ScaleSpeed * Time.deltaTime;
        }

        // If we've reached max scale, switch to scaling down
        if (regainCircleImage.transform.localScale.x >= MaxScale)
        {
            scalingUp = false;
        }
        // If we've reached min scale, switch to scaling up
        else if (regainCircleImage.transform.localScale.x <= MinScale)
        {
            scalingUp = true;
        }
    }
    
    void RotateCircle()
    {
        // Rotate the circle image continuously
        regainCircleImage.transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
    }

    void SetBackgroundColor(string hexCode)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hexCode, out color))
        {
            regainBackgroundImage.color = color;
        }
        else
        {
            Debug.LogError("Invalid Hex Color Code: " + hexCode);
        }
    }

     void ExecuteRegainControlBtn()
     {
         // Check if client if login with token or log out, if the token is null or empty, it means logout
         string token = GameManager.Instance.APIManager.GetToken;
         if (string.IsNullOrEmpty(token))
         {
             GameManager.Instance.TriggerShowLoginView();
         }
         else
         {
             GameManager.Instance.TriggerShowHealthDataGraphicView();
         }
     }
}
